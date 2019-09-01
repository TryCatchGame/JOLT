using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class CurrencyMenu : MonoBehaviour {

	[Separator("Visual displays")]
	[SerializeField, Tooltip("The currency text for the in game menu"), MustBeAssigned]
	private TextMeshProUGUI currencyText;

	void Start() {
		currencyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
	}

	public void UpdatePlayerCurrency(int money) {
		currencyText.text = money.ToString();
	}
}

