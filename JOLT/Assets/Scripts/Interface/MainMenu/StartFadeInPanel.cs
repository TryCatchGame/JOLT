using UnityEngine;
using UnityEngine.EventSystems;

using MyBox;

using GameUtility;
namespace GameInterface.MainMenu {
    /// <summary>
    /// Used to fade in at the start for the main menu.
    /// </summary>
    [RequireComponent(typeof(FadableImage))]
    public class StartFadeInPanel : MonoBehaviour {
        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private FadableImage fadablePanel;

        private EventSystem currentEventSystem;

        private void Awake() {
            if (fadablePanel == null) {
                fadablePanel = GetComponent<FadableImage>();
            }
            currentEventSystem = EventSystem.current;
        }

        private void Start() {
            currentEventSystem.enabled = false;
            TapToPlayInteraction.Instance.CanTransition = false;

            fadablePanel.FadeOutImage(EnableEventSystemAndDestroySelf);
        }

        private void EnableEventSystemAndDestroySelf() {
            currentEventSystem.enabled = true;
            TapToPlayInteraction.Instance.CanTransition = true;
            // Not needed anymore
            Destroy(gameObject);
        }
    }
}
