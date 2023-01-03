
using Unity.VisualScripting;
using UnityEngine;

namespace Interface.InterfaceStates
{
    public class PlayerLayerActive: InterfaceState
    {
        private GameObject memory;
        public PlayerLayerActive(InterfaceController contr, StateMachine sm) : base(contr, sm)
        {
        }

        public override void Enter()
        {
            Selector.Instance.DeActivate();
            memory = contr.CurrentPlayerLayer;
            memory.SetActive(true);
            contr.MainInfoPanelLayer.SetActive(true);
            contr.CharactersButtonsLayer.SetActive(true);
        }

        public override void Exit()
        {
            Selector.Instance.Activate();
            contr.MainInfoPanelLayer.SetActive(false);
            memory.SetActive(false);
            contr.CharactersButtonsLayer.SetActive(false);
        }
    }
}