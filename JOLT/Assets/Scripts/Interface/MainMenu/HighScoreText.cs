using UnityEngine;
using UnityEngine.UI;

using MyBox;

namespace GameInterface.MainMenu {
    /// <summary>
    /// Used to just update the high score text in the main menu on start.
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class HighScoreText : MonoBehaviour {
        [SerializeField, AutoProperty]
        private Text text;

        private void Awake() {
            if (text == null) {
                text = GetComponent<Text>();
            }

            text.text = $"BEST: {GlobalProperties.HighScore_Local}";

            // This component is not needed anymore.
            Destroy(this);
        }
    }
}