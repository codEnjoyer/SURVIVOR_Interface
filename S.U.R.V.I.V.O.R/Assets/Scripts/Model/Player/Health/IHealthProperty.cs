public interface IHealthProperty
{
   public void InitialAction(Health health);
   public void OnTurnEnd(Health health);
   public void FinalAction(Health health);
   
}