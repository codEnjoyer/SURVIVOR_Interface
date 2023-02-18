using System;
using Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes
{
    public class SceneTransition : MonoBehaviour
    {
        public static SceneTransition Instance { get; private set; }

        public static bool shouldPlayOpeningAnimation = false;
            
        [SerializeField] private Text loadingPercent;
        [SerializeField] private Image loadingProgress;
        private Animator animator;
        private AsyncOperation asyncOperation;
        private SceneName sceneName;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                animator = GetComponent<Animator>();
            }
        }

        private void Start()
        {
            if (shouldPlayOpeningAnimation)
            {
                animator.SetTrigger("sceneOpening");
                shouldPlayOpeningAnimation = false;
            }
        }

        private void Update()
        {
            if (asyncOperation != null)
            {
                loadingPercent.text = $"{Math.Round(asyncOperation.progress * 100)}%";
                loadingProgress.fillAmount = asyncOperation.progress;
            }
        }

        public static void LoadScene(SceneName sceneName)
        {
            Instance.animator.SetTrigger("sceneClosing");
            Instance.sceneName = sceneName;
        }

        private void StartLoad()
        {
            shouldPlayOpeningAnimation = true;
            asyncOperation = SceneManager.LoadSceneAsync((int) sceneName);
        }
    }
}