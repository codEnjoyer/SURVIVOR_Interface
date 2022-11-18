
public interface IHealthProperty
{
   public HealthPropertyType Type { get; }
   public void InitialAction(IHealth health);
   public void OnTurnEnd(IHealth health);
   public void FinalAction(IHealth health);
}