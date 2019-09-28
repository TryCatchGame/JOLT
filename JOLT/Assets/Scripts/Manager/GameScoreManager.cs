using UnityEngine;

using TMPro;

using MyBox;
namespace GameManager {
    public class GameScoreManager : Singleton<GameScoreManager> {
        #region GameScoreTexts_Struct
        [System.Serializable]
        private struct GameScoreText {
            [SerializeField, Tooltip("The text"), MustBeAssigned]
            private TextMeshProUGUI text;

            [SerializeField, Tooltip("The animator for the text"), MustBeAssigned]
            private Animator animator;

            [SerializeField, Tooltip("The animation to play when the text is changed"), MustBeAssigned]
            private AnimationClip textChangedAnimation;

            internal void ChangeText(string newContent) {
                text.text = newContent;
                animator.Play(textChangedAnimation.name, -1, 0f);
            }

            internal void SetActiveText(bool activate) {
                text.gameObject.SetActive(activate);
            }
        }
        #endregion

        #region GameScoreDisplay_Struct
        [System.Serializable]
        private struct GameScoreDisplay {
            [SerializeField, Tooltip("The text to show current score that the player has"), MustBeAssigned]
            private GameScoreText currentScoreText;

            [SerializeField, Tooltip("The text to show the highscore"), MustBeAssigned]
            private GameScoreText highScoreText;

            internal void SetScoreDisplaysText(string currentScoreDisplay, string highScoreDisplay = "") {
                currentScoreText.ChangeText(currentScoreDisplay);

                if (!string.IsNullOrEmpty(highScoreDisplay)) {
                    highScoreText.ChangeText(highScoreDisplay);
                }
            }

            internal void HideScoreTexts() {
                currentScoreText.SetActiveText(false);
                highScoreText.SetActiveText(false);
            }
        }
        #endregion
        [SerializeField, Tooltip("The respective display texts in game"), MustBeAssigned]
        private GameScoreDisplay gameScoreDisplay;

        internal int CurrentScoreCount { get; private set; }

        private int currentHighScore;

        private bool obtainedNewHighScore;

        private void Awake() {
            obtainedNewHighScore = false;
            CurrentScoreCount = 0;
            currentHighScore = GlobalProperties.HighScore_Local;

            gameScoreDisplay.SetScoreDisplaysText("0", $"BEST: {currentHighScore}");

            SaveScoreOnGameOver();
            HideIngameScoreDisplayOnGameOver();

            #region Local_Function

            void SaveScoreOnGameOver() {
                GameOverManager.Instance.onGameOverEvent += SaveCurrentHighScore;
                GameOverManager.Instance.onGameOverEvent += UpdateCurrentScoreCountToAchievements;

                GooglePlayServiceManager.Instance.UpdateLeaderboardWithScore(CurrentScoreCount);
            }

            void HideIngameScoreDisplayOnGameOver() {
                GameOverManager.Instance.onGameOverEvent += gameScoreDisplay.HideScoreTexts;
            }

            #endregion
        }

        private void UpdateCurrentScoreCountToAchievements() {
            GooglePlayServiceManager.Instance.UpdateScoreAchievements(CurrentScoreCount);
        }

        private void SaveCurrentHighScore() {
            GlobalProperties.HighScore_Local = currentHighScore;
        }

        internal void IncrementCurrentScore() {
            ++CurrentScoreCount;

            if (CurrentScoreCount > currentHighScore) {
                currentHighScore = CurrentScoreCount;
                gameScoreDisplay.SetScoreDisplaysText($"{CurrentScoreCount}", $"BEST: {currentHighScore}");
                obtainedNewHighScore = true;
            } else {
                gameScoreDisplay.SetScoreDisplaysText($"{CurrentScoreCount}");
            }
        }

        internal void UpdateGameOverMenuWithScores() {
            GameOverManager.Instance.SetGameOverMenuScoreTexts(CurrentScoreCount, currentHighScore, obtainedNewHighScore);
        }
    }
}