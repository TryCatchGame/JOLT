using UnityEngine;

using MyBox;

using GameInterface;
using GameEntity.Player;

namespace GameManager {

    public class GameOverManager : Singleton<GameOverManager> {

        internal delegate void OnGameOver();

        internal OnGameOver onGameOverEvent;

        #region TransitionDisplay_Struct

        [System.Serializable]
        private struct TransitionDisplayAnimation {
            [SerializeField, Tooltip("The animation controller for the transition display"), MustBeAssigned]
            private Animator animController;

            [SerializeField, Tooltip("The animation to play to start the transition"), MustBeAssigned]
            private AnimationClip startTransitionAnimation;

            [SerializeField, Tooltip("The loading circle to show that it is transitioning"), MustBeAssigned]
            private LoadingCircle loadingCircle;

            internal void StartTransition() {
                loadingCircle.ShowLoadingCircle();
                animController.Play(startTransitionAnimation.name);
            }
        }

        #endregion

        [SerializeField, Tooltip("The player's core"), MustBeAssigned]
        private Core playerCore;

        [Separator("Displays")]
        [SerializeField, Tooltip("The game over menu to show"), MustBeAssigned]
        private GameOverMenu gameOverMenu;

        [SerializeField, Tooltip("The animation to play when transitioning to main menu"), MustBeAssigned]
        private TransitionDisplayAnimation transitionDisplayAnimation;

        internal void TriggerGameOver() {
            onGameOverEvent?.Invoke();

            DisableIngameMovements();

            UpdateGameOverMenuScores();

            gameOverMenu.ShowGameOverMenu();

            #region Local_Function

            void DisableIngameMovements() {
                Time.timeScale = 0f;
                playerCore.DisablePaddleMovement();
            }

            void UpdateGameOverMenuScores() {
                GameScoreManager.Instance.UpdateGameOverMenuWithScores();
            }

            #endregion
        }

        /// <summary>
        /// Should be used as a callback on the continue button in the game over menu.
        /// </summary>
        public void TransitionToMainMenu() {
            transitionDisplayAnimation.StartTransition();
        }

        internal void SetGameOverMenuScoreTexts(int currentScore, int highScore, bool newHighScore = false) {
            gameOverMenu.SetScoreTexts(currentScore, highScore, newHighScore);
        }
    }
}
