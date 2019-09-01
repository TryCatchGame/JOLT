using UnityEngine;
using UnityEngine.UI;

using MyBox;

namespace GameInterface {
    [RequireComponent(typeof(Animator))]
    public class GameOverMenu : MonoBehaviour {

        #region ContinueButton_Struct

        [System.Serializable]
        private struct ContinueButton {
            [SerializeField, Tooltip("The button"), MustBeAssigned]
            private Button button;

            [SerializeField, Tooltip("The animator controller for this button"), MustBeAssigned]
            private Animator buttonAnimator;

            public void Enable() {
                button.interactable = true;
                buttonAnimator.enabled = true;
            }
        }

        #endregion

        #region ScoreTexts_Struct
        [System.Serializable]
        private struct ScoreTexts {
            [SerializeField, Tooltip("The high score text"), MustBeAssigned]
            private Text highScoreText;

            [SerializeField, Tooltip("The current score text for the current play's run"), MustBeAssigned]
            private Text currentScoreText;

            public Text HighScoreText { get => highScoreText; }
            public Text CurrentScoreText { get => currentScoreText; }

            internal void SetHighScoreText(int highScore, bool newHighScore = false) {
                if (newHighScore) {
                    highScoreText.text = $"NEW BEST: {highScore.ToString()}";
                } else {
                    highScoreText.text = $"BEST: {highScore.ToString()}";
                }
            }
        }

        #endregion

        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private Animator animController;

        [Separator("Animations")]
        [SerializeField, Tooltip("What animation to play to show this game over menu"), MustBeAssigned]
        private AnimationClip showMenuAnimation;

        [Separator("Resume button")]
        [SerializeField, Tooltip("The continue button in this game over menu"), MustBeAssigned]
        private ContinueButton continueButton;

        [Separator("Visual displays")]
        [SerializeField, Tooltip("The respective score texts in the game over menu"), MustBeAssigned]
        private ScoreTexts scoreTexts;

        private GameObject[] childObjects;

        private void Awake() {
            if (animController == null) {
                animController = GetComponent<Animator>();
            }

            SetChildObjects();

            #region Local_Function

            void SetChildObjects() {
                childObjects = new GameObject[transform.childCount];

                for (int i = 0; i < childObjects.Length; ++i) {
                    childObjects[i] = transform.GetChild(i).gameObject;
                }
            }

            #endregion
        }

        public void ShowGameOverMenu() {
            SetActiveChildObjects(true);

            animController.Play(showMenuAnimation.name);
        }

        internal void SetScoreTexts(int currentScore, int highScore, bool newHighScore = false) {
            scoreTexts.CurrentScoreText.text = currentScore.ToString();

            scoreTexts.SetHighScoreText(highScore, newHighScore);
        }

        #region Utils

        internal void EnableContinueButton() {
            continueButton.Enable();
        }

        private void SetActiveChildObjects(bool activate) {
            foreach (var childObject in childObjects) {
                childObject.SetActive(activate);
            }
        }

        #endregion
    }
}
