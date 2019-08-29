using UnityEngine;

using MyBox;

namespace GameEntity.Player {
    [System.Serializable]
    public struct PlayerCollisionTags {
        [SerializeField, Tooltip("The tag for the collectables"), Tag]
        private string collectableTag;

        [SerializeField, Tooltip("The tag for the enemies"), Tag]
        private string enemyTag;

        public string CollectableTag { get => collectableTag; }
        public string EnemyTag { get => enemyTag; }
    }
}
