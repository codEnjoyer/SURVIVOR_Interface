using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Automat: MonoBehaviour, IMainGun
{
    [SerializeField] private GunData data;
    [SerializeField] private Magazine currentMagazine;
    [SerializeField] private List<GunModule> gunModule;
    
    public GunData Data => data;
    public Caliber Caliber => data.Caliber;
    public IEnumerable<GunModule> GunModules => gunModule;
    public void Attack(List<BodyPart> targets, float distance, Skills skills)
    {
        throw new NotImplementedException();
    }
    public Magazine Reload(Magazine magazine)
    {
        throw new NotImplementedException();
    }
}

