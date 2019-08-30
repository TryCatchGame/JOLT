using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using MyBox;

namespace GameUtility {

    [RequireComponent(typeof(Image))]
    public class FadableImage : MonoBehaviour {

        internal delegate void OnFadeCompleted();

        [Separator("Auto Assign")]
        [SerializeField, AutoProperty]
        private Image imageToFade;

        [Separator("Default Properties")]
        [SerializeField, Tooltip("The default speed to fade at"), PositiveValueOnly]
        private float defaultFadeSpeed;

        [SerializeField, Tooltip("The min and max alpha value it can go to"), MinMaxRange(0f, 1f)]
        private MinMaxFloat minMaxAlpha;

        private Coroutine fadeCoroutine;

        private void Awake() {
            if (imageToFade == null) {
                imageToFade = GetComponent<Image>();
            }
        }

        internal void FadeInImage(OnFadeCompleted callback = null) {
            FadeInImage(callback, defaultFadeSpeed);
        }

        internal void FadeInImage(float fadeSpeed) {
            FadeInImage(null, fadeSpeed);
        }

        internal void FadeInImage(OnFadeCompleted callback, float fadeSpeed) {
            StopFadeCoroutineIfExists();

            fadeCoroutine = StartCoroutine(FadeCoroutine(minMaxAlpha.Min, minMaxAlpha.Max, callback, fadeSpeed));
        }

        internal void FadeOutImage(OnFadeCompleted callback) {
            FadeOutImage(callback, defaultFadeSpeed);
        }

        internal void FadeOutImage(float fadeSpeed) {
            FadeOutImage(null, defaultFadeSpeed);
        }

        internal void FadeOutImage(OnFadeCompleted callback, float fadeSpeed) {
            StopFadeCoroutineIfExists();

            fadeCoroutine = StartCoroutine(FadeCoroutine(minMaxAlpha.Max, minMaxAlpha.Min, callback, fadeSpeed));
        }

        #region Utils

        private IEnumerator FadeCoroutine(float fromAlpha, float toAlpha, OnFadeCompleted callback, float fadeSpeed) {
            Color temp;

            float progress = 0f;
            while (progress < 1f) {
                LerpImageAlphaColor(fromAlpha, toAlpha, progress);

                yield return new WaitForEndOfFrame();
                progress += (Time.unscaledDeltaTime * fadeSpeed) / 100f;
            }
            // Ensures that the image is completed faded at the end.
            LerpImageAlphaColor(fromAlpha, toAlpha, 1);

            callback?.Invoke();

            yield return null;

            #region Local_Function

            void LerpImageAlphaColor(float a, float b, float t) {
                temp = imageToFade.color;
                temp.a = Mathf.Lerp(a, b, t);
                imageToFade.color = temp;
            }

            #endregion
        }

        private void StopFadeCoroutineIfExists() {
            if (fadeCoroutine != null) {
                StopCoroutine(fadeCoroutine);
            }
        }

        #endregion
    }
}
