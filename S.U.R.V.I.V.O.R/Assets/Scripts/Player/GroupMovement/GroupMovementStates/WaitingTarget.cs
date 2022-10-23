using Graph_and_Map;
using Player;
using Unity.VisualScripting;

public class WaitingTarget : GmState
{
    public WaitingTarget(GroupMovementLogic gml, StateMachine sm) : base(gml, sm)
    {
    }
    
    public override void Update()
    {
        gml.DrawPath();
    }
    
}