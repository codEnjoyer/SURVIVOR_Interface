namespace Assets.Scripts.Model
{
    public class Caliber_919 : Caliber
    {
        public readonly Calibers919 Type;

        public Caliber_919(Calibers919 type, float damage, float extraFireDistance, float armorPenetrating, float boneBreakingChance,
            float bleedingChance, float recoil, float extraAccuracy, float noise, float weight) : base(damage,
            extraFireDistance, armorPenetrating, boneBreakingChance, bleedingChance, recoil, extraAccuracy, noise,
            weight)
        {
            Type = type;
        }
    }
}