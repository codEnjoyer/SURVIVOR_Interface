using System.Runtime.Serialization;

namespace Model.GameEntity.EntityHealth
{
    [DataContract]
    public class Poisoning : IHealthProperty
    {
        [DataMember] public int Duration { get; private set; }
        [DataMember] public readonly int damage;
        [DataMember] public HealthPropertyType Type { get; private set; }

        public Poisoning(int damage = 1, int duration = 4)
        {
            Type = HealthPropertyType.Poisoning;
            this.Duration = duration;
            this.damage = damage;
        }

        public void InitialAction(Health health)
        {
        }

        public void TurnEndAction(Health health)
        {
            health.Target.TakeDamage(new DamageInfo(damage));
            Duration--;
            if (Duration == 0)
                health.DeleteProperty(this);
        }

        public void FinalAction(Health health)
        {
        }
    }
}