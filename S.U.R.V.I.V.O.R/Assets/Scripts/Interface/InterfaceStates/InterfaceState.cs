namespace Interface.InterfaceStates
{
    public abstract class InterfaceState: State
    {
        protected InterfaceController contr;

        protected InterfaceState(InterfaceController contr, StateMachine sm)
        {
            this.contr = contr;
            this.stateMachine = sm;
        }
    }
}