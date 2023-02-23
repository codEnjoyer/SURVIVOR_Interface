using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.GameEntity.EntityHealth;
using Model.Items;
using UnityEngine;
using Random = System.Random;

public enum FireType
{
    Semi,
    SemiAutomatic,
    Auto,
    Burst,
}
[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Equipable))]
public abstract class Gun : MonoBehaviour, IWeapon
{
    protected static Random rnd;
    private const float RECOIL_MULTIPLIER = 0.05f;
    protected const float BONE_BROCKING_ON_NON_PENETRATION_MODIFIER = 1.3f;
    private float currentRecoil;

    public FireType fireType;
    public Magazine CurrentMagazine { get; protected set; }

    protected readonly List<GunModule> gunModules = new();
    public event Action<GunModuleType> OnModulesChanged;
    public abstract GunData Data { get; }
    public IReadOnlyCollection<GunModule> GunModules => gunModules;

    protected float OptimalFireDistanceBegin =>
        Data.OptimalFireDistanceBegin + gunModules.Sum(x => x.Data.DeltaAverageDistanceBegin);

    protected float OptimalFireDistanceEnd =>
        Data.OptimalFireDistanceEnd + gunModules.Sum(x => x.Data.DeltaAverageDistanceEnd);

    protected float SpreadSizeOnOptimalFireDistance => 
        Data.SpreadSizeOnOptimalFireDistance + GunModules.Sum(x => x.Data.DeltaSpreadSizeOnOptimalFireDistance);
    
    protected float DeltaRecoil => 
        Data.DeltaRecoil + GunModules.Sum(x => x.Data.DeltaRecoil);

    private float CurrentRecoil
    {
        get => currentRecoil;
        set => currentRecoil = Math.Max(0, value);
    }

    protected virtual void Awake()
    {
        rnd = new Random();
        CurrentMagazine = new Magazine();
    }

    public bool CheckGunModule(GunModuleType module) => Data.AvailableGunModules.Contains(module);
    
    public virtual Magazine Reload(Magazine magazine)
    {
        if (CurrentMagazine == null)
        {
            CurrentMagazine = magazine;
            OnModulesChanged?.Invoke(GunModuleType.Magazine);
            return null;
        }

        var result = CurrentMagazine;
        CurrentMagazine = magazine;
        OnModulesChanged?.Invoke(GunModuleType.Magazine);
        return result;
    }

    protected abstract int GetAmountOfShots(Skills skills);
    
    public virtual void Attack(Vector3 targetPoint, Skills skills)
    {
        var position = transform.position;
        for (var g = 0; g < GetAmountOfShots(skills); g++)
        {
            foreach (var dot in GetOneShot(targetPoint, out var ammo))
            {
                var wasHitted = Physics.Raycast(position, dot - position, out var hit);
                var target = hit.transform.gameObject.GetComponent<BodyPart>();

                if (wasHitted && target != null)
                {
                    GiveDamage(ammo, target);
                }
            }
        }
    }

    public bool AddGunModule(GunModule newGunModule)
    {
        if (CheckGunModule(newGunModule.Data.ModuleType)
            && !gunModules.Any(module => module.Data.ModuleType.Equals(newGunModule.Data.ModuleType)))
        {
            gunModules.Add(newGunModule);
            OnModulesChanged?.Invoke(newGunModule.Data.ModuleType);
            return true;
        }

        return false;
    }

    public bool RemoveGunModule(GunModule gunModule)
    {
        OnModulesChanged?.Invoke(gunModule.Data.ModuleType);
        return gunModules.Remove(gunModule);
    }

    protected (Vector3, Vector3, Vector3) GetNewBasis(Vector3 targetPoint)
    {
        //НАХОЖДЕНИЕ НОВОГО БАЗИСА
        var position = transform.position;
        var newK = targetPoint - position;
        var newI = new Vector3(0, -newK.z, newK.y);
        var newJ = Vector3.Cross(newI, newK);

        newK = newK.normalized;
        newI = newI.normalized;
        newJ = newJ.normalized;
        return (newI, newJ, newK);
    }

