using UnityEngine;

using GameUtility;

using MyBox;
using GameEntity.Player;

namespace GameManager {
    public abstract class Spawner : MonoBehaviour {
        #region SpawnInterval_STRUCT

        [System.Serializable]
        private struct SpawnInterval {
            [SerializeField, MinMaxRange(1f, float.MaxValue), Tooltip("The min and max waiting time before an object is generated.")]
            private MinMaxFloat minMaxSpawnInterval;

            internal float MaxWaitingTime { get => minMaxSpawnInterval.Max; }

            internal float MinWaitingTime { get => minMaxSpawnInterval.Min; }

            internal float GenerateWaitingTime() {
                return Random.Range(MinWaitingTime, MaxWaitingTime);
            }
        }

        #endregion

        [SerializeField, Tooltip("True if it can start spawn on start")]
        private bool startSpawningOnStart;

        [SerializeField, Tooltip("The random interval between each object spawns"), MustBeAssigned]
        private SpawnInterval spawnInterval;

        [SerializeField, Tooltip("The core for the player in the scene"), MustBeAssigned]
        protected Core playerCore;

        [SerializeField, Tooltip("How much extra distance to add when spawning away from the player"), PositiveValueOnly]
        private float additionalSpawnDistance;

        private Timer spawnTimer;

        private Vector2 screenSize;

        internal bool IsSpawning { get; set; }

        private void Awake() {
            spawnTimer = new Timer(spawnInterval.GenerateWaitingTime(), SpawnObjectAndResetTimerDuration);

            InitalizeScreenSize();

            IsSpawning = startSpawningOnStart;

            #region Local_Function

            void InitalizeScreenSize() {
                Camera cameraRef = Camera.main;

                screenSize = cameraRef.ViewportToWorldPoint(new Vector2(1, 1));
            }

            #endregion
        }

        private void Update() {
            if (!IsSpawning) {
                return;
            }

            spawnTimer.Update(Time.deltaTime);
        }

        #region Utils

        protected abstract void SpawnObjectAtPosition(Vector2 position);

        /// <summary>
        /// Called by the timer when the timer's interval is up.
        /// </summary>
        private void SpawnObjectAndResetTimerDuration() {

            SpawnObjectAtPosition(GeneratePositionToSpawn());
            ResetTimerDuration();

            #region Local_Function

            Vector2 GeneratePositionToSpawn() {
                Vector2 directionToSpawn = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
                return (directionToSpawn * screenSize) + (additionalSpawnDistance * directionToSpawn);
            }

            void ResetTimerDuration() {
                spawnTimer.ResetTimer(spawnInterval.GenerateWaitingTime());
            }

            #endregion

        }

        #endregion
    }
}
