using Model;
using UnityEngine;

namespace Interface.Menu.Pause
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private PauseMenu pauseMenu;
        private Game game;

        private void Awake()
        {
            game = Game.Instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (game.OnPause)
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
            game.Resume();
        }
        public void Pause()
        {
            pauseMenu.gameObject.SetActive(true);
            game.Pause();
        }
    }
}
