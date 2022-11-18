namespace Player.GroupMovement.GroupMovementStates
{
    public class Sleeping: GmState
    {
        public Sleeping(GroupMovementLogic gml, StateMachine sm) : base(gml, sm)
        {
        
        }

        public override void Enter()
        {
            gml.ClearWay();
        }
    }
}