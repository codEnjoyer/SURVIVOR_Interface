
public abstract class HealthProperty
{
   public abstract HealthPropertyType Type { get; }
   public abstract void InitialAction(Health health);
   public abstract void OnTurnEnd(Health health);
   public abstract void FinalAction(Health health);

   public override bool Equals(object obj)
   {
      if (obj is HealthProperty healthProperty)
         return Equals(healthProperty);
      return false;
   }

   private bool Equals(HealthProperty other) => Type == other.Type;

   public override int GetHashCode() => (int) Type;
}