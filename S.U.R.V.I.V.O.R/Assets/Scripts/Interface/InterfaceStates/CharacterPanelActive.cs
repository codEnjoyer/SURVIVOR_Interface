namespace Interface.InterfaceStates
{
    public class CharacterPanelActive : InterfaceState
    {
        public CharacterPanelActive(InterfaceController contr, StateMachine sm) : base(contr, sm)
        {
        }

        public override void Enter()
        {
            contr.CharactersButtonsLayer.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(true);
            contr.GroupButtonsLayer.SetActive(true);
            CameraController.Instance.isActive = true; 
            MinimapController.Instance.isActive = true;
        }

        public override void Exit()
        {
            contr.CharactersButtonsLayer.SetActive(false);
            contr.MainInfoPanelLayer.SetActive(false);
            contr.GroupButtonsLayer.SetActive(false);
            CameraController.Instance.isActive = false; 
            MinimapController.Instance.isActive = false;
        }
    }
}