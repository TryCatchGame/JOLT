using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameManager;

namespace GameInterface.SettingsMenu {
    [RequireComponent(typeof(Button))]
    public class AchievementButton : MonoBehaviour {
        [AutoProperty, SerializeField]
        private Button button;

        private void Awake() {
            button.onClick.AddListener(ShowGooglePlayAchievements);
        }

        private void ShowGooglePlayAchievements() {
            GooglePlayServiceManager.Instance?.ShowAchievementInterface();
        }
    }
}
