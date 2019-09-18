using UnityEngine;
using UnityEngine.UI;

using GameManager.Sound;

using MyBox;
namespace GameUtility {
    [RequireComponent(typeof(Button))]
    public class PlayButtonPressSoundOnClick : MonoBehaviour {
        [SerializeField, AutoProperty]
        private Button button;
        private void Awake() {
            if (button == null) {
                button = GetComponent<Button>();
            }

            button.onClick.AddListener(PlayButtonPressSound);
        }

        private void PlayButtonPressSound() {
            SoundManager.Instance.PlaySoundBySoundType(SoundType.BUTTON_PRESS);
        }
    }
}
