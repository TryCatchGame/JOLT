using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyBox;
namespace GameManager {
    public class SettingsMenuManager : MonoBehaviour {
        #region SettingsMenuButtons_Struct
        [System.Serializable]
        private struct SettingsMenuButton {
        }
        #endregion

        #region SettingsMenuAnimator_Struct
        [System.Serializable]
        private struct SettingsMenuAnimator {
            [SerializeField, Tooltip("The animator for the settings menu"), MustBeAssigned]
            private Animator animController;

            [SerializeField, Tooltip("The animation to show the settings menu"), MustBeAssigned]
            private AnimationClip showSettingsMenuAnimation;

            [SerializeField, Tooltip("The animation to hide the settings menu"), MustBeAssigned]
            private AnimationClip hideSettingsMenuAnimation;

            internal void PlayShowAnimation() {
                animController.Play(showSettingsMenuAnimation.name);
            }

            internal void PlayHideAnimation() {
                animController.Play(hideSettingsMenuAnimation.name);
            }
        }
        #endregion

        #region SettingsMenu_Struct
        [System.Serializable]
        private struct SettingsMenu {
            [SerializeField, Tooltip("The respective animation for the settings menu"), MustBeAssigned]
            private SettingsMenuAnimator animator;

            internal void PlayHideSettingsAnimation() {
                animator.PlayHideAnimation();
            }

            internal void PlayShowSettingsAnimation() {
                animator.PlayShowAnimation();
            }
        }
        #endregion
    }
}
