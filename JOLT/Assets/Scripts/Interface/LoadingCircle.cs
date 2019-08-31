using UnityEngine;

using MyBox;

namespace GameInterface {

    /// <summary>
    /// Attach to a loading circle to handle it's animation.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class LoadingCircle : MonoBehaviour {

        #region PlacementPosition_Enum

        private enum PlacementPosition {
            TOP_LEFT,
            TOP_RIGHT,
            BOTTOM_LEFT,
            BOTTOM_RIGHT
        }

        #endregion

        [Separator("Auto Property")]
        [SerializeField, AutoProperty]
        private Animator animController;

        [SerializeField, AutoProperty]
        private SpriteRenderer spriteRenderer;

        [Separator("Loading circle initalization")]
        [SerializeField, Tooltip("Where this loading circle should be placed at"), SearchableEnum]
        private PlacementPosition placementPosition;

        [Separator("Loading circle animation")]
        [SerializeField, Tooltip("The animation to play to start showing the loading circle"), MustBeAssigned]
        private AnimationClip showLoadingCircleAnimation;

        [SerializeField, Tooltip("The animation to play to hide the loading circle"), MustBeAssigned]
        private AnimationClip hideLoadingCircleAnimation;

        private void Awake() {
            if (animController == null) {
                animController = GetComponent<Animator>();
            }

            SetPosition();

            #region Local_Function

            void SetPosition() {
                Vector2 positionToSet = GetWorldPositionToSet() + GetOffsetByPlacementPosition();

                transform.position = positionToSet;
            }

            Vector2 GetOffsetByPlacementPosition() {
                Vector2 offset = spriteRenderer.size / 2f;

                switch (placementPosition) {
                    case PlacementPosition.BOTTOM_RIGHT:
                        offset.x *= -1f;
                        break;
                    case PlacementPosition.TOP_LEFT:
                        offset.y *= 1f;
                        break;
                    case PlacementPosition.TOP_RIGHT:
                        offset.x *= -1f;
                        offset.y *= 1f;
                        break;
                }

                return offset;
            }

            Vector2 GetWorldPositionToSet() {
                Vector2 viewportPosition = GetViewportByPlacementPosition();

                return Camera.main.ViewportToWorldPoint(viewportPosition);
            }

            Vector2 GetViewportByPlacementPosition() {
                switch (placementPosition) {
                    case PlacementPosition.BOTTOM_LEFT:
                        return Vector2.zero;
                    case PlacementPosition.BOTTOM_RIGHT:
                        return Vector2.right;
                    case PlacementPosition.TOP_LEFT:
                        return Vector2.up;
                    case PlacementPosition.TOP_RIGHT:
                        return Vector2.one;
                }

                return Vector2.zero;
            }

            #endregion
        }

        public void ShowLoadingCircle() {
            animController.Play(showLoadingCircleAnimation.name);
        }

        public void HideLoadingCircle() {
            animController.Play(hideLoadingCircleAnimation.name);
        }
    }
}