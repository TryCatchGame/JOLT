using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MyBox;
namespace GameInterface.ShopMenu {
    public class ShopItem : MonoBehaviour {
        [SerializeField, Tooltip("The image to show the content of this item"), MustBeAssigned]
        private Image contentImage;

        [SerializeField, Tooltip("The text to show the price of this item"), MustBeAssigned]
        private TextMeshProUGUI contentPriceText;

        internal void SetShopItemContent(Sprite contentSprite, int contentPrice) {
            contentImage.sprite = contentSprite;
            contentPriceText.text = contentPrice.ToString();
        }
    }

}