using UnityEngine;

using MyBox;

using GameEntity.Player;

namespace GameEntity.Collectables {
    public abstract class Collectable : MonoBehaviour {
        [SerializeField, Tooltip("The move speed of this collectable"), PositiveValueOnly]
        private float moveSpeed;

        protected Core PlayerCore { get; private set; }

        private void Update() {
            transform.position = Vector2.MoveTowards(transform.position, PlayerCore.transform.position, moveSpeed * Time.deltaTime);
        }

        #region Utils

        /// <summary>
        /// Called by the player's paddle to collect this collectable.
        /// </summary>
        internal void Collect() {
            OnCollectedEvent();

            CreateCollectedEffect();
            Destroy(gameObject);

            #region Local_Function

            void CreateCollectedEffect() {
                // TODO: Do effect when this is collected;
            }

            #endregion
        }

        protected abstract void OnCollectedEvent();

        internal void Destroy() {
            CreateDestroyedEffect();
            Destroy(gameObject);

            #region Local_Function

            void CreateDestroyedEffect() {
                // TODO: Create effect when this is destroyed. (Not collected)
            }

            #endregion
        }

        internal void SetTargetCore(Core target) {
            PlayerCore = target;
        }

        #endregion
    }
}
