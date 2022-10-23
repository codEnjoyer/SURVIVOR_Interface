using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class GroupsMovementController : MonoBehaviour
    {
        public Button GroupMoveButton;
        public GroupMovementLogic ChosenGroup;

        public void Awake()
        {
            GroupMoveButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            ChosenGroup.OnSelected();
        }
    }
}
