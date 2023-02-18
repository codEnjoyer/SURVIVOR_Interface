using Model;
using Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Interface.Menu.Pause
{
    public class ToMainMenuButton: MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            GlobalMapController.Data = GlobalMapController.Instance.CreateData();
            SceneTransition.LoadScene(SceneName.MainMenu);
        }
    }
}