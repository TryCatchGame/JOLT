using UnityEngine;

using MyBox;

using GameEntity.Collectables;

namespace GameManager {
    public class CollectableSpawner : Spawner {
        [Separator("Collectable Spawner Properties")]
        [SerializeField, Tooltip("Collectables to spawn"), MustBeAssigned]
        private Collectable[] collectablesToSpawn;

        protected override void SpawnObjectAtPosition(Vector2 position) {

            SpawnRandomCollectable();

            #region Local_Function

            void SpawnRandomCollectable() {
                Collectable newCollectable = Instantiate(GetCollectableToSpawn());

                newCollectable.transform.position = position;

                newCollectable.SetTargetCore(playerCore);
            }

            Collectable GetCollectableToSpawn() {
                return collectablesToSpawn.GetRandom<Collectable>();
            }

            #endregion
        }
    }
}
