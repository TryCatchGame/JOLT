using UnityEngine.SceneManagement;
using UnityEngine;

using MyBox;

namespace GameManager {
    public class SceneTransitionManager : Singleton<SceneTransitionManager> {
        public void TransitionToScene(SceneReference sceneReference) {
            TransitionToScene(sceneReference.SceneName);
        }

        public void TransitionToScene(string sceneName) {
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }

        public void TransitionToSceneWithAsync(SceneReference sceneReference) {
            TransitionToSceneWithAsync(sceneReference.SceneName);
        }

        public void TransitionToSceneWithAsync(string sceneName) {
            var loadingSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            loadingSceneAsync.completed += delegate { Time.timeScale = 1f; };
        }

    }

}