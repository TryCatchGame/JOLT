using UnityEngine;

using GameManager;
namespace GameAnimationBehaviour.PauseMenu {
    public class EnableResumeButtonAnimatorOnStateExit : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            GamePauseManager.Instance.EnableResumeButtonAnimator();
        }
    }
}
