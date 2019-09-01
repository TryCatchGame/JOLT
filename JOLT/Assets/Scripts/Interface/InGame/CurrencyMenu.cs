using UnityEngine;
using TMPro;
using MyBox;

namespace GameInterface.InGame {
    public class CurrencyMenu : MonoBehaviour {
        [Separator("Visual displays")]
        [SerializeField, Tooltip("The currency text for the in game menu"), MustBeAssigned]
        private TextMeshProUGUI currencyText;

        void Awake() {
            currencyText.text = GlobalProperties.TotalGemCount_Local.ToString();
        }

        internal void UpdateGemCountText(int gem) {
            currencyText.text = gem.ToString();
        }
    }
}

