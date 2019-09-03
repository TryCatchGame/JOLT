using UnityEngine;
using UnityEngine.UI;

using MyBox;
namespace GameManager {
    public class SettingsMenuManager : Singleton<SettingsMenuManager> {
        #region SettingsMenuButtons_Struct
        [System.Serializable]
        private struct SettingsMenuButtons {
            [SerializeField, Tooltip("The button that is used to show the settings menu"), MustBeAssigned]
            private Button showSettingsButton;

            [SerializeField, Tooltip("The buttons in the settings menu"), MustBeAssigned]
            private Button[] settingMenuButtons;

            internal void SetSettingMenuButtonsInteractivity(bool interactable) {
                foreach (Button button in settingMenuButtons) {
                    button.interactable = interactable;
                }
            }
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

            [SerializeField, Tooltip("The respective buttons for the settings menu"), MustBeAssigned]
            private SettingsMenuButtons buttons;

            internal void PlayHideSettingsAnimation() {
                animator.PlayHideAnimation();
            }

            internal void PlayShowSettingsAnimation() {
                animator.PlayShowAnimation();
            }

            internal void SetButtonsInteractivity(bool interactable) {
                buttons.SetSettingMenuButtonsInteractivity(interactable); ;
            }
        }
        #endregion

        [SerializeField, Tooltip("The setting menu in the scene"), MustBeAssigned]
        private SettingsMenu settingsMenu;

        private bool showingSettingsMenu;

        private void Awake() {
            showingSettingsMenu = false;
        }

        public void EnableSettingsButtonInteractivity() {
            // See 'GameAnimationBehaviour.SettingsMenu.EnableSettingsButtonOnStateExit'
            settingsMenu.SetButtonsInteractivity(true);
        }

        public void ToggleSettingsMenu() {
            if (showingSettingsMenu) {
                HideSettingsMenu();
            } else {
                ShowSettingsMenu();
            }

            showingSettingsMenu = !showingSettingsMenu;
        }

        #region Utils
        private void ShowSettingsMenu() {
            settingsMenu.PlayShowSettingsAnimation();
        }

        private void HideSettingsMenu() {
            settingsMenu.SetButtonsInteractivity(false);
            settingsMenu.PlayHideSettingsAnimation();
        }
        #endregion
    }
}
