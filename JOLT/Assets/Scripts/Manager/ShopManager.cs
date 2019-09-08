using UnityEngine.EventSystems;

using MyBox;

namespace GameManager {
    public class ShopManager : Singleton<ShopManager> {
        private EventSystem currentEventSystem;

        private void Awake() {
            currentEventSystem = EventSystem.current;
            currentEventSystem.enabled = false;
        }

        #region Utils
        public void EnableEventSystem() {
            currentEventSystem.enabled = true;
        }

        public void DisableEventSystem() {
            currentEventSystem.enabled = false;
        }
        #endregion
    }
}
