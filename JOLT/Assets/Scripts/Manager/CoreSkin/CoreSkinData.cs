using GameInterface.ShopMenu;

namespace GameManager.CoreSkin {
    [System.Serializable]
    public class CoreSkinData {
        public string SkinName { get; set; }
        public ShopItemState SkinState { get; set; }

        internal CoreSkinData(string skinName, ShopItemState skinState) {
            SkinName = skinName;
            SkinState = skinState;
        }
    }
}