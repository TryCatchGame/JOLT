using UnityEngine;

using MyBox;

using GameUtility;
using GameEntity.Enemy;
using GameEntity.Player;

namespace GameManager {

    public class EnemySpawner : Singleton<EnemySpawner> {

        #region SpawnInterval_STRUCT

        [System.Serializable]
        private struct SpawnInterval {
            [SerializeField, MinMaxRange(1f, float.MaxValue), Tooltip("The min and max waiting time before an enemy is generated.")]
            private MinMaxFloat minMaxSpawnInterval;

            internal float MaxWaitingTime { get => minMaxSpawnInterval.Max;  }

            internal float MinWaitingTime { get => minMaxSpawnInterval.Min; }

            internal float GenerateWaitingTime() {
                return Random.Range(MinWaitingTime, MaxWaitingTime);
            }
        }

        #endregion

        [Separator("Enemy Spawning Properties")]
        [SerializeField, Tooltip("The enemies this spawner can spawn."), MustBeAssigned]
        private Enemy[] enemiesToSpawn;

        [SerializeField, Tooltip("The random interval between each enemy spawn"), MustBeAssigned]
        private SpawnInterval enemySpawnInterval;

        [SerializeField, Tooltip("The core that the enemy will target after it spawns"), MustBeAssigned]
        private Core enemyTargetCore;

        [SerializeField, Tooltip("How much extra distance to add when spawning away from the player"), PositiveValueOnly]
        private float additionalSpawnDistance;

        private Timer spawnTimer;

        private Vector2 screenSize;

        private void Awake() {
            spawnTimer = new Timer(enemySpawnInterval.GenerateWaitingTime(), SpawnRandomEnemyAndResetTimerDuration);

            InitalizeScreenSize();

            #region Local_Function

            void InitalizeScreenSize() {
                Camera cameraRef = Camera.main;

                screenSize = cameraRef.ViewportToWorldPoint(new Vector2(1, 1));
            }

            #endregion
        }

        void Update() {
            spawnTimer.Update(Time.deltaTime);
        }

        #region Utils

        /// <summary>
        /// Called by the timer when the timer's interval is up.
        /// </summary>
        private void SpawnRandomEnemyAndResetTimerDuration() {

            SpawnRandomEnemy();
            ResetTimerDuration();

            #region Local_Function

            void SpawnRandomEnemy() {
                Enemy newEnemy = Instantiate(GetEnemyToSpawn());

                newEnemy.transform.position = GeneratePositionToSpawn();

                newEnemy.SetPlayerTarget(enemyTargetCore);
            }

            Vector2 GeneratePositionToSpawn() {
                Vector2 directionToSpawn = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
                return (directionToSpawn * screenSize) + (additionalSpawnDistance * directionToSpawn);
            }

            Enemy GetEnemyToSpawn() {
                // TODO: Make spawning biased towards those that have not been spawned recently.

                return enemiesToSpawn.GetRandom<Enemy>();
            }

            void ResetTimerDuration() {
                spawnTimer.ResetTimer(enemySpawnInterval.GenerateWaitingTime());
            }

            #endregion

        }

        #endregion
    }
}
