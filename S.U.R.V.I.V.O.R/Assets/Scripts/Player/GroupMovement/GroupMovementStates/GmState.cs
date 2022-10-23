
using Player;

public abstract class GmState: State
{
    protected GroupMovementLogic gml;
    public GmState(GroupMovementLogic gml, StateMachine sm)
    {
        this.gml = gml;
        stateMachine = sm;
    }

}