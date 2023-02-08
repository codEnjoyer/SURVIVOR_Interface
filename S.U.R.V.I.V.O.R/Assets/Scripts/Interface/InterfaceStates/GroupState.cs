namespace Interface.InterfaceStates
{
    public class GroupState : InterfaceState
    {
        public GroupState(InterfaceController contr, StateMachine sm) : base(contr, sm)
        {
        }
        

        public override void Enter()
        {
            stateMachine.DefaultState = stateMachine.PreviousState;
            Selector.Instance.gameObject.SetActive(false);
            contr.GroupInfoLayer.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(true);
        }

        public override void Exit()
        {
            Selector.Instance.gameObject.SetActive(true);
            contr.GroupInfoLayer.SetActive(false);
            contr.MainInfoPanelLayer.SetActive(false);
        }
    }
}