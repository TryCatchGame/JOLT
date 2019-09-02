using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameEntity.Player;
namespace GameManager {
    [RequireComponent(typeof(Animator))]
    public class GamePauseManager : Singleton<GamePauseManager> {
        #region ResumeButton_Struct
        [System.Serializable]
        private struct ResumeButton {
            [SerializeField, Tooltip("The button itself"), MustBeAssigned]
            private Button resumeButton;

            [SerializeField, Tooltip("The animator component for the button"), MustBeAssigned]
            private Animator buttonAnimator;

            internal void SetActive(bool activate) {
                resumeButton.gameObject.SetActive(activate);
            }

            internal void SetAnimatorEnabled(bool enable) {
                buttonAnimator.enabled = enable;
            }
        }
        #endregion

        #region PauseMenuAnimator_Struct
        [System.Serializable]
        private struct PauseMenuAnimator {
            [SerializeField, Tooltip("The animator controller for the pause menu")]
            private Animator animController;

            [SerializeField, Tooltip("The animation to play when the game is paused"), MustBeAssigned]
            private AnimationClip pauseAnimation;

            internal void PlayAnimation() {
                animController.Play(pauseAnimation.name);
            }

            internal void EnableAnimator(bool enable) {
                animController.enabled = enable;
            }
        }
        #endregion

        #region PauseMenu_Struct
        [System.Serializable]
        private struct PauseMenu {
            [SerializeField, Tooltip("The background panel for this pause menu"), MustBeAssigned]
            private GameObject backgroundPanel;

            [SerializeField, Tooltip("The resume button for this pause menu"), MustBeAssigned]
            private ResumeButton resumeButton;

            [SerializeField, Tooltip("The animator and animation displays for this menu"), MustBeAssigned]
            private PauseMenuAnimator pauseMenuAnimator;

            internal void EnableResumeButtonAnimator(bool enable) {
                resumeButton.SetAnimatorEnabled(enable);
            }

            internal void EnablePauseMenuAnimator(bool enable) {
                pauseMenuAnimator.EnableAnimator(enable);
            }

            internal void SetActive(bool activate) {
                resumeButton.SetActive(activate);
                backgroundPanel.SetActive(activate);
            }

            internal void PlayPauseAnimation() {
                pauseMenuAnimator.PlayAnimation();
            }
        }
        #endregion

        [Separator("Display")]
        [SerializeField, Tooltip("The pause menu itself"), MustBeAssigned]
        private PauseMenu pauseMenu;

        [SerializeField, Tooltip("The button used to pause the game"), MustBeAssigned]
        private Button pauseButton;

        [Separator("Pause properties")]
        [SerializeField, Tooltip("The player's core"), MustBeAssigned]
        private Core playerCore;

        private void Awake() {
            HideInterfaceOnGameOver();

            #region Local_Function
            void HideInterfaceOnGameOver() {
                GameOverManager.Instance.onGameOverEvent += HideAllPauseRelatedInterface;
            }
            #endregion
        }

        public void PauseGame() {
            pauseButton.gameObject.SetActive(false);

            DisableEntitiesMovement();
            DisplayPauseMenu();

            #region Local_Function

            void DisableEntitiesMovement() {
                playerCore.DisablePaddleMovement();
                Time.timeScale = 0f;
            }

            void DisplayPauseMenu() {
                pauseMenu.EnablePauseMenuAnimator(true);
                pauseMenu.SetActive(true);
                pauseMenu.PlayPauseAnimation();
            }
            #endregion
        }

        public void ResumeGame() {
            pauseButton.gameObject.SetActive(true);

            HidePauseMenu();
            EnableEntitiesMovement();

            #region Local_Function

            void EnableEntitiesMovement() {
                playerCore.EnablePaddleMovement();
                Time.timeScale = 1f;
            }

            void HidePauseMenu() {
                pauseMenu.SetActive(false);
                pauseMenu.EnableResumeButtonAnimator(true);
            }
            #endregion
        }

        #region Utils
        internal void EnableResumeButtonAnimator() {
            // Check 'GameAnimationBehaviour.PauseMenu.EnableResumeButtonAnimatorOnStateExit.cs'
            pauseMenu.EnablePauseMenuAnimator(false);
            pauseMenu.EnableResumeButtonAnimator(true);
        }

        private void HideAllPauseRelatedInterface() {
            pauseMenu.SetActive(false);
            pauseButton.gameObject.SetActive(false);
        }
        #endregion
    }
}
