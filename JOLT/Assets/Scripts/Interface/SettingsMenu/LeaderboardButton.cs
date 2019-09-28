using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameManager;

namespace GameInterface.SettingsMenu {
    [RequireComponent(typeof(Button))]
    public class LeaderboardButton : MonoBehaviour {
        [AutoProperty, SerializeField]
        private Button button;

        private void Awake() {
            button.onClick.AddListener(ShowGooglePlayLeaderboard);
        }

        private void ShowGooglePlayLeaderboard() {
            GooglePlayServiceManager.Instance?.ShowLeaderboardInterface();
        }
    }
}