    protected Vector3 GetOffset(float maxRo, float spreadModifier, (Vector3, Vector3, Vector3) newBasis)
    {
        var ro = rnd.NextDouble() * maxRo;
        if (spreadModifier < 0 || spreadModifier > 1)
            throw new ArgumentException();
        ro -= rnd.NextDouble() * (maxRo * spreadModifier);
        var fi = rnd.NextDouble() * (2 * 3.14f);

        //Смещение в новых координатах
        var newX = (float)(ro * Math.Cos(fi));
        var newY = (float)(ro * Math.Sin(fi));
        var newZ = 0;

        var (newI, newJ, newK) = newBasis;

        //Смещение в старых координатах
        var offsetInOldCoordinates = newI * newX + newJ * newY + newK * newZ;

        return offsetInOldCoordinates;
    }

    protected ICollection<Vector3> GetOneShot(Vector3 targetPoint, out SingleAmmo ammo)
    {
        var distance = Vector3.Distance(transform.position, targetPoint);
        ammo = CurrentMagazine.GetAmmo(); //Извлекаем патрон
        //if (ammo == null)
        //    th; //TODO Осечка

        var currentOptDistBegin = OptimalFireDistanceBegin + ammo.DeltaOptimalFireDistanceBegin;
        var currentOptDistEnd = OptimalFireDistanceEnd + ammo.DeltaOptimalFireDistanceEnd;

        var distanceDifference =
            currentOptDistBegin <=  distance && distance >= currentOptDistEnd
                ? 1
                : distance <= currentOptDistBegin
                    ? distance / currentOptDistBegin //TODO сделать "экспоненциальное" увеличение с помощью коэффициента в степени
                    : currentOptDistEnd / distance; //TODO сделать линейное увеличение, как у конуса

        var spreadSize =
            SpreadSizeOnOptimalFireDistance +
            ammo.DeltaSpreadSizeOnOptimalFireDistance + CurrentRecoil*RECOIL_MULTIPLIER; //Чем меньше точность, тем меньше круг

        CurrentRecoil += ammo.Recoil + DeltaRecoil;//TODO после стрельбы убрать отдачу
        
        var maxRo = spreadSize * (1 + (1 - distanceDifference));
        var newBasis = GetNewBasis(targetPoint);
        var result = new List<Vector3>();
        for(var z = 0; z < ammo.AmountOfBullets; z++)
            result.Add(GetOffset(maxRo, 0,newBasis) + targetPoint);
        return result;
    }
    
    protected virtual void GiveDamage(SingleAmmo ammo, BodyPart target)//TODO переопределить для дробовиков
    {
        target.Hp -= ammo.KeneeticDamage;
        if (target is BodyPathWearableClothes wear && wear.CurrentArmor > 0)
        {
            GiveDamageToBodyPartWithArmor(ammo, wear);
            return;
        }

        GiveDamageToBodyPartWithoutArmor(ammo, target);
    }

    protected virtual void GiveDamageToBodyPartWithArmor(SingleAmmo ammo, BodyPathWearableClothes wear)
    {
        var isPenetrated = rnd.NextDouble() <= ammo.ArmorPenetratingChance;
        if (isPenetrated)
        {
            wear.DamageArmor(ammo.ArmorDamageOnPenetration);
            wear.Hp -= ammo.FullDamage * ammo.UpperArmorDamage;

            var isBleeding = rnd.NextDouble() <= ammo.BleedingChance;
            if (isBleeding)
                wear.Health.AddProperty(new Bleeding());

            var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance;
            if (isBroking)
                wear.Health.AddProperty(new Broking());
        }
        else
        {
            wear.DamageArmor(ammo.ArmorDamageOnNonPenetration);
            wear.Hp -= ammo.FullDamage * ammo.UnderArmorDamage;

            var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance * BONE_BROCKING_ON_NON_PENETRATION_MODIFIER;
            if (isBroking)
                wear.Health.AddProperty(new Broking());
        }
    }

    protected virtual void GiveDamageToBodyPartWithoutArmor(SingleAmmo ammo, BodyPart target)
    {
        target.Hp -= ammo.FullDamage + ammo.FullDamage * ammo.KeneeticDamage;

        var isBleeding = rnd.NextDouble() <= ammo.BleedingChance;
        if (isBleeding)
            target.Health.AddProperty(new Bleeding());

        var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance;
        if (isBroking)
            target.Health.AddProperty(new Broking());
    }
}