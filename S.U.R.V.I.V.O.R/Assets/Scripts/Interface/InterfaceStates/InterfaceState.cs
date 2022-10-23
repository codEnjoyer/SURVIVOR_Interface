public abstract class InterfaceState : IState
{
    protected InterfaceController contr;
    protected StateMachine sm;

    StateMachine IState.StateMachine => sm;

    public InterfaceState(InterfaceController contr, StateMachine sm)
    {
        this.contr = contr;
        this.sm = sm;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}