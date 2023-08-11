using Morchul.Utility.Events;
using Morchul.Utility.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Morchul.Utility
{
    public class LoadHandler : MonoBehaviour
    {
        [SerializeField]
        [Scene]
        private string loadingScene;

        [Header("Manager")]
        [SerializeField]
        private SaveLoadManager manager;

        [Header("Events")]
        [SerializeField]
        private GameEvent OnLoadFinished;

        public void Load(string targetScene, int progress)
        {
            Time.timeScale = 0;
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(loadingScene).completed += loadingSceneReady;

            void loadingSceneReady(AsyncOperation _)
            {
                SceneManager.UnloadSceneAsync(currentScene);

                SceneManager.LoadSceneAsync(targetScene).completed += targetSceneReady;
            }

            void targetSceneReady(AsyncOperation _)
            {
                manager.Load(progress);

                SceneManager.UnloadSceneAsync(loadingScene).completed += loadingFinished;
            }

            void loadingFinished(AsyncOperation _)
            {
                Time.timeScale = 1;
                OnLoadFinished.RaiseEvent();
            }
        }
    }
}

