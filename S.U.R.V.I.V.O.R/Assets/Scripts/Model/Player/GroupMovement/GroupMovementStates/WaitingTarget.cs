using UnityEngine;

namespace Model.Player.GroupMovement.GroupMovementStates
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
            
            if (Input.GetMouseButtonDown(0) &&
                Physics.Raycast(
                    Camera.main.ScreenPointToRay(Input.mousePosition),
                    out var hitInfo, 200f))
            {
                stateMachine.ChangeState(gml.Walking);
            }
        }
    }
}