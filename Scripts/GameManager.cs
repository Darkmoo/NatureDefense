using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool GameIsOver;
    public GameObject gameOverUI;

    void Start()
    {
        GameIsOver = false;
        Time.timeScale = 1f;

    }

    void Update () {
        if (GameIsOver)
            return;
        CheckButtons();
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }

	}

    private void EndGame()
    {
        GameIsOver = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
        Debug.Log("EndGame");
    }

    void CheckButtons()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }

        if (Input.GetKey("1"))
        {
            Shop.instance.SelectStandartTurret();
        }

        if (Input.GetKey("2"))
        {
            Shop.instance.SelectCrystalLauncher();
        }

        if (Input.GetKey("3"))
        {
            Shop.instance.SelectPoisonLaser();
        }
    }

}