using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour {

    public Text moneyText;
    public string paramName;
	
	void Update () {
        moneyText.text = paramName + PlayerStats.Money.ToString();
	}
}
