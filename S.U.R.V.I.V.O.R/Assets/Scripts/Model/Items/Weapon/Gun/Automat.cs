using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Automat: MonoBehaviour, IGun
{
    [SerializeField] private GunData data;
    [SerializeField] private Magazine currentMagazine;
    [SerializeField] private List<GunModule> gunModule;

    public bool IsFirstGun => true;
    public GunData Data => data;
    public ICollection<GunModule> GunModules => gunModule;
    public float AttackDistance => 100;

    public void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        throw new NotImplementedException();
    }
    public Magazine Reload(Magazine magazine)
    {
        throw new NotImplementedException();
    }
}

