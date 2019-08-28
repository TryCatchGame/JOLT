using UnityEngine;

namespace GameEntity.Enemy {
    /// <summary>
    /// Used to help to check if an enemy is touching the borders of the screen/camera.
    /// </summary>
    [RequireComponent(typeof(Enemy))]
    public class BorderCollisionChecker : MonoBehaviour {

        private Camera mainCameraRef;

        private void Awake() {
            mainCameraRef = Camera.main;
        }

        internal bool IsCollidingWithScreenBorder() {
            Vector3 pos = mainCameraRef.WorldToViewportPoint(transform.position);

            if (pos.x < 0.0) { return true; }
            if (1.0 < pos.x) { return true; }
            if (pos.y < 0.0) { return true; }
            if (1.0 < pos.y) { return true; }

            return false;
        }


    }
}
