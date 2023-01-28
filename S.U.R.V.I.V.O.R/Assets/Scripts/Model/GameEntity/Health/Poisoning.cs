using System.Runtime.Serialization;

namespace Model.GameEntity.Health
{
    [DataContract(Namespace = "Model.GameEntity.Health")]
    public class Poisoning: HealthProperty
    {
        [DataMember] public int Duration { get; private set; }
        [DataMember] public readonly int damage;
        [DataMember] public override HealthPropertyType Type { get; }

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