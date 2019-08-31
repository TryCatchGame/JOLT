using UnityEngine;
using UnityEngine.EventSystems;

using MyBox;

using GameManager;

namespace GameInterface.MainMenu {
    /// <summary>
    /// Handles the 'tap anywhere to play' interaction for the player.
    /// </summary>
    public class TapToPlayInteraction : Singleton<TapToPlayInteraction> {
        [SerializeField, Tooltip("The respective canvas to fade out when the player taps to play."), MustBeAssigned]
        private FadeOutCanvas[] canvasesToFadeOut;

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

        // Update is called once per frame
        void Update() {
            if (!CanTransition) { return; }

            if (TappedOnNonInterface()) {
                DoFadeOutTransitionEffect();
            }

            #region Local_Function

            void DoFadeOutTransitionEffect() {
                CanTransition = false;

                foreach (var canvasToFadeOut in canvasesToFadeOut) {
                    canvasToFadeOut.FadeOut();
                }
                // One of the canvas's animation has been attached with
                // a script 'GameAnimationBehaviour.TransitionToSceneOnStateExitBehaviour'
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
                    return Input.touchCount > 0;
                }
            }

            bool PointerOverAnyInterface() {
                return currentEventSystem.IsPointerOverGameObject();
            }

            #endregion
        }
    }
}
