using UnityEngine;

using MyBox;

namespace GameManager {
    public class SpawnersManager : Singleton<SpawnersManager> {
        [SerializeField, Tooltip("All the spawners in the scene"), MustBeAssigned]
        private Spawner[] sceneSpawners;

        internal void ActivateSpawners(bool enabled) {
            foreach (var spawner in sceneSpawners) {
                spawner.IsSpawning = enabled;
            }
        }
    }
}
