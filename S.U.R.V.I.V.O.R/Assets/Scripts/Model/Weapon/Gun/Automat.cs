using System;
using System.Collections.Generic;
using System.Linq;
using Model.Entities.Characters.CharacterSkills;
using Model.GameEntity;
using Model.GameEntity.EntityHealth;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Automat : Gun
{
    [SerializeField] private GunData data;

    public override GunData Data => data;

    public override void Attack(Vector3 targetPoint, Skills skills)
    {
        var position = transform.position;
        foreach (var dot in GetOneShot(targetPoint, out var ammo))
        {
            var wasHitted = Physics.Raycast(position, dot - position, out var hit);
            var target = hit.transform.gameObject.GetComponent<BodyPart>();
            
            if (wasHitted && target != null)
            {
                GiveDamage(ammo, target);
            }
            //var sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //sphere1.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //sphere1.transform.position = newTarget;
        }
    }

    private void GiveDamage(SingleAmmo ammo, BodyPart target)
    {
        target.Hp -= ammo.KeneeticDamage;
        if (target is BodyPathWearableClothes wear && wear.CurrentArmor > 0)
        {           
            GiveDamageToBodyPartWithArmor(ammo, wear);
            return;
        }
        GiveDamageToBodyPartWithoutArmor(ammo, target);
    }
    
    private void GiveDamageToBodyPartWithArmor(SingleAmmo ammo, BodyPathWearableClothes wear)
    {
        var isPenetrated = rnd.NextDouble() <= ammo.ArmorPenetratingChance;
        if (isPenetrated)
        {
            wear.DamageArmor(ammo.ArmorDamageOnPenetration);
            wear.Hp -= ammo.FullDamage * ammo.UpperArmorDamage;
            
            var isBleeding = rnd.NextDouble() <= ammo.BleedingChance;
            if (isBleeding)
                wear.Health.AddProperty(new Bleeding());
            
            var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance;
            if (isBroking)
                wear.Health.AddProperty(new Broking());
        }
        else
        {
            wear.DamageArmor(ammo.ArmorDamageOnNonPenetration);
            wear.Hp -= ammo.FullDamage * ammo.UnderArmorDamage;

            var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance * BONE_BROCKING_ON_NON_PENETRATION_MODIFIER;
            if (isBroking)
                wear.Health.AddProperty(new Broking());
        }
    }
        
    private void GiveDamageToBodyPartWithoutArmor(SingleAmmo ammo, BodyPart target)
    {
        target.Hp -= ammo.FullDamage + ammo.FullDamage * ammo.KeneeticDamage;
        
        var isBleeding = rnd.NextDouble() <= ammo.BleedingChance;
        if (isBleeding)
            target.Health.AddProperty(new Bleeding());
            
        var isBroking = rnd.NextDouble() <= ammo.BoneBrokingChance;
        if (isBroking)
            target.Health.AddProperty(new Broking());
    }
}