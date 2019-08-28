﻿using UnityEngine;

using MyBox;

using GameEntity.Player;

namespace GameEntity.Enemy {
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

        private bool isMovingToPlayerTarget;

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

        private void Awake() {
            isMovingToPlayerTarget = true;
        }

        void Update() {
            if (isMovingToPlayerTarget) {
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
                if (borderCollisionChecker.IsCollidingWithScreenBorder()) {
                    isMovingToPlayerTarget = true;
                    OnCollisionEvent(CollisionType.SCREEN_BORDER);

                    CreateCollisionEffect();
                }
            }

            #endregion
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.CompareTag(playerTag)) {
                OnCollisionEvent(CollisionType.PLAYER);

                TriggerCollisionDeath();
            }

            #region Local_Function

            void TriggerCollisionDeath() {
                CreateCollisionEffect();
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

        private void CreateCollisionEffect() {
            // TODO: Some effects on *general* collision
            // NOTE: This function should suit for all types of collision; No more, no less.
        }

        internal void SetPlayerTarget(Core target) {
            PlayerTarget = target;
            playerTag = PlayerTarget.tag;
        }

        protected void SetFreeMoveDirection(Vector2 moveDirection) {
            FreeMoveDirection = moveDirection.normalized;
        }

        #endregion
    }
}