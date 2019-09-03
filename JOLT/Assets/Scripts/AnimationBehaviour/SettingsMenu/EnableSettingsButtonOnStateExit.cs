using UnityEngine;

using GameManager;
namespace GameAnimationBehaviour.SettingsMenu {
    public class EnableSettingsButtonOnStateExit : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            SettingsMenuManager.Instance.EnableSettingsButtonInteractivity();
        }
    }
}