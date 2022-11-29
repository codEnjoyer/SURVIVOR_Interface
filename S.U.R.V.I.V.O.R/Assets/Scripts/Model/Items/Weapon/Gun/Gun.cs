using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Gun: MonoBehaviour, IWeapon
{
    private readonly List<GunModuleType> availableGunModules;

    protected Magazine currentMagazine;

    public Magazine CurrentMagazine => currentMagazine;

    protected Gun(List<GunModuleType> availableGunModules)
    {
        this.availableGunModules = availableGunModules;
    }

    public abstract bool IsFirstGun { get; }
    public abstract GunData Data { get; }
    public abstract IReadOnlyCollection<GunModule> GunModules { get; }
    public abstract void AddGunModule(GunModule gunModule);
    public abstract void RemoveGunModule(GunModule gunModule);
    public abstract Magazine Reload(Magazine magazine);
    public bool CheckGunModule(GunModuleType module) => availableGunModules.Contains(module);
    public abstract float AttackDistance { get; }
    public abstract void Attack(List<BodyPart> targets, float distance, Skills skills);
}