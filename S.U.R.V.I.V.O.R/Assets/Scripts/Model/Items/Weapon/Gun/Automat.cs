using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Automat : Gun
{
    [SerializeField] private GunData data;
    [SerializeField] private List<GunModule> gunModule;

    public override bool IsFirstGun { get; }
    public override GunData Data => data;
    public override IReadOnlyCollection<GunModule> GunModules { get; }

    private Automat(List<GunModuleType> availableGunModules) : base(availableGunModules)
    {
        availableGunModules.AddRange(new[]
        {
            GunModuleType.Grip,
            GunModuleType.Scope,
            GunModuleType.Shutter,
            GunModuleType.Spring,
            GunModuleType.Suppressor,
            GunModuleType.Tactical
        });
    }

    public override void AddGunModule(GunModule gunModule)
    {
        throw new NotImplementedException();
    }

    public override void RemoveGunModule(GunModule gunModule)
    {
        throw new NotImplementedException();
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

    public override float AttackDistance { get; }

    public override void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        throw new NotImplementedException();
    }
}