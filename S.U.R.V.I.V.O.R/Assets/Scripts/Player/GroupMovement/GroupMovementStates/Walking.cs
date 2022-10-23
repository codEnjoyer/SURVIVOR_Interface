using Player;
using UnityEngine;

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
        gml.ClearWay();
    }
}