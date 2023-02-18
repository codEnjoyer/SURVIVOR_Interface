using Model;
using UnityEngine;

namespace Interface.Menu.Pause
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;
        private GlobalMapController globalMapController;

        private void Awake()
        {
            globalMapController = GlobalMapController.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (globalMapController.OnPause)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
        
        public void Resume()
        {
            pauseMenu.gameObject.SetActive(false);
            globalMapController.Resume();
        }
        public void Pause()
        {
            pauseMenu.gameObject.SetActive(true);
            globalMapController.Pause();
        }
    }
}
