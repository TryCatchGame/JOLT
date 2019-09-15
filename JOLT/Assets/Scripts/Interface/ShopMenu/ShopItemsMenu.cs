using UnityEngine;
using GameManager.CoreSkin;

using MyBox;
namespace GameInterface.ShopMenu {
    /// <summary>
    /// Attach it to the game-object that is responsible for having all the shop items as it's child elements.
    /// </summary>
    public class ShopItemsMenu : MonoBehaviour {
        private const int CORE_PRICE = 100;

        [SerializeField, Tooltip("Prefab of a shop item"), MustBeAssigned]
        private ShopItem shopItemPrefab;

        private void Awake() {
            // DEBUG
            if (CoreSkinDataManager.Instance != null) {
                PopulateShopItemsMenuFromDataFile();
            } else {
                Debug.Log("CoreSkinDataManager is not initalized!");
            }

            // This component is not needed anymore.
            Destroy(this);

            #region Local_Function
            void PopulateShopItemsMenuFromDataFile() {
                CoreSkinData[] coreSkinDatas = CoreSkinDataManager.LoadCoreSkinData();

                foreach (var coreSkin in coreSkinDatas) {
                    var newShopItem = Instantiate(shopItemPrefab);
                    newShopItem.transform.SetParent(transform, false);

                    if (CoreSkinDataManager.Instance.TryGetSpriteBySpriteName(coreSkin.skinName, out Sprite coreSprite)) {
                        newShopItem.SetShopItemContent(coreSprite, CORE_PRICE);
                    }
                    newShopItem.SetShopItemState(coreSkin.skinState);
                }
            }
            #endregion
        }
    }
}
