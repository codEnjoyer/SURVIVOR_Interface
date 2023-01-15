using System;
using System.Collections.Generic;
using System.Linq;
using Model.GameEntity;
using Model.GameEntity.Skills;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Automat : Gun
{
    [SerializeField] private GunData data;
    
    public override GunData Data => data;

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