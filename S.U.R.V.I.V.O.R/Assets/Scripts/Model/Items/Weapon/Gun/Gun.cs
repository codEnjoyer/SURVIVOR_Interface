using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public abstract class Gun : MonoBehaviour, IWeapon
{
    protected Magazine currentMagazine;
    protected readonly List<GunModule> gunModules = new();

    public abstract GunData Data { get; }
    public abstract Magazine Reload(Magazine magazine);
    public abstract void Attack(List<BodyPart> targets, float distance, Skills skills);
    
    public Magazine CurrentMagazine => currentMagazine;
    public bool CheckGunModule(GunModuleType module) => Data.AvailableGunModules.Contains(module);
    public float AttackDistance => Data.FireDistance;
    public IReadOnlyCollection<GunModule> GunModules => gunModules;

    public event Action OnModulesChanged;
    public bool AddGunModule(GunModule newGunModule)
    {
        if (Data.AvailableGunModules.Contains(newGunModule.Data.ModuleType)
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