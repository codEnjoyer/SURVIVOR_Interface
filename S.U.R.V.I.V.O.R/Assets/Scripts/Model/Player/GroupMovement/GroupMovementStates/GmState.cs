namespace Model.Player.GroupMovement.GroupMovementStates
{
    public abstract class GmState: State
    {
        protected readonly GroupMovementLogic gml;

        protected GmState(GroupMovementLogic gml, StateMachine sm)
        {
            this.gml = gml;
            stateMachine = sm;
        }

    }
}