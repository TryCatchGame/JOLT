using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

namespace GameManager {

	public class CurrencyManager : Singleton<CurrencyManager> {

		[Separator("Displays")]
		[SerializeField, Tooltip("The currency menu that is displayed"), MustBeAssigned]
		private CurrencyMenu currencyMenu;

		[SerializeField, Tooltip("Current amount of money the player has.")]
		private int money;

		public int Money { get => money; private set => money = value; }

		public void ModifyMoneyValue(int modifyAmount) {
			// Money can't go below 0.
			money = Mathf.Max(0, money + modifyAmount);
			// Update currency to display.
			currencyMenu.UpdatePlayerCurrency(money);
			// Save money.
			PlayerPrefs.SetInt("Money", money);
		}

		public bool CheckSufficientMoney(int amount) {
			if(amount < Money) {
				return false;
			}
			return true;
		}
	}
}
