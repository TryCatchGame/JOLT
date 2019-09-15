﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

using MyBox;
namespace GameInterface.ShopMenu {
    [RequireComponent(typeof(Image))]
    public class ShopItem : MonoBehaviour {
        private static readonly Color OWNED_COLOR = Color.yellow;
        private static readonly Color NORMAL_COLOR = Color.white;
        private static readonly Color IN_USE_COLOR = Color.cyan;

        [SerializeField, AutoProperty]
        private Image holderImage;

        [Separator()]
        [SerializeField, Tooltip("The image to show the content of this item"), MustBeAssigned]
        private Image contentImage;

        [SerializeField, Tooltip("The text to show the price of this item"), MustBeAssigned]
        private TextMeshProUGUI contentPriceText;

        private void Awake() {
            holderImage = GetComponent<Image>();
        }

        internal void SetShopItemContent(Sprite contentSprite, int contentPrice) {
            contentImage.sprite = contentSprite;
            contentPriceText.text = contentPrice.ToString();
        }

        internal void SetShopItemState(ShopItemState shopItemState) {
            switch (shopItemState) {
                case ShopItemState.IN_USE:
                    holderImage.color = IN_USE_COLOR;
                    contentPriceText.text = "USING";
                    break;
                case ShopItemState.OWNED:
                    holderImage.color = OWNED_COLOR;
                    contentPriceText.text = "OWNED";
                    break;
                case ShopItemState.NORMAL:
                    holderImage.color = NORMAL_COLOR;
                    break;
            }
        }
    }

}