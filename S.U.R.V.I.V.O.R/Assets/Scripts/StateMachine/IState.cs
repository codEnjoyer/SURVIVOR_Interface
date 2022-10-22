public interface IState
{
    public StateMachine StateMachine { get; }
    public void Enter();
    public void Exit();
}