using UnityEngine;

using MyBox;

using GameUtility;
using GameEntity.Enemy;
using GameEntity.Player;

namespace GameManager {

    public class EnemySpawner : Singleton<EnemySpawner> {

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

        [Separator("Enemy Spawning Properties")]
        [SerializeField, Tooltip("The enemies this spawner can spawn."), MustBeAssigned]
        private Enemy[] enemiesToSpawn;

        [SerializeField, Tooltip("The random interval between each enemy spawn"), MustBeAssigned]
        private SpawnInterval enemySpawnInterval;

        [SerializeField, Tooltip("The core that the enemy will target after it spawns"), MustBeAssigned]
        private Core enemyTargetCore;

        private Timer spawnTimer;

        private void Awake() {
            spawnTimer = new Timer(enemySpawnInterval.GenerateWaitingTime(), SpawnEnemyAndResetTimerDuration);
        }

        // Update is called once per frame
        void Update() {
            spawnTimer.Update(Time.deltaTime);
        }

        #region Local_Function

        private void SpawnEnemyAndResetTimerDuration() {

            SpawnEnemy();
            ResetTimerDuration();

            #region Local_Function

            void SpawnEnemy() {
                Enemy newEnemy = Instantiate(GetEnemyToSpawn());

                // TODO: Generate a location to spawn;

                newEnemy.SetPlayerTarget(enemyTargetCore);
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
