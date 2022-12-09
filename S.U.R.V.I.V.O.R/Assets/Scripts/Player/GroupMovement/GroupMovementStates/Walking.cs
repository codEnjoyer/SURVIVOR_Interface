namespace Player.GroupMovement.GroupMovementStates
{
    public class Walking: GmState
    {
        public Walking(GroupMovementLogic gml, StateMachine sm) : base(gml, sm)
        {
        }

        public override void Enter()
        {
            gml.CreateWay();
        }

        public override void FixedUpdate()
        {
            gml.Move();
        }

        public override void Exit()
        {
            LocationInventory.Instance.LocationItemGrid.Clear();
        }
    }
}