namespace Model.GameEntity.Health
{
    public class Poisoning: HealthProperty
    {
        public int Duration { get; private set; }
        public readonly int damage;
        public override HealthPropertyType Type { get; }

        public Poisoning(int damage = 1, int duration = 4)
        {
            Type = HealthPropertyType.Poisoning;
            this.Duration = duration;
            this.damage = damage;
        }

        public override void InitialAction(Health health) {}

        public override void OnTurnEnd(Health health)
        {
            health.Target.TakeDamage(new DamageInfo(damage));
            Duration--;
            if (Duration == 0)
                health.DeleteProperty(this);
        }

        public override void FinalAction(Health health) {}
    }
}