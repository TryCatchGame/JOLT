using UnityEngine;
using GameManager.CoreSkin;

using MyBox;
using System.Collections.Generic;

using GameManager;
namespace GameInterface.ShopMenu {
    /// <summary>
    /// Attach it to the game-object that is responsible for having all the shop items as it's child elements.
    /// </summary>
    public class ShopItemsMenu : MonoBehaviour {
        private const int CORE_PRICE = 100;

        [SerializeField, Tooltip("Prefab of a shop item"), MustBeAssigned]
        private ShopItem shopItemPrefab;

        private Dictionary<string, ShopItem> itemNameShopItems;

        private void Awake() {
            itemNameShopItems = new Dictionary<string, ShopItem>();

            // DEBUG
            if (CoreSkinDataManager.Instance != null) {
                PopulateShopItemsMenuFromDataFile();
            } else {
                Debug.Log("CoreSkinDataManager is not initalized!");
            }

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
                    newShopItem.SetShopMenuReference(this);
                    itemNameShopItems.Add(coreSkin.skinName, newShopItem);
                }
            }
            #endregion
        }

        internal void TriggerShopItemClicked(ShopItem clickedItem) {
            if (clickedItem.CurrentState == ShopItemState.IN_USE) {
                // Item is already brought and in use. Do nothing
                return;
            }

            if (clickedItem.CurrentState == ShopItemState.NORMAL) {
                RequestItemPurchase();
            } else if (clickedItem.CurrentState == ShopItemState.OWNED) {
                UseClickedItem();
            }

            #region Local_Function
            void RequestItemPurchase() {
                if (CurrencyManager.Instance.GemCount < CORE_PRICE) {
                    // TODO: Warn about insufficent gem count to purchase
                    Debug.LogWarning("Not enough gems!");
                    return;
                }

                // TODO: Ask for purchase confirmation?
                CurrencyManager.Instance.ModifyGemValue(-CORE_PRICE);
                UseClickedItem();
            }

            void UseClickedItem() {
                clickedItem.SetShopItemState(ShopItemState.IN_USE);

                var previouslyUsedItem = itemNameShopItems[GlobalProperties.CurrentlyUsingSkinName_Local];

                SwapCurrentlyUsedItemOnLocalData(previouslyUsedItem);
                SwapCurrentlyUsedItemOnFileData(previouslyUsedItem);
            }

            void SwapCurrentlyUsedItemOnLocalData(ShopItem previouslyUsedItem) {
                previouslyUsedItem.SetShopItemState(ShopItemState.OWNED);
                GlobalProperties.CurrentlyUsingSkinName_Local = clickedItem.ItemName;
            }

            void SwapCurrentlyUsedItemOnFileData(ShopItem previouslyUsedItem) {
                CoreSkinData previousItem = new CoreSkinData(previouslyUsedItem.ItemName, ShopItemState.OWNED);
                CoreSkinData currentItem = new CoreSkinData(clickedItem.ItemName, ShopItemState.IN_USE);

                CoreSkinDataManager.OverrideCoreSkinData(new CoreSkinData[] {
                    previousItem, currentItem
                });
            }
            #endregion
        }
    }
}
