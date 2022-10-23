public abstract class InterfaceState: State
{
    protected InterfaceController contr;
    public InterfaceState(InterfaceController contr, StateMachine sm)
    {
        this.contr = contr;
        this.stateMachine = sm;
    }
}