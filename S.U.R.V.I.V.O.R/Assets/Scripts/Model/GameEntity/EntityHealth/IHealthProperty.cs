using System.Runtime.Serialization;

namespace Model.GameEntity.EntityHealth
{
    public interface IHealthProperty
    {
        public  HealthPropertyType Type { get; }
        public  void InitialAction(Health health);
        public  void TurnEndAction(Health health);
        public  void FinalAction(Health health);
    }
}