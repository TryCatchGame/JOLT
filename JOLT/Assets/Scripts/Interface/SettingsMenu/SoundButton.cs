using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameManager.Sound;
namespace GameInterface.SettingsMenu {
    [RequireComponent(typeof(Image), typeof(Button))]
    public class SoundButton : MonoBehaviour {
        [AutoProperty, SerializeField]
        private Image image;

        [AutoProperty, SerializeField]
        private Button button;

        [Separator()]
        [SerializeField, Tooltip("Sprite to show when the sound is toggled on"), MustBeAssigned]
        private Sprite soundOnSprite;

        [SerializeField, Tooltip("Sprite to show when the sound is toggled off"), MustBeAssigned]
        private Sprite soundOffSprite;

        private void Awake() {
            SetCurrentImageSpriteBySoundStatus();

            button.onClick.AddListener(ToggleSoundStatus);
        }

        private void ToggleSoundStatus() {
            SoundManager.Instance.ToggleSoundStatus();
            SetCurrentImageSpriteBySoundStatus();
        }

        private void SetCurrentImageSpriteBySoundStatus() {
            if (GlobalProperties.SoundOn_Local) {
                image.sprite = soundOnSprite;
            } else {
                image.sprite = soundOffSprite;
            }
        }
    }
}
