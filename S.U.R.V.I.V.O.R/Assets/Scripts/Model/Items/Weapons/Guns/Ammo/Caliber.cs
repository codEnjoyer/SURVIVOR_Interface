using System;

namespace Assets.Scripts.Model
{
    public abstract class Caliber
    {
        public Caliber(float damage,
            float extraFireDistance,
            float armorPenetrating,
            float boneBreakingChance,
            float bleedingChance,
            float recoil,
            float extraAccuracy,
            float noise,
            float weight
        )
        {
            Damage = damage;
            ExtraFireDistance = extraFireDistance;
            ArmorPenetrating = armorPenetrating;
            BoneBreakingChance = boneBreakingChance;
            BleedingChance = bleedingChance;
            Recoil = recoil;
            ExtraAccuracy = extraAccuracy;
            Noise = noise;
            Weight = weight;
        }

        public float Damage { get; protected set; }
        public float ExtraFireDistance { get; protected set; }
        public float ArmorPenetrating { get; protected set; }
        public float BoneBreakingChance { get; protected set; }
        public float BleedingChance { get; protected set; }
        public float Recoil { get; protected set; }
        public float ExtraAccuracy { get; protected set; }
        public float Noise { get; protected set; }
        public float Weight { get; protected set; }
    }
}