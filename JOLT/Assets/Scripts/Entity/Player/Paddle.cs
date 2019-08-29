using UnityEngine;

using MyBox;

using GameEntity.Collectables;

namespace GameEntity.Player {

    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(SpriteRenderer))]
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

        private void Awake() {
            if (paddleRenderer == null) {
                paddleRenderer = GetComponent<SpriteRenderer>();
            }
            if (paddleCollider == null) {
                paddleCollider = GetComponent<Collider2D>();
            }

            currentRotationDirection = PaddleRotationDirection.NONE;
        }

        private void Update() {
            transform.RotateAround(targetCore.transform.position, Vector3.forward, GetCurrentRotationSpeed());

            #region Local_Function

            float GetCurrentRotationSpeed() {
                return paddleRotationSpeed * Time.deltaTime * ((int)currentRotationDirection);
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
            paddleRenderer.enabled = enable;
            paddleCollider.enabled = enable;
        }

        internal void SetPaddleCollisionTags(PlayerCollisionTags collisionTags) {
            paddleCollisionTags = collisionTags;
        }

        #endregion
    }
}
