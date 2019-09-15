using GameInterface.ShopMenu;

namespace GameManager.CoreSkin {
    [System.Serializable]
    public class CoreSkinData {
        public string skinName;
        public ShopItemState skinState;

        internal CoreSkinData(string _skinName, ShopItemState _skinState) {
            skinName = _skinName;
            skinState = _skinState;
        }
    }
}