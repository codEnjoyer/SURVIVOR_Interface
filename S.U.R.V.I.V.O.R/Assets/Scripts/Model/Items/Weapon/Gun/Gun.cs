﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class Gun : MonoBehaviour, IWeapon
{
    protected Magazine currentMagazine;
    protected readonly List<GunModule> gunModules = new();

    public abstract GunData Data { get; set; }
    public abstract Magazine Reload(Magazine magazine);
    public abstract void Attack(List<BodyPart> targets, float distance, Skills skills);


    public bool IsFirstGun => Data.IsFirstGun;
    public Magazine CurrentMagazine => currentMagazine;
    public bool CheckGunModule(GunModuleType module) => Data.AvailableGunModules.Contains(module);
    public float AttackDistance => Data.FireDistance;
    public IReadOnlyCollection<GunModule> GunModules => gunModules;

    public bool AddGunModule(GunModule newGunModule)
    {
        if (Data.AvailableGunModules.Contains(newGunModule.Data.ModuleType)
            && !gunModules.Any(module => module.Data.ModuleType.Equals(newGunModule.Data.ModuleType)))
        {
            gunModules.Add(newGunModule);
            return true;
        }

        return false;
    }

    public bool RemoveGunModule(GunModule gunModule) => gunModules.Remove(gunModule);
}