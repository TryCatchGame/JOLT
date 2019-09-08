using UnityEngine;
using GameManager;
namespace GameAnimationBehaviour.ShopMenu {
    public class EnableShopEventSystemOnStateExit : StateMachineBehaviour {
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            ShopManager.Instance.EnableEventSystem();
        }
    }
}