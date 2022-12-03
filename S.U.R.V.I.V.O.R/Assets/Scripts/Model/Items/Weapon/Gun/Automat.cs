using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Automat : Gun
{
    [SerializeField] private GunData data;


    // private Automat()
    // {
    //     availableGunModules.AddRange(new[]
    //     {
    //         GunModuleType.Grip,
    //         GunModuleType.Scope,
    //         GunModuleType.Shutter,
    //         GunModuleType.Spring,
    //         GunModuleType.Suppressor,
    //         GunModuleType.Tactical
    //     });
    // }

    public override GunData Data
    {
        get => data;
        set => data = value;
    }
    
    public override Magazine Reload(Magazine magazine)
    {
        if (currentMagazine == null)
        {
            currentMagazine = magazine;
            return null;
        }

        var result = currentMagazine;
        currentMagazine = magazine;
        return result;
    }

    public override void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        throw new NotImplementedException();
    }
}