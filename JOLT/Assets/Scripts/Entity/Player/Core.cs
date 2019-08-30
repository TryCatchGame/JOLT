using UnityEngine;

using MyBox;

using GameEntity.Collectables;

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
        }

        #endregion

        [Separator("Core paddles")]
        [SerializeField, Tooltip("The respective paddles for the core"), MustBeAssigned]
        private CorePaddles corePaddles;

        [Separator("Collision Tags")]
        [SerializeField, Tooltip("The respective collision tags"), MustBeAssigned]
        private PlayerCollisionTags playerCollisionTags;

        private void Awake() {
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

        private void TriggerGameOver() {
            // TODO: Trigger event that the player's core got hit by an enemy.
        }

        internal void IncreaseShieldPaddleLife() {
            corePaddles.ShieldPaddle.IncrementShieldLife();
        }

    }
}

