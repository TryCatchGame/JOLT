using UnityEngine;

namespace GameEntity.Enemy {
    /// <summary>
    /// Used to help to check if an enemy is touching the borders of the screen/camera.
    /// </summary>
    [RequireComponent(typeof(Enemy))]
    public class BorderCollisionChecker : MonoBehaviour {

        private static Camera mainCameraRef;

        private void Awake() {
            if (mainCameraRef == null) {
                mainCameraRef = Camera.main;
            }
        }

        internal bool IsCollidingWithScreenBorder() {
            Vector2 pos = mainCameraRef.WorldToViewportPoint(transform.position);

            Debug.Log(pos);

            if (pos.x < 0.0) { return true; }
            if (1.0 < pos.x) { return true; }
            if (pos.y < 0.0) { return true; }
            if (1.0 < pos.y) { return true; }

            return false;
        }


    }
}
