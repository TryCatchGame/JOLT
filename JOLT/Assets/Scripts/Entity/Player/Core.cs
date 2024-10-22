﻿using UnityEngine;

using MyBox;

using GameEntity.Collectables;
using GameManager;

namespace GameEntity.Player {
    public class Core : MonoBehaviour {

        #region CorePaddles_Struct

        [System.Serializable]
        private struct CorePaddles {
            [SerializeField, Tooltip("The main paddle for the player"), MustBeAssigned]
            private Paddle playerPaddle;

            [SerializeField, Tooltip("The shield paddle for the player"), MustBeAssigned]
            private ShieldPaddle shieldPaddle;

            public Paddle PlayerPaddle { get => playerPaddle; }
            public ShieldPaddle ShieldPaddle { get => shieldPaddle; }

            internal void SetPlayerCollisionTags(PlayerCollisionTags collisionTags) {
                shieldPaddle.SetPaddleCollisionTags(collisionTags);
                playerPaddle.SetPaddleCollisionTags(collisionTags);
            }

            internal void SetCanMove(bool canMove) {
                playerPaddle.CanMove = canMove;
                shieldPaddle.CanMove = canMove;
            }
        }

        #endregion

        [Separator("DEBUG")]
        [SerializeField, Tooltip("True if the player can never die")]
        private bool invulnerable;

        [Separator("Core paddles")]
        [SerializeField, Tooltip("The respective paddles for the core"), MustBeAssigned]
        private CorePaddles corePaddles;

        [Separator("Collision Tags")]
        [SerializeField, Tooltip("The respective collision tags"), MustBeAssigned]
        private PlayerCollisionTags playerCollisionTags;

        private void Awake() {
            if (!Application.isEditor) {
                invulnerable = false;
            }

            corePaddles.SetPlayerCollisionTags(playerCollisionTags);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (CollisionHasTag(playerCollisionTags.CollectableTag)) {
                collision.gameObject.GetComponent<Collectable>().Destroy();
            } else if (CollisionHasTag(playerCollisionTags.EnemyTag)) {
                TriggerGameOver();
            }

            #region Local_Function

            bool CollisionHasTag(string tagToCheck) {
                return collision.gameObject.CompareTag(tagToCheck);
            }

            #endregion
        }

        #region Utils

        // NOTE: Refactor to 1 function?
        internal void DisablePaddleMovement() {
            corePaddles.SetCanMove(false);
        }

        internal void EnablePaddleMovement() {
            corePaddles.SetCanMove(true);
        }

        private void TriggerGameOver() {
            if (invulnerable) { return; }

            GameOverManager.Instance.TriggerGameOver();
        }

        internal void IncreaseShieldPaddleLife() {
            corePaddles.ShieldPaddle.IncrementShieldLife();
        }

        #endregion
    }
}

