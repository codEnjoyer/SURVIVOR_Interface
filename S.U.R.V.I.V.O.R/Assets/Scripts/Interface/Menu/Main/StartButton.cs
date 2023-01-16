using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Interface.Menu.Main
{
    public class StartButton: MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            SceneManager.LoadScene((int) SceneName.MainScene);
        }
    }
}