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
        }

        public override void Exit()
        {
            contr.CharactersButtonsLayer.SetActive(false);
            contr.MainInfoPanelLayer.SetActive(false);
            contr.GroupButtonsLayer.SetActive(false);
        }
    }
}