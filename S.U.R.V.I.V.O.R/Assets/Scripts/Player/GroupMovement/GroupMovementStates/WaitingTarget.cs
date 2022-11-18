using UnityEngine;

namespace Player.GroupMovement.GroupMovementStates
{
    public class WaitingTarget : GmState
    {
        public WaitingTarget(GroupMovementLogic gml, StateMachine sm) : base(gml, sm)
        {
        }
    
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                stateMachine.ChangeState(gml.Sleeping);
            else
                gml.DrawPath();
        }
    }
}