using UnityEngine;

using MyBox;

namespace GameEntity.Enemy {

    [RequireComponent(typeof(SpriteRenderer))]
    public class Splitter : Enemy {
        [Separator("Splitter properties")]
        [SerializeField, Tooltip("The enemy prefab to split to"), MustBeAssigned]
        private Enemy enemyPrefabToSplitInto;

        [SerializeField, Tooltip("How many enemies will it split into"), PositiveValueOnly]
        private int splitCount;

        [SerializeField, Tooltip("The angle of wideness which the split enemies will fly towards"), PositiveValueOnly]
        private float splitWideness;

        protected override void OnCollisionEvent(CollisionType collisionType) {
            if (collisionType == CollisionType.PLAYER) {
                Split();
            }
        }

        #region Utils

        private void Split() {

            Vector2 initialSplitDirection = GetDirectionAwayFromPlayer();

            float splitSpreadOffset = splitWideness / 2f;

            float angleStep = (splitWideness / (splitCount + 1));
            float currentAngle = (angleStep - splitSpreadOffset);

            for (int i = 0; i < splitCount; ++i) {
                Vector2 directionToMove = initialSplitDirection.Rotate(currentAngle);

                CreateAndMoveSplittedEnemy(directionToMove);

                currentAngle += angleStep;
            }

            #region Local_Function

            void CreateAndMoveSplittedEnemy(Vector2 directionToMove) {
                Enemy newSplittedEnemy = Instantiate(enemyPrefabToSplitInto);

                Vector3 offset = Radius * directionToMove;
                newSplittedEnemy.transform.position = transform.position + offset;

                newSplittedEnemy.SetPlayerTarget(PlayerTarget);
                newSplittedEnemy.SetFreeMoveDirection(directionToMove);
            }

            #endregion
        }

        #endregion
    }
}