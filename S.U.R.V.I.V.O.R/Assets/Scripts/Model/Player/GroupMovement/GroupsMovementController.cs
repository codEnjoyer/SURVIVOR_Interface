using UnityEngine;
using UnityEngine.UI;

namespace Model.Player.GroupMovement
{
    public class GroupsMovementController : MonoBehaviour
    {
        public Button GroupMoveButton;

        public void Awake()
        {
            GroupMoveButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Game.Instance.ChosenGroup.GetComponent<GroupMovementLogic>().PreparingToMove();
        }
    }
}
