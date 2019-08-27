using UnityEngine;

using MyBox;

namespace GameEntity.Player.Controls {

    [RequireComponent(typeof(Paddle))]
    public class PaddleController : MonoBehaviour {

        [Separator("Auto Assign Properties")]
        [SerializeField, AutoProperty]
        private Paddle paddle;

        [Separator("DEBUG")]
        [SerializeField, Tooltip("The key to press to rotate the paddle clockwise (Editor only)"), SearchableEnum]
        private KeyCode rotateClockwiseKey;

        [SerializeField, Tooltip("The key to press to rotate the paddle anti-clockwise (Editor only)"), SearchableEnum]
        private KeyCode rotateAntiClockwiseKey;

        private bool isEditor;

        private void Awake() {
            isEditor = Application.isEditor;
        }

        private void Update() {
            if (isEditor) {
                if (Input.GetKey(rotateClockwiseKey)) {
                    RotateClockwise();
                } else if (Input.GetKey(rotateAntiClockwiseKey)) {
                    RotateAntiClockwise();
                }
            }
        }

        public void RotateClockwise() {
            paddle.RotatePaddleClockwise();
        }

        public void RotateAntiClockwise() {
            paddle.RotatePaddleAntiClockwise();
        }
    }
}
