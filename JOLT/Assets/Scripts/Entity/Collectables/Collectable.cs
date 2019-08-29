using UnityEngine;

using MyBox;

using GameEntity.Player;

namespace GameEntity.Collectables {
    public abstract class Collectable : MonoBehaviour {
        [SerializeField, Tooltip("The move speed of this collectable"), PositiveValueOnly]
        private float moveSpeed;

        private Core playerCore;

        private void Update() {
            transform.position = Vector2.MoveTowards(transform.position, playerCore.transform.position, moveSpeed * Time.deltaTime);
        }

        #region Utils

        /// <summary>
        /// Called by the player's paddle to collect this collectable.
        /// </summary>
        internal void Collect() {
            OnCollectedEvent();

            Destroy(gameObject);
        }

        protected abstract void OnCollectedEvent();

        internal void SetTargetCore(Core target) {
            playerCore = target;
        }

        #endregion
    }
}
