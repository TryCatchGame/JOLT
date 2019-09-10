using UnityEngine;

using MyBox;
namespace GameInterface.ShopMenu {
    /// <summary>
    /// Attach it to the game-object that is responsible for having all the shop items as it's child elements.
    /// </summary>
    public class ShopItemsMenu : MonoBehaviour {
        #region PurchasableCore
        [System.Serializable]
        private class PurchasableCore {
            [SerializeField, Tooltip("The sprite of the core"), MustBeAssigned]
            private Sprite coreSprite;

            [SerializeField, Tooltip("The price of the core"), PositiveValueOnly]
            private int corePrice = 100;

            public int CorePrice { get => corePrice; }
            public Sprite CoreSprite { get => coreSprite; }
            // TODO: Some other identifier for the core's skin?
            public string CoreName { get => coreSprite.name; }
        }
        #endregion

        [SerializeField, Tooltip("Prefab of a shop item"), MustBeAssigned]
        private ShopItem shopItemPrefab;

        [Separator()]
        [SerializeField, Tooltip("The list of all the cores that can be purchased in the shop"), MustBeAssigned]
        private PurchasableCore[] purchasableCores;

        private void Awake() {
            PopulateShopItemsMenuWithPurchasableCores();

            // This component is not needed anymore.
            Destroy(this);
            #region Local_Function
            void PopulateShopItemsMenuWithPurchasableCores() {
                foreach (var purchasableCore in purchasableCores) {
                    var newShopItem = Instantiate(shopItemPrefab);
                    newShopItem.transform.SetParent(transform, false);

                    newShopItem.SetShopItemContent(purchasableCore.CoreSprite, purchasableCore.CorePrice);
                }
            }
            #endregion
        }
    }
}
