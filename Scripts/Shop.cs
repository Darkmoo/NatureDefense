using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public static Shop instance;
    public TurretBlueprint standartTurret;
    public Button standartButton;
    public TurretBlueprint crystalLauncher;
    public Button crystalButton;
    public TurretBlueprint poisonLaser;
    public Button laserButton;

    private Color greenColor = Color.green;
    private Color startColor = Color.black;

    BuildManager buildManager;

    void Start()
    {
        instance = this;
        buildManager = BuildManager.instance;
    }

    void Update()
    {
        if (!buildManager.CanBuild)
        {
            standartButton.GetComponent<Outline>().effectColor = startColor;
            crystalButton.GetComponent<Outline>().effectColor = startColor;
            laserButton.GetComponent<Outline>().effectColor = startColor;
        }
    }

    public void SelectStandartTurret()
    {
        Debug.Log("Purchased standart turret");
        buildManager.SelectTurretToBuild(standartTurret);
        standartButton.GetComponent<Outline>().effectColor = Color.green;
    }

    public void SelectCrystalLauncher()
    {
        Debug.Log("Crystal launcher purchsed");
        buildManager.SelectTurretToBuild(crystalLauncher);
        crystalButton.GetComponent<Outline>().effectColor = Color.green;
    }

    public void SelectPoisonLaser()
    {
        Debug.Log("Poison laser purchsed");
        buildManager.SelectTurretToBuild(poisonLaser);
        laserButton.GetComponent<Outline>().effectColor = Color.green;
    }

}
