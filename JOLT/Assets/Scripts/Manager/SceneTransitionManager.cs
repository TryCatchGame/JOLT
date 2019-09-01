using UnityEngine.SceneManagement;
using UnityEngine;

using MyBox;

namespace GameManager {
    public class SceneTransitionManager : Singleton<SceneTransitionManager> {

        internal delegate void OnSceneTransitionInvoked();

        /// <summary>
        /// Invoked when a caller calls to transition to a scene.
        /// </summary>
        internal OnSceneTransitionInvoked onSceneTransitionInvokedEvent;

        public void TransitionToScene(SceneReference sceneReference) {
            TransitionToScene(sceneReference.SceneName);
        }

        public void TransitionToScene(string sceneName) {
            onSceneTransitionInvokedEvent?.Invoke();
            Time.timeScale = 1f;
            SceneManager.LoadScene(sceneName);
        }

        public void TransitionToSceneWithAsync(SceneReference sceneReference) {
            TransitionToSceneWithAsync(sceneReference.SceneName);
        }

        public void TransitionToSceneWithAsync(string sceneName) {
            onSceneTransitionInvokedEvent?.Invoke();
            var loadingSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            loadingSceneAsync.completed += delegate { Time.timeScale = 1f; };
        }

    }

}