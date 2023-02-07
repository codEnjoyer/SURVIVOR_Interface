using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.Items;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
[RequireComponent(typeof(Equipable))]
public abstract class Gun : MonoBehaviour, IWeapon
{
    public Magazine CurrentMagazine { get; protected set; }
    protected readonly List<GunModule> gunModules = new();
    public event Action OnModulesChanged;
    public abstract GunData Data { get; }

    public bool CheckGunModule(GunModuleType module) => Data.AvailableGunModules.Contains(module);
    public float AttackDistance => Data.FireDistance;
    public IReadOnlyCollection<GunModule> GunModules => gunModules;

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
    
    public abstract void Attack(List<BodyPart> targets, float distance, Skills skills);

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