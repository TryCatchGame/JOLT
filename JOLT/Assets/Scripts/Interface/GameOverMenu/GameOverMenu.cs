using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameAnimationBehaviour;

namespace GameInterface {
    [RequireComponent(typeof(Animator))]
    public class GameOverMenu : MonoBehaviour {

        #region ContinueButton_Struct

        [System.Serializable]
        private struct ContinueButton {
            [SerializeField, Tooltip("The button"), MustBeAssigned]
            private Button button;

            [SerializeField, Tooltip("The animator controller for this button"), MustBeAssigned]
            private Animator buttonAnimator;

            public void Enable() {
                button.interactable = true;
                buttonAnimator.enabled = true;
            }
        }

        #endregion

        #region ShowMenuAnimation_Struct

        [System.Serializable]
        private struct ShowMenuAnimationState {
            [SerializeField, Tooltip("What animation to play to show this game over menu"), MustBeAssigned]
            private AnimationClip showMenuAnimation;

            [SerializeField, Tooltip("The behaviour for this animation state"), MustBeAssigned]
            private TriggerEventOnStateExitBehaviour eventOnStateExitBehaviour;

            public AnimationClip ShowMenuAnimation { get => showMenuAnimation; }
            public TriggerEventOnStateExitBehaviour EventOnStateExitBehaviour { get => eventOnStateExitBehaviour; }
            public string AnimationStateName { get => showMenuAnimation.name; }
        }

        #endregion

        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private Animator animController;

        [Separator("Animations")]
        [SerializeField, Tooltip("What animation to play to show this game over menu"), MustBeAssigned]
        private ShowMenuAnimationState showMenuAnimationState;

        [Separator("Resume button")]
        [SerializeField, Tooltip("The continue button in this game over menu"), MustBeAssigned]
        private ContinueButton continueButton;

        private GameObject[] childObjects;

        private void Awake() {
            if (animController == null) {
                animController = GetComponent<Animator>();
            }

            SetChildObjects();
            showMenuAnimationState.EventOnStateExitBehaviour.onAnimationStateExitEvent += EnableContinueButton;

            #region Local_Function

            void SetChildObjects() {
                childObjects = new GameObject[transform.childCount];

                for (int i = 0; i < childObjects.Length; ++i) {
                    childObjects[i] = transform.GetChild(i).gameObject;
                }
            }

            #endregion
        }

        public void ShowGameOverMenu() {
            SetActiveChildObjects(true);

            animController.Play(showMenuAnimationState.AnimationStateName);
        }

        #region Utils

        private void EnableContinueButton() {
            continueButton.Enable();
        }

        private void SetActiveChildObjects(bool activate) {
            foreach (var childObject in childObjects) {
                childObject.SetActive(activate);
            }
        }

        #endregion
    }
}
