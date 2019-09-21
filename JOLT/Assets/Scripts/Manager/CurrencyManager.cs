using UnityEngine;
using MyBox;

using GameInterface.InGame;

namespace GameManager {
	public class CurrencyManager : Singleton<CurrencyManager> {
		[Separator("Displays")]
		[SerializeField, Tooltip("The currency menu that is displayed"), MustBeAssigned]
		private CurrencyMenu currencyMenu;

		public int GemCount { get; private set; }

        private void Awake() {
            GemCount = GlobalProperties.TotalGemCount_Local;
        }

		internal void ModifyGemValue(int modifyAmount) {
            GemCount += modifyAmount;
            // Money can't go below 0.
            ClampGemValue();

            currencyMenu.UpdateGemCountText(GemCount);

            GlobalProperties.TotalGemCount_Local = GemCount;
            UpdateAchievementIfModificationIsPositive();

            #region Local_Function

            void UpdateAchievementIfModificationIsPositive() {
                if (modifyAmount > 0) {
                    GooglePlayServiceManager.Instance.UpdateDiamondAchievements(modifyAmount);
                }
            }

            void ClampGemValue() {
                if (GemCount < 0) {
                    GemCount = 0;
                }
            }

            #endregion
        }
	}
}
