using Unity.VisualScripting;
using UnityEngine;

namespace Interface.InterfaceStates
{
    public class CharacterState : InterfaceState
    {
        private readonly GameObject playerLayerObj;

        public CharacterState(InterfaceController contr, StateMachine sm, PlayerLayerLogic obj)
            : base(contr, sm)
        {
            playerLayerObj = obj.gameObject;
        }

        public override void Enter()
        {
            stateMachine.DefaultState = contr.CharactersState;
            Selector.Instance.gameObject.SetActive(false);
            playerLayerObj.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(true);
            contr.CharactersButtonsLayer.SetActive(true);
        }

        public override void Exit()
        {
            Selector.Instance.gameObject.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(false);
            playerLayerObj.SetActive(false);
            contr.CharactersButtonsLayer.SetActive(false);
        }
    }
}