using global::System;

namespace Assets.Scripts.Model
{
    public class Caliber_762 : Caliber
    {
        public readonly Calibers762 Type;

        public Caliber_762(Calibers762 type,float damage, float extraFireDistance, float armorPenetrating, float boneBreakingChance,
            float bleedingChance, float recoil, float extraAccuracy, float noise, float weight) : base(damage,
            extraFireDistance, armorPenetrating, boneBreakingChance, bleedingChance, recoil, extraAccuracy, noise,
            weight)
        {
            Type = type;
        }
    }
}