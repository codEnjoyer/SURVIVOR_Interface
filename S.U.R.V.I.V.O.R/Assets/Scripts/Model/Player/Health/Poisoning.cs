
public class Poisoning: IHealthProperty
{
    public void InitialAction (Health health)
    {
        throw new System.NotImplementedException();
    }

    public void OnTurnEnd(Health health)
    {
        
    }

    public void FinalAction(Health health)
    {
        throw new System.NotImplementedException();
    }


    public override bool Equals(object obj)
    {
        return obj is Poisoning;
    }
}