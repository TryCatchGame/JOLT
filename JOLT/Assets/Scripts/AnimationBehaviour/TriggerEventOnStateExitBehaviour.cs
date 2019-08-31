using UnityEngine;

namespace GameAnimationBehaviour {
    public class TriggerEventOnStateExitBehaviour : StateMachineBehaviour {

        public delegate void OnAnimationStateExit();

        public OnAnimationStateExit onAnimationStateExitEvent;

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            onAnimationStateExitEvent?.Invoke();
        }
    }
}
