using UnityEngine;

using MyBox;

namespace GameEntity.Enemy {

    public class Shrinker : Enemy {
        [SerializeField, Tooltip("The enemy to shrink to"), MustBeAssigned]
        private Enemy shrinkedEnemy;

        [AutoProperty, SerializeField]
        private SpriteRenderer spriteRenderer;

        protected override void OnCollisionEvent(CollisionType collisionType) {
            if (collisionType == CollisionType.PLAYER) {
                ShrinkEnemy();
            }
        }

        #region Utils

        private void ShrinkEnemy() {
            Enemy newShrinkedEnemy = Instantiate(shrinkedEnemy);

            Vector2 directionToMove = GetDirectionAwayFromPlayer();

            SetNewShrinkedEnemyPosition();

            newShrinkedEnemy.SetPlayerTarget(PlayerTarget);
            newShrinkedEnemy.SetFreeMoveDirection(directionToMove);

            #region Local_Function

            void SetNewShrinkedEnemyPosition() {
                Vector3 offset = Radius * directionToMove;
                newShrinkedEnemy.transform.position = transform.position + offset;
            }

            #endregion
        }

        #endregion
    }
}
