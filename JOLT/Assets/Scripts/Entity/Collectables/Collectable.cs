using UnityEngine;

using MyBox;

using GameEntity.Player;

namespace GameEntity.Collectables {
    public abstract class Collectable : MonoBehaviour {
        [SerializeField, Tooltip("The move speed of this collectable"), PositiveValueOnly]
        private float moveSpeed;

        [SerializeField, Tooltip("True if this gives a gem to the player")]
        private bool givesGem;

        [ConditionalField(nameof(givesGem), false, true),SerializeField, Tooltip("How many gems this gives the player"), PositiveValueOnly]
        private int gemCount;

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

            if (givesGem) {
                AddGemToPlayer();
            }

            CreateCollectedEffect();
            Destroy(gameObject);

            #region Local_Function

            void CreateCollectedEffect() {
                // TODO: Do effect when this is collected;
            }

            void AddGemToPlayer() {
                // TODO: Actually add gem to player.
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
