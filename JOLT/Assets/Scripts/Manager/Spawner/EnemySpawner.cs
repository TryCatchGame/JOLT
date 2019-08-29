using UnityEngine;

using MyBox;

using GameEntity.Enemy;

namespace GameManager {

    public class EnemySpawner : Spawner {

        [Separator("Enemy Spawning Properties")]
        [SerializeField, Tooltip("The enemies this spawner can spawn."), MustBeAssigned]
        private Enemy[] enemiesToSpawn;

        protected override void SpawnObjectAtPosition(Vector2 position) {

            SpawnRandomEnemy();

            #region Local_Function

            void SpawnRandomEnemy() {
                Enemy newEnemy = Instantiate(GetEnemyToSpawn());

                newEnemy.transform.position = position;

                newEnemy.SetPlayerTarget(playerCore);
            }

            Enemy GetEnemyToSpawn() {
                // TODO: Make spawning biased towards those that have not been spawned recently.

                return enemiesToSpawn.GetRandom<Enemy>();
            }

            #endregion
        }
    }
}
