using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class GroupMovementLogic : MonoBehaviour
    {
        public Button GroupMoveButton;
        public Group ChosenGroup;

        public void Awake()
        {
            GroupMoveButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            ChosenGroup.CurrentStage = Group.Stage.WaitingTarget;
        }
    }
}
