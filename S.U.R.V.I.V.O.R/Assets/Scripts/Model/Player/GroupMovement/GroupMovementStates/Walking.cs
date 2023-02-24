using UnityEngine;

namespace Model.Player.GroupMovement.GroupMovementStates
{
    public class Walking: GmState
    {
        public Walking(GroupMovementLogic gml, StateMachine sm) : base(gml, sm)
        {
        }

        public override void Enter()
        {
            gml.CreateWay();
            if (gml.WayLength == 0)
                stateMachine.ChangeState(gml.Sleeping);
        }

        public override void FixedUpdate()
        {
            gml.Move();
        }

        public override void Exit()
        {
            LocationInventory.Instance.LocationInventoryGrid.Clear();
        }
    }
}