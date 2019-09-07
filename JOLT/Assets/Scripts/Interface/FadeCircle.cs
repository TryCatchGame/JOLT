using UnityEngine;

using MyBox;
namespace GameInterface {
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class FadeCircle : MonoBehaviour {
        #region FadeAnimation_STRUCT
        [System.Serializable]
        private struct FadeAnimation {
            [SerializeField, Tooltip("The animation to play to fade in the circle"), MustBeAssigned]
            private AnimationClip fadeInAnimation;

            [SerializeField, Tooltip("The animation to play to fade out the circle"), MustBeAssigned]
            private AnimationClip fadeOutAnimation;

            internal AnimationClip FadeInAnimation { get => fadeInAnimation; }
            internal AnimationClip FadeOutAnimation { get => fadeOutAnimation; }
        }
        #endregion

        private enum PlacementPoint {
            TOP_LEFT,
            TOP_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_RIGHT,
            MIDDLE_LEFT,
            MIDDLE_RIGHT
        }
        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private Animator animController;

        [Separator("Animations")]
        [SerializeField, Tooltip("The animation to play to fade the circle"), MustBeAssigned]
        private FadeAnimation fadeAnimation;

        [Separator()]
        [SerializeField, Tooltip("Where to place this fade circle at the start"), MustBeAssigned]
        private PlacementPoint startPlacementPoint;

        private void Awake() {
            PlaceFadeCircleAtPlacementPosition();

            #region Local_Function
            void PlaceFadeCircleAtPlacementPosition() {
                var placementPosition = Camera.main.ViewportToWorldPoint(PlacementPointToViewport(startPlacementPoint));
                placementPosition.z = 0f;
                transform.position = placementPosition;
            }
            #endregion
        }

        public void FadeInCircle() {
            animController.Play(fadeAnimation.FadeInAnimation.name);
        }

        public void FadeOutCircle() {
            animController.Play(fadeAnimation.FadeOutAnimation.name);
        }

        #region Utils
        private static Vector2 PlacementPointToViewport(PlacementPoint placementPoint) {
            switch (placementPoint) {
                case PlacementPoint.BOTTOM_LEFT:
                    return new Vector2(0, 0);
                case PlacementPoint.TOP_RIGHT:
                    return new Vector2(1, 1);
                case PlacementPoint.BOTTOM_RIGHT:
                    return new Vector2(1, 0);
                case PlacementPoint.TOP_LEFT:
                    return new Vector2(0, 1);
                case PlacementPoint.MIDDLE_LEFT:
                    return new Vector2(0, 0.5f);
                case PlacementPoint.MIDDLE_RIGHT:
                    return new Vector2(1, 0.5f);
            }
            return Vector2.zero;
        }
        #endregion
    }
}
