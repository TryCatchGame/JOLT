using UnityEngine;

using MyBox;

namespace GameEntity.Enemy {
    /// <summary>
    /// Used to help to check if an enemy is touching the borders of the screen/camera.
    /// </summary>
    [RequireComponent(typeof(Enemy), typeof(SpriteRenderer))]
    public class BorderCollisionChecker : MonoBehaviour {

        [AutoProperty, SerializeField]
        private SpriteRenderer spriteRenderer;

        private static Camera mainCameraRef;

        /// <summary>
        /// Radius of this enemy (To get the offset when checking collision)
        /// </summary>
        public float Radius {
            get; private set;
        }

        private void Awake() {
            if (mainCameraRef == null) {
                mainCameraRef = Camera.main;
            }

            Radius = spriteRenderer.size.magnitude / 2f;
        }

        internal bool IsCollidingWithScreenBorder() {
            Vector3 currentOffset = (transform.position).normalized * Radius;

            Vector2 pos = mainCameraRef.WorldToViewportPoint(transform.position + currentOffset);

            if (pos.x < 0.0) { return true; }
            if (1.0 < pos.x) { return true; }
            if (pos.y < 0.0) { return true; }
            if (1.0 < pos.y) { return true; }

            return false;
        }


    }
}
