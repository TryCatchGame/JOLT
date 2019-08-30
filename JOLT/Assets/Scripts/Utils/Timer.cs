namespace GameUtility {

    /// <summary>
    /// A very simple timer that relies on the game update to run.
    /// </summary>
    internal class Timer {

        internal delegate void OnTimerInterval();

        internal OnTimerInterval onTimerIntervalEvent;

        private float timer;

        /// <summary>
        /// True if the timer will auto-reset itself once the interval is reached
        /// </summary>
        internal bool AutoReset { get; set; }

        /// <summary>
        /// How long to wait for before the timer interval is reached.
        /// </summary>
        internal float Duration { get; private set; }

        internal bool IsActive { get; private set; }

        #region Constructor

        internal Timer(float duration, bool autoReset = true, bool activeOnStart = true) {
            onTimerIntervalEvent = delegate { };
            Duration = duration;
            AutoReset = autoReset;

            timer = 0f;
            IsActive = activeOnStart;
        }

        internal Timer(float duration, OnTimerInterval onTimerInterval, bool autoReset = true, bool activeOnStart = true) {
            onTimerIntervalEvent = onTimerInterval;
            AutoReset = autoReset;
            Duration = duration;

            timer = 0f;
            IsActive = activeOnStart;
        }

        #endregion

        internal void Update(float deltaTime) {
            if (!IsActive) { return; }

            timer += deltaTime;

            if (timer >= Duration) {
                onTimerIntervalEvent?.Invoke();

                if (AutoReset) {
                    ResetTimer();
                } else {
                    IsActive = false;
                }
            }
        }

        #region Utils

        internal void ResetTimer() {
            timer = 0f;
            IsActive = true;
        }

        internal void ResetTimer(float newDuration) {
            timer = 0f;
            Duration = newDuration;
            IsActive = true;
        }

        internal void StopTimer() {
            IsActive = false;
        }

        #endregion
    }
}
