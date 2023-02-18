public class StateMachine
{
    public State CurrentState { get; private set; }
    public State PreviousState { get; private set; }
    public State DefaultState { get; set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        PreviousState = CurrentState;
        PreviousState.Exit();
        if (DefaultState != null && CurrentState == newState)
        {
            CurrentState = DefaultState;
        }
        else
        {
            CurrentState = newState;
        }

        CurrentState.Enter();
    }
}