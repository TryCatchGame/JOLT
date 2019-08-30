using UnityEngine.SceneManagement;

using MyBox;

namespace GameManager {
    public class SceneTransitionManager : Singleton<SceneTransitionManager> {
        internal void TransitionToScene(SceneReference sceneReference) {
            TransitionToScene(sceneReference.SceneName);
        }

        internal void TransitionToScene(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }

        internal void TransitionToSceneWithAsync(SceneReference sceneReference) {
            TransitionToSceneWithAsync(sceneReference.SceneName);
        }

        internal void TransitionToSceneWithAsync(string sceneName) {
            SceneManager.LoadSceneAsync(sceneName);
        }

    }

}