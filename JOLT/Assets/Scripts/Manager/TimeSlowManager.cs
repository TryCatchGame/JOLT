using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using MyBox;

using GameUtility;

namespace GameManager {

    public class TimeSlowManager : Singleton<TimeSlowManager> {

        #region BackgroundEffect_Struct

        [System.Serializable]
        private struct BackgroundEffect {
            [SerializeField, Tooltip("The background effect to show when time is slowed"), MustBeAssigned]
            private FadableImage timeSlowedImage;

            [SerializeField, Tooltip("How fast the fading in/out of the background effect will occur"), PositiveValueOnly]
            private float fadeSpeed;

            internal void FadeInEffect() {
                timeSlowedImage.FadeInImage(fadeSpeed);
            }

            internal void FadeOutEffect() {
                timeSlowedImage.FadeOutImage(fadeSpeed);
            }

            internal void FadeOutEffect(FadableImage.OnFadeCompleted callback) {
                timeSlowedImage.FadeOutImage(callback, fadeSpeed);
            }
        }

        #endregion

        #region CounterDisplay_Struct

        /// <summary>
        /// Helps to count how many times can the time slow be invoked.
        /// </summary>
        [System.Serializable]
        private struct CounterDisplay {
            [SerializeField, Tooltip("The counting image to display to the player"), MustBeAssigned]
            private Image countingImage;

            [SerializeField, Tooltip("The respective sprites to change to based on the counted number. (Index sensitive)"), MustBeAssigned]
            private Sprite[] countingSprites;

            /// <summary>
            /// Up to how many it can count.
            /// </summary>
            internal int MaxCount {
                get => countingSprites.Length;
            }

            internal void SetCurrentCountDisplay(int count) {
                if (count <= 0) {
                    HideImageIfActive(countingImage);
                } else {
                    ShowImageIfInactive(countingImage);
                    countingImage.sprite = countingSprites[count - 1];
                }

                #region Local_Function

                void HideImageIfActive(Image image) {
                    if (image.gameObject.activeInHierarchy) {
                        image.gameObject.SetActive(false);
                    }
                }

                void ShowImageIfInactive(Image image) {
                    if (!image.gameObject.activeInHierarchy) {
                        image.gameObject.SetActive(true);
                    }
                }

                #endregion
            }
        }

        #endregion

        #region TimeSlowData_Struct
        [System.Serializable]
        private struct TimeSlowProperty {
            [SerializeField, Tooltip("The timescale it will slow to."), PositiveValueOnly]
            private float slowTimeScale;

            [SerializeField, Tooltip("How fast it will slow down time"), PositiveValueOnly]
            private float slowDownTimeSpeed;

            [SerializeField, Tooltip("How long does each time slow lasts for"), PositiveValueOnly]
            private float timeSlowDuration;

            internal float SlowTimeScale { get => slowTimeScale; }
            internal float SlowDownTimeSpeed { get => slowDownTimeSpeed; }
            internal float TimeSlowDuration { get => timeSlowDuration; }
        }

        #endregion

        [Separator("Display properties")]
        [SerializeField, Tooltip("The background effect to display when time is slowed"), MustBeAssigned]
        private BackgroundEffect timeSlowedBackgroundEffect;

        [SerializeField, Tooltip("The display to show how many times the player can do a time slow"), MustBeAssigned]
        private CounterDisplay timeSlowCounterDisplay;

        [SerializeField, Tooltip("The button to use to trigger time slows"), MustBeAssigned]
        private Button timeSlowTriggerButton;

        [Separator("Time slow properties")]
        [SerializeField, Tooltip("The property to use when slowing down time"), MustBeAssigned]
        private TimeSlowProperty timeSlowProperty;

        private Timer timeSlowTimer;

        internal int TimeSlowUsableCount { get; private set; }

        private void Awake() {
            TimeSlowUsableCount = 0;
            timeSlowTimer = new Timer(timeSlowProperty.TimeSlowDuration, StopTimeSlow, false, false);
        }

        private void StopTimeSlow() {
            SpeedBackTime();
            timeSlowedBackgroundEffect.FadeOutEffect(EnableTimeSlowButton);

            #region Local_Function

            void SpeedBackTime() {
                SlowlyLerpTimeScaleBySpeed(timeSlowProperty.SlowTimeScale, 1f, timeSlowProperty.SlowDownTimeSpeed);
            }

            #endregion
        }

        internal void TriggerTimeSlow() {
            --TimeSlowUsableCount;
            if (TimeSlowUsableCount <= 0) {
                DisableTimeSlowButtonIfEnabled();
            }

            SetTimeSlowButtonInteractivity(false);
            SlowDownTime();
            timeSlowedBackgroundEffect.FadeInEffect();

            #region Local_Function

            void SlowDownTime() {
                SlowlyLerpTimeScaleBySpeed(1f, timeSlowProperty.SlowTimeScale, timeSlowProperty.SlowDownTimeSpeed);
            }

            void DisableTimeSlowButtonIfEnabled() {
                if (timeSlowTriggerButton.gameObject.activeInHierarchy) {
                    timeSlowTriggerButton.gameObject.SetActive(false);
                }
            }

            #endregion
        }

        #region Utils

        internal void AddTimeSlowUsableCount() {
            ++TimeSlowUsableCount;

            EnableTimeSlowButtonIfDisabled();

            #region Local_Function

            void EnableTimeSlowButtonIfDisabled() {
                if (!timeSlowTriggerButton.gameObject.activeInHierarchy) {
                    timeSlowTriggerButton.gameObject.SetActive(true);
                }

            }

            #endregion
        }

        private void EnableTimeSlowButton() {
            SetTimeSlowButtonInteractivity(true);
        }

        private void SetTimeSlowButtonInteractivity(bool interactable) {
            timeSlowTriggerButton.interactable = interactable;
        }

        private IEnumerator SlowlyLerpTimeScaleBySpeed(float from, float to, float lerpSpeed) {
            float progress = 0f;
            while (progress < 1f) {
                Time.timeScale = Mathf.Lerp(from, to, progress);

                yield return new WaitForEndOfFrame();
                progress += Time.unscaledDeltaTime * lerpSpeed;
            }

            Time.timeScale = to;
        }

        #endregion
    }
}
