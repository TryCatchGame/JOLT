using System.Collections;
using UnityEngine;

using GameInterface;

namespace GameAnimationBehaviour.GameOver {

    public class EnableContinueButtonOnStateExit : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            animator.gameObject.GetComponent<GameOverMenu>().EnableContinueButton();
        }
    }
}