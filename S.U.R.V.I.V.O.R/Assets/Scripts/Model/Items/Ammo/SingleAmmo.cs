using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Caliber", menuName = "Data/Caliber", order = 50)]
public class SingleAmmo: ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float extraFireDistance;
    [SerializeField] private float armorPenetrating;
    [SerializeField] private float boneBreakingChance;
    [SerializeField] private float bleedingChance;
    [SerializeField] private float recoil;
    [SerializeField] private float extraAccuracy;
    [SerializeField] private float noise;
    [SerializeField] private Caliber caliber;
    
    public float Damage => damage;
    public float ExtraFireDistance => extraFireDistance;
    public float ArmorPenetrating => armorPenetrating;
    public float BoneBreakingChance => boneBreakingChance;
    public float BleedingChance => bleedingChance;
    public float Recoil => recoil;
    public float ExtraAccuracy => extraAccuracy;
    public float Noise => noise;
    public Caliber Caliber => caliber;

    public override bool Equals(object other)
    {
        if (other is SingleAmmo ammo)
            return this.Equals(ammo);
        return false;
    }

    private bool Equals(SingleAmmo other)
    {
        return base.Equals(other) && caliber == other.caliber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), (int) caliber);
    }
}
