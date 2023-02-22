namespace Model.GameEntity.EntityHealth
{
    public class Broking : IHealthProperty
    {
        public HealthPropertyType Type { get; }
        public void InitialAction(Health health)
        {
            throw new System.NotImplementedException();
        }

        public void TurnEndAction(Health health)
        {
            throw new System.NotImplementedException();
        }

        public void FinalAction(Health health)
        {
            throw new System.NotImplementedException();
        }
    }
}