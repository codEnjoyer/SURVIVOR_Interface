public class CharacterPanelActive : InterfaceState
{
    public CharacterPanelActive(InterfaceController contr, StateMachine sm)
        : base(contr, sm)
    {
    }

    public override void Enter()
    {
        // if (sm.CurrentState == this)
        //    sm.ChangeState(contr.nothingActive);
        // contr.CharactersButtonsLayer.SetActive(true);
    }

    public override void Exit()
    {
        // contr.CharactersButtonsLayer.SetActive(false);
    }
}