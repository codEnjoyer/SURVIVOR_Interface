
public class NothingActive: InterfaceState
{
    public NothingActive(InterfaceController contr, StateMachine sm) 
        : base(contr, sm)
    {
    }
    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}