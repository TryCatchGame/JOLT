using System.Collections;
using UnityEngine;

using MyBox;

namespace GameUtility {
    public class CameraShake : Singleton<CameraShake> {
        [Separator("Shake properties")]
        [SerializeField, Tooltip("How long should the shake last for?"), PositiveValueOnly]
        private float shakeDuration;

        [SerializeField, Tooltip("How strong is the shake?"), PositiveValueOnly]
        private float shakeAmount;

        private Vector3 startingPosition;

        public void TriggerShake() {
            startingPosition = transform.localPosition;
            StartCoroutine(Shake());
        }

        private IEnumerator Shake() {
            float shakeTimer = shakeDuration;
            while (shakeTimer > 0) {
                ShakeCameraRandomly();
                yield return new WaitForEndOfFrame();
            }
            transform.localPosition = startingPosition;

            #region Local_Function
            void ShakeCameraRandomly() {
                transform.localPosition = startingPosition + Random.insideUnitSphere * shakeAmount;

                shakeTimer -= Time.unscaledDeltaTime;
            }
            #endregion
        }
    }
}
