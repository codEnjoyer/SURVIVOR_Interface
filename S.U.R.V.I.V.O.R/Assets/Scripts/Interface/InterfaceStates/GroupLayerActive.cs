namespace Interface.InterfaceStates
{
    public class GroupLayerActive : InterfaceState
    {
        public GroupLayerActive(InterfaceController contr, StateMachine sm) : base(contr, sm)
        {
        }
        

        public override void Enter()
        {
            contr.InterfaceGroupLogicController.OnGroupLayerOpen.Invoke();
            Selector.Instance.DeActivate();
            contr.GroupInfoLayer.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(true);
        }

        public override void Exit()
        {
            Selector.Instance.Activate();
            contr.GroupInfoLayer.SetActive(false);
            contr.MainInfoPanelLayer.SetActive(false);
        }
    }
}