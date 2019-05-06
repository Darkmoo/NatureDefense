using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour {

    public Text livesText;
    public string paramName;
	
	void Update () {
        livesText.text = paramName + PlayerStats.Lives.ToString() + " ♥";
	}
}
