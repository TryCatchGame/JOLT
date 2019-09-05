using UnityEngine;

using MyBox;
namespace GameParticle.Enemy {
    [RequireComponent(typeof(SpriteRenderer))]
    public class EnemyBounceParticle : MonoBehaviour {
        [Separator("Auto Assign")]
        [SerializeField, AutoProperty]
        private SpriteRenderer spriteRenderer;

        [Separator("Particle Properties")]
        [SerializeField, Tooltip("The alpha scale this particle starts at"), Range(0f, 1f)]
        private float startingAlphaScale;

        [SerializeField, Tooltip("How long it takes for this particle to completely fade away"), PositiveValueOnly]
        private float stayDuration;

        private float stayTimer;

        private void Awake() {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            stayTimer = 0f;
        }

        private void Update() {
            stayTimer += Time.deltaTime;

            LerpSpriteRendererAlphaByCurrentlyStayedTime();

            DestroyIfStayedDurationReached();

            #region Local_Function

            void DestroyIfStayedDurationReached() {
                if (stayTimer >= stayDuration) {
                    // NOTE: Object pooling?
                    Destroy(gameObject);
                }
            }

            void LerpSpriteRendererAlphaByCurrentlyStayedTime() {
                float currInterpolate = (stayTimer / stayDuration);
                float currAlphaScale = Mathf.Lerp(startingAlphaScale, 0f, currInterpolate);
                SetSpriteRendererAlphaScale(currAlphaScale);
            }

            void SetSpriteRendererAlphaScale(float alphaScale) {
                var temp = spriteRenderer.color;
                temp.a = alphaScale;
                spriteRenderer.color = temp;
            }

            #endregion
        }
    }
}
