using UnityEngine;

using MyBox;

using GameManager.CoreSkin;
namespace GameEntity.Player {
    /// <summary>
    ///  Attached to the core to load skins
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CoreSkinLoader : MonoBehaviour {
        [SerializeField, AutoProperty]
        private SpriteRenderer coreRenderer;

        private void Awake() {
            if (coreRenderer == null) {
                coreRenderer = GetComponent<SpriteRenderer>();
            }

            // DEBUG!!!
            if (CoreSkinDataManager.Instance != null) {
                LoadCurrentlyUsedSkin();
            }

            // This component is not needed anymore.
            Destroy(this);
        }

        private void LoadCurrentlyUsedSkin() {
            if (CoreSkinDataManager.Instance.TryGetSpriteBySpriteName(GlobalProperties.CurrentlyUsingSkinName_Local, out Sprite playerUsedSkin)) {
                coreRenderer.sprite = playerUsedSkin;
            } else {
                coreRenderer.sprite = CoreSkinDataManager.Instance.DefaultCoreSkin;
            }
        }
    }
}
