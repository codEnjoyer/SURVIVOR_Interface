
namespace Interface.InterfaceStates
{
    public class NothingActive: InterfaceState
    {
        public NothingActive(InterfaceController contr, StateMachine sm) : base(contr, sm)
        {
        }

        public override void Enter()
        {
            contr.MainInfoPanelLayer.SetActive(true);
            contr.GroupButtonsLayer.SetActive(true);
        }

        public override void Exit()
        {
            contr.MainInfoPanelLayer.SetActive(false);
            contr.GroupButtonsLayer.SetActive(false);
        }
    }
}