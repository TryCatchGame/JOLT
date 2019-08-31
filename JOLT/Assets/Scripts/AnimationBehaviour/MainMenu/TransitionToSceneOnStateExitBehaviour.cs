using UnityEngine;

using MyBox;

using GameManager;

namespace GameAnimationBehaviour {

    public class TransitionToSceneOnStateExitBehaviour : StateMachineBehaviour {

        [SerializeField, Tooltip("The scene to transition to after this main menu canvas fades out"), MustBeAssigned]
        private SceneReference sceneToTransitionTo;

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            SceneTransitionManager.Instance.TransitionToSceneWithAsync(sceneToTransitionTo);
        }
    }
}
