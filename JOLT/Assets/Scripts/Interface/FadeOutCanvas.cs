using UnityEngine;

using MyBox;

namespace GameInterface {

    /// <summary>
    /// Attach to canvas that has a fade out animation;
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class FadeOutCanvas : MonoBehaviour {
        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private Animator animController;

        [Separator("Fade out")]
        [SerializeField, Tooltip("The animation to fade out to"), MustBeAssigned]
        private AnimationClip fadeOutAnimation;

        private void Awake() {
            if (animController == null) {
                animController = GetComponent<Animator>();
            }   
        }

        public void FadeOut() {
            animController.Play(fadeOutAnimation.name);
        }
    }
}
