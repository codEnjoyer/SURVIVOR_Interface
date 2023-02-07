public abstract class State
{
    protected StateMachine stateMachine;
    public virtual void Enter(){}

    public virtual void Update(){}
    public virtual void FixedUpdate (){}
    public virtual void Exit(){}
}