using UnityEngine;

using MyBox;

using GameEntity.Collectables;

namespace GameEntity.Player {

    [RequireComponent(typeof(Collider2D), typeof(Animator), typeof(SpriteRenderer))]
    public class Paddle : MonoBehaviour {

        private enum PaddleRotationDirection {
            ANTI_CLOCKWISE = -1,
            NONE = 0,
            CLOCKWISE = 1
        }

        [Separator("Auto Assign")]
        [SerializeField, AutoProperty]
        private SpriteRenderer paddleRenderer;

        [SerializeField, AutoProperty]
        private Collider2D paddleCollider;

        [SerializeField, AutoProperty]
        private Animator animController;

        [Separator("Paddle animation")]
        [SerializeField, Tooltip("The animation to play when the paddle is enabled"), MustBeAssigned]
        private AnimationClip paddleEnabledAnimation;

        [Separator("Core movements")]
        [SerializeField, Tooltip("The core which this paddle protects"), MustBeAssigned]
        private Core targetCore;

        [SerializeField, Tooltip("How fast this paddle rotates"), PositiveValueOnly]
        private float paddleRotationSpeed;

        private PlayerCollisionTags paddleCollisionTags;

        private PaddleRotationDirection currentRotationDirection;

        internal bool IsEnabled {
            get => paddleRenderer.enabled && paddleCollider.enabled;
        }

        internal bool CanMove { get; set; }

        private void Awake() {
            AssignNullAutoProperties();

            currentRotationDirection = PaddleRotationDirection.NONE;
            CanMove = true;

            if (!(this is ShieldPaddle)) {
                EnablePaddle(true);
            }

            #region Local_Function

            void AssignNullAutoProperties() {
                if (paddleRenderer == null) {
                    paddleRenderer = GetComponent<SpriteRenderer>();
                }
                if (paddleCollider == null) {
                    paddleCollider = GetComponent<Collider2D>();
                }
                if (animController == null) {
                    animController = GetComponent<Animator>();
                }
            }
            #endregion
        }

        private void Update() {
            if (!CanMove) { return; }

            transform.RotateAround(targetCore.transform.position, Vector3.forward, GetCurrentRotationSpeed());

            #region Local_Function

            float GetCurrentRotationSpeed() {
                return paddleRotationSpeed * Time.unscaledDeltaTime * ((int)currentRotationDirection);
            }

            #endregion
        }

        private void OnCollisionEnter2D(Collision2D collision) {

            if (TryGetCollectableFromCollision(out Collectable collidedCollectable)) {
                collidedCollectable.Collect();
            } else if (collision.gameObject.CompareTag(paddleCollisionTags.EnemyTag)) {
                if (this is ShieldPaddle) {
                    (this as ShieldPaddle).DecreaseShieldLife();
                }
            }

            #region Local_Function

            bool TryGetCollectableFromCollision(out Collectable collectable) {
                if (collision.gameObject.CompareTag(paddleCollisionTags.CollectableTag)) {
                    collectable = collision.gameObject.GetComponent<Collectable>();
                } else {
                    collectable = null;
                }

                return collectable != null;
            }

            #endregion
        }

        #region Utils

        internal void RotatePaddleAntiClockwise() {
            currentRotationDirection = PaddleRotationDirection.ANTI_CLOCKWISE;
        }

        internal void RotatePaddleClockwise() {
            currentRotationDirection = PaddleRotationDirection.CLOCKWISE;
        }

        internal void StopPaddleRotation() {
            currentRotationDirection = PaddleRotationDirection.NONE;
        }

        internal void EnablePaddle(bool enable) {
            if (enable) {
                animController.Play(paddleEnabledAnimation.name, -1, 0f);
            }

            paddleRenderer.enabled = enable;
            paddleCollider.enabled = enable;
        }

        internal void SetPaddleCollisionTags(PlayerCollisionTags collisionTags) {
            paddleCollisionTags = collisionTags;
        }

        #endregion
    }
}
