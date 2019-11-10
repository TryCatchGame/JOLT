using UnityEngine;

using GameManager;

namespace GameAnimationBehaviour.InstructionsPanel {
    public class StartGameOnStateExit : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            SpawnersManager.Instance.ActivateSpawners(true);

            Destroy(animator.gameObject);
        }
    }
}
