using UnityEngine;
using UnityEngine.EventSystems;

using MyBox;

using GameManager;
namespace GameInterface.MainMenu {
    /// <summary>
    /// Handles the 'tap anywhere to play' interaction for the player.
    /// </summary>
    public class TapToPlayInteraction : Singleton<TapToPlayInteraction> {
        internal delegate void OnTappedToPlay();

        internal OnTappedToPlay onTappedToPlayEvent;

        [SerializeField, Tooltip("The respective canvas to fade out when the player taps to play."), MustBeAssigned]
        private FadeOutCanvas[] canvasesToFadeOut;

        [SerializeField, Tooltip("The loading circle to show when transitioning"), MustBeAssigned]
        private LoadingCircle loadingCircle;

        private bool isEditor;

        private EventSystem currentEventSystem;

        /// <summary>
        /// True if the player is allowed to tap anywhere to transition to the next scene.
        /// </summary>
        internal bool CanTransition { get; set; }

        private void Awake() {
            isEditor = Application.isEditor;
            currentEventSystem = EventSystem.current;
            CanTransition = true;
        }

        private void Update() {
            if (!CanTransition) { return; }

            if (TappedOnNonInterface()) {
                if (!TryHideSettingsMenuIfShowing()) {
                    CanTransition = false;
                    DoFadeOutTransitionEffect();
                    onTappedToPlayEvent?.Invoke();
                }
            }

            #region Local_Function
            bool TryHideSettingsMenuIfShowing() {
                if (SettingsMenuManager.Instance.ShowingSettingsMenu) {
                    SettingsMenuManager.Instance.ToggleSettingsMenu();
                    return true;
                }
                return false;
            }

            void DoFadeOutTransitionEffect() {
                loadingCircle.ShowLoadingCircle();

                foreach (var canvasToFadeOut in canvasesToFadeOut) {
                    canvasToFadeOut.FadeOut();
                }
                // One of the canvas's animation has been attached with
                // a script 'GameAnimationBehaviour.TransitionToSceneOnStateExitBehaviour'.
            }

            bool TappedOnNonInterface() {
                if (HasTapped()) {
                    if (!PointerOverAnyInterface()) {
                        return true;
                    }
                }
                return false;
            }

            bool HasTapped() {
                if (isEditor) {
                    return Input.GetKeyDown(KeyCode.Mouse0);
                } else {
                    if (Input.touchCount > 0) {
                        if (Input.GetTouch(0).phase == TouchPhase.Began) {
                            return true;
                        }
                    }
                    return false;
                }
            }

            bool PointerOverAnyInterface() {
                if (isEditor) {
                    return currentEventSystem.IsPointerOverGameObject();
                } else {
                    return currentEventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
                }
            }
            #endregion
        }

        public void DisallowTransition() {
            CanTransition = false;
        }

        internal void AllowTransition() {
            CanTransition = true;
        }
    }
}
