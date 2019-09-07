using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameInterface;
using GameInterface.MainMenu;
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

            [Separator()]
            [SerializeField, Tooltip("The respective buttons for the settings menu"), MustBeAssigned]
            private SettingsMenuButtons buttons;

            [Separator()]
            [SerializeField, Tooltip("The fade out canvas for this settings menu"), MustBeAssigned]
            private FadeOutCanvas fadeOutCanvas;

            internal void PlayHideSettingsAnimation() {
                animator.PlayHideAnimation();
            }

            internal void PlayShowSettingsAnimation() {
                animator.PlayShowAnimation();
            }

            internal void SetButtonsInteractivity(bool interactable) {
                buttons.SetSettingMenuButtonsInteractivity(interactable); ;
            }

            internal void PlayFadeOutAnimation() {
                fadeOutCanvas.FadeOut();
            }
        }
        #endregion

        [SerializeField, Tooltip("The setting menu in the scene"), MustBeAssigned]
        private SettingsMenu settingsMenu;

        internal bool ShowingSettingsMenu { get; private set; }

        private void Awake() {
            ShowingSettingsMenu = false;
            TapToPlayInteraction.Instance.onTappedToPlayEvent += FadeOutSettingsMenu;
        }

        public void EnableSettingsButtonInteractivity() {
            // See 'GameAnimationBehaviour.SettingsMenu.EnableSettingsButtonOnStateExit'
            settingsMenu.SetButtonsInteractivity(true);
        }

        public void ToggleSettingsMenu() {
            if (ShowingSettingsMenu) {
                HideSettingsMenu();
            } else {
                ShowSettingsMenu();
            }
            ShowingSettingsMenu = !ShowingSettingsMenu;
        }

        #region Utils
        private void ShowSettingsMenu() {
            settingsMenu.PlayShowSettingsAnimation();
        }

        private void HideSettingsMenu() {
            settingsMenu.SetButtonsInteractivity(false);
            settingsMenu.PlayHideSettingsAnimation();
        }

        private void FadeOutSettingsMenu() {
            settingsMenu.PlayFadeOutAnimation();
        }
        #endregion
    }
}
