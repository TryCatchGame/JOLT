using UnityEngine;

using MyBox;

namespace GameEntity.Player {
    public class Core : MonoBehaviour {

        [SerializeField, Tooltip("The tag for the enemies"), Tag]
        private string enemyTag;

        [SerializeField, Tooltip("The tag for the collectables"), Tag]
        private string collectableTag;

        private void OnCollisionEnter2D(Collision2D collision) {

            if (CollisionHasTag(collectableTag)) {
                Destroy(collision.gameObject);
            } else if (CollisionHasTag(enemyTag)) {
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

    }
}

