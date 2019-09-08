using UnityEngine;

using MyBox;

using GameEntity.Player;

namespace GameEntity.Collectables {
    [RequireTag("Collectables")]
    [RequireLayer("Collectables")]
	public abstract class Collectable : MonoBehaviour {
        #region CollectableEffects_STRUCT
        [System.Serializable]
        private struct CollectableEffects {
            [SerializeField, Tooltip("The prefab for the effect to show when the collectable is collected"), MustBeAssigned]
            private GameObject collectedEffect;

            [SerializeField, Tooltip("The prefab for the effect to show when the collectable is destroyed"), MustBeAssigned]
            private GameObject destroyedEffect;

            internal GameObject CreateCollectedEffect() {
                return Instantiate(collectedEffect);
            }

            internal GameObject CreateDestroyedEffect() {
                return Instantiate(destroyedEffect);
            }
        }
        #endregion

        [SerializeField, Tooltip("The move speed of this collectable"), PositiveValueOnly]
		private float moveSpeed;

		[SerializeField, Tooltip("True if this gives a gem to the player")]
		private bool givesGem;

		[ConditionalField(nameof(givesGem), false, true), SerializeField, Tooltip("How many gems this gives the player"), PositiveValueOnly]
		private int gemCount;

        [Separator()]
        [SerializeField, Tooltip("The respective effects to play for the collectable"), MustBeAssigned]
        private CollectableEffects collectableEffects;

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

			if(givesGem) {
				AddGemToPlayer();
			}

			CreateCollectedEffect();
			Destroy(gameObject);

			#region Local_Function
			void CreateCollectedEffect() {
                var collectedEffect = collectableEffects.CreateCollectedEffect();
                collectedEffect.transform.position = transform.position;
            }

			void AddGemToPlayer() {
				GameManager.CurrencyManager.Instance.ModifyGemValue(gemCount);
			}
			#endregion
		}

		protected abstract void OnCollectedEvent();

		internal void Destroy() {
			CreateDestroyedEffect();
			Destroy(gameObject);

			#region Local_Function
			void CreateDestroyedEffect() {
                var destroyedEffect = collectableEffects.CreateDestroyedEffect();
                destroyedEffect.transform.position = transform.position;
            }
			#endregion
		}

		internal void SetTargetCore(Core target) {
			PlayerCore = target;
		}

		#endregion
	}
}
