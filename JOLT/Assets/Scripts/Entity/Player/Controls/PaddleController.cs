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

        private float screenCenterPosiitonX;

        private void Awake() {
            isEditor = Application.isEditor;

            if (!isEditor) {
                screenCenterPosiitonX = Screen.width / 2f;
            }
        }

        private void Update() {
            UpdatePaddleRotation();
        }

        private void UpdatePaddleRotation() {
            if (isEditor) {
                RotatePaddleByKeyPress();
            } else {
                RotatePaddleByScreenPress();
            }

            #region Local_Function

            void RotatePaddleByScreenPress() {
                if (Input.touchCount >= 1) {
                    RotatePaddleByTouchPosition();
                } else {
                    StopRotation();
                }
            }

            void RotatePaddleByTouchPosition() {
                Vector2 screenPressPosition = Input.GetTouch(0).position;

                if (screenCenterPosiitonX < screenPressPosition.x) {
                    RotateClockwise();
                } else if (screenCenterPosiitonX > screenPressPosition.x) {
                    RotateAntiClockwise();
                }
            }

            void RotatePaddleByKeyPress() {
                if (Input.GetKey(rotateClockwiseKey)) {
                    RotateClockwise();
                } else if (Input.GetKey(rotateAntiClockwiseKey)) {
                    RotateAntiClockwise();
                } else {
                    StopRotation();
                }
            }

            #endregion
        }

        #region Utils

        public void StopRotation() {
            paddle.StopPaddleRotation();
        }

        public void RotateClockwise() {
            paddle.RotatePaddleClockwise();
        }

        public void RotateAntiClockwise() {
            paddle.RotatePaddleAntiClockwise();
        }

        #endregion
    }
}
