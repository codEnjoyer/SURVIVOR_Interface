using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using UnityEngine;
using Random = System.Random;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Equipable))]
public abstract class Gun : MonoBehaviour, IWeapon
{
    public Magazine CurrentMagazine { get; protected set; }

    protected readonly List<GunModule> gunModules = new();
    public event Action OnModulesChanged;
    public abstract GunData Data { get; }
    public bool CheckGunModule(GunModuleType module) => Data.AvailableGunModules.Contains(module);
    public IReadOnlyCollection<GunModule> GunModules => gunModules;

    protected Random rnd;

    protected float OptimalFireDistanceBegin =>
        Data.OptimalFireDistanceBegin + gunModules.Sum(x => x.Data.DeltaAverageDistanceBegin);
    protected float OptimalFireDistanceEnd =>
        Data.OptimalFireDistanceEnd + gunModules.Sum(x => x.Data.DeltaAverageDistanceEnd);
    
    protected float SpreadSizeOnOptimalFireDistance => Data.SpreadSizeOnOptimalFireDistance + GunModules.Sum(x => x.Data.DeltaSpreadSizeOnOptimalFireDistance);

    protected virtual void Awake()
    {
        rnd = new Random();
        CurrentMagazine = new Magazine();
    }

    public virtual Magazine Reload(Magazine magazine)
    {
        if (CurrentMagazine == null)
        {
            CurrentMagazine = magazine;
            return null;
        }

        var result = CurrentMagazine;
        CurrentMagazine = magazine;
        return result;
    }

    public abstract void Attack(Vector3 targetPoint, Skills skills);
    
    public bool AddGunModule(GunModule newGunModule)
    {
        if (CheckGunModule(newGunModule.Data.ModuleType)
            && !gunModules.Any(module => module.Data.ModuleType.Equals(newGunModule.Data.ModuleType)))
        {
            gunModules.Add(newGunModule);
            OnModulesChanged?.Invoke();
            return true;
        }

        return false;
    }

    public bool RemoveGunModule(GunModule gunModule)
    {
        OnModulesChanged?.Invoke();
        return gunModules.Remove(gunModule);
    }
}