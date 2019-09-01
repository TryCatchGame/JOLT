using UnityEngine;

using TMPro;

using MyBox;

namespace GameManager {
    public class GameScoreManager : Singleton<GameScoreManager> {
        #region GameScoreDisplay_Struct
        [System.Serializable]
        private struct GameScoreDisplay {
            [SerializeField, Tooltip("The text to show current score that the player has"), MustBeAssigned]
            private TextMeshProUGUI currentScoreText;

            [SerializeField, Tooltip("The text to show the highscore"), MustBeAssigned]
            private TextMeshProUGUI highScoreText;

            internal void SetScoreDisplaysText(string currentScoreDisplay, string highScoreDisplay = "") {
                currentScoreText.text = currentScoreDisplay;

                if (!string.IsNullOrEmpty(highScoreDisplay)) {
                    highScoreText.text = highScoreDisplay;
                }
            }

            internal void HideScoreTexts() {
                currentScoreText.gameObject.SetActive(false);
                highScoreText.gameObject.SetActive(false);
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
            }

            void HideIngameScoreDisplayOnGameOver() {
                GameOverManager.Instance.onGameOverEvent += gameScoreDisplay.HideScoreTexts;
            }

            #endregion
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