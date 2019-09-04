using UnityEngine;
using TMPro;
using MyBox;
namespace GameInterface.InGame {
    public class CurrencyMenu : MonoBehaviour {
        // NOTE: Possible refactor into a 'CurrencyText' struct?
        [Separator("Visual displays")]
        [SerializeField, Tooltip("The currency text for the in game menu"), MustBeAssigned]
        private TextMeshProUGUI currencyText;

        [SerializeField, Tooltip("The animator for the currency text"), MustBeAssigned]
        private Animator currencyTextAnimator;

        [SerializeField, Tooltip("The animation to play when the currency is changed"), MustBeAssigned]
        private AnimationClip currencyChangedAnimation;

        void Awake() {
            currencyText.text = GlobalProperties.TotalGemCount_Local.ToString();
        }

        internal void UpdateGemCountText(int gem) {
            currencyText.text = gem.ToString();

            currencyTextAnimator.Play(currencyChangedAnimation.name, -1, 0f);
        }
    }
}

