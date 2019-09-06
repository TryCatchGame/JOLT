using UnityEngine;

using MyBox;

using GameEntity.Player;
using GameParticle.Enemy;

namespace GameEntity.Enemy {
	[RequireTag("Enemy")]
	[RequireLayer("Enemy")]
	[RequireComponent(typeof(Collider2D), typeof(BorderCollisionChecker))]
	public abstract class Enemy : MonoBehaviour {
		protected enum CollisionType {
			SCREEN_BORDER,
			PLAYER
		}

		[Separator("Auto-Assign")]
		[SerializeField, AutoProperty]
		private BorderCollisionChecker borderCollisionChecker;

		[Separator("Enemy Movement")]
		[SerializeField, Tooltip("The move speed of the enemy"), PositiveValueOnly]
		private float moveSpeed;

		[Separator("Wall Bounce properties")]
		[SerializeField, Tooltip("True if this enemy creates an effect when bouncing off the walls")]
		private bool effectOnWallBounce;

		[SerializeField, Tooltip("The effect it creates"), ConditionalField(nameof(effectOnWallBounce), false, true), MustBeAssigned]
		private EnemyBounceEffect wallBounceEffect;

		private string playerTag;

		protected Core PlayerTarget {
			get; private set;
		}

		protected Vector2 PlayerTargetPosition {
			get => PlayerTarget.transform.position;
		}

		/// <summary>
		/// Which direction to move to if this enemy is not moving to the player target.
		/// </summary>
		protected Vector2 FreeMoveDirection {
			get; private set;
		}

		protected bool IsMovingToPlayerTarget {
			get; private set;
		}

		protected float Radius {
			get => borderCollisionChecker.Radius;
		}

		private void Awake() {
			if(borderCollisionChecker == null) {
				borderCollisionChecker = GetComponent<BorderCollisionChecker>();
			}

			IsMovingToPlayerTarget = true;
		}

		void Update() {
			if(IsMovingToPlayerTarget) {
				UpdateMovementToPlayerTarget();
			} else {
				UpdateMovementByFreeMoveDirection();

				TargetPlayerIfOutOfBounds();
			}

			#region Local_Function

			void UpdateMovementToPlayerTarget() {
				transform.position = Vector2.MoveTowards(transform.position, PlayerTargetPosition, moveSpeed * Time.deltaTime);
			}

			void UpdateMovementByFreeMoveDirection() {
				transform.position += (Vector3)FreeMoveDirection * moveSpeed * Time.deltaTime;
			}

			void TargetPlayerIfOutOfBounds() {
				if(borderCollisionChecker.IsCollidingWithScreenBorder()) {
					IsMovingToPlayerTarget = true;
					OnCollisionEvent(CollisionType.SCREEN_BORDER);

					CreateWallCollisionEffect();
				}
			}

			void CreateWallCollisionEffect() {
				if(effectOnWallBounce) {
					var newWallBounceEffect = Instantiate(wallBounceEffect);
					newWallBounceEffect.transform.position = transform.position;
				}
			}

			#endregion
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			if(collision.gameObject.CompareTag(playerTag)) {
				OnCollisionEvent(CollisionType.PLAYER);

				TriggerCollisionDeath();
				CameraShake.Instance.TriggerShake();
			}

			#region Local_Function

			void TriggerCollisionDeath() {
				CreateDeathEffect();

				Destroy(gameObject);
			}

			void CreateDeathEffect() {
				// TODO: Some visual effects on death.
			}

			#endregion
		}

		protected abstract void OnCollisionEvent(CollisionType collisionType);

		#region Util

		internal void SetPlayerTarget(Core target) {
			PlayerTarget = target;
			playerTag = PlayerTarget.tag;
		}

		internal void SetFreeMoveDirection(Vector2 moveDirection, bool enableFreeMove = true) {
			FreeMoveDirection = moveDirection.normalized;

			if(enableFreeMove) {
				IsMovingToPlayerTarget = false;
			}
		}

		protected Vector2 GetDirectionAwayFromPlayer() {
			return (PlayerTargetPosition - (Vector2)transform.position).normalized * -1f;
		}

		#endregion
	}
}
