using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startClor;

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startClor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            BuildManager.instance.SelectNode(this);
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
        
        rend.material.color = Color.red;
    }

    public void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Не хватает золота!");
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, transform.position, Quaternion.identity);
        GameObject turretEffect = (GameObject)Instantiate(buildManager.buildEffect, transform.position, Quaternion.identity);
        Destroy(turretEffect, 1.5f);
        turret = _turret;
        turretBlueprint = blueprint;
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Не хватает золота для улчучшения!");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret);

        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, transform.position, Quaternion.identity);
        GameObject turretEffect = (GameObject)Instantiate(buildManager.buildEffect, transform.position, Quaternion.identity);
        Destroy(turretEffect, 1.5f);
        turret = _turret;
        isUpgraded = true;
    }

    public void SellTurret()
    {
        if(isUpgraded)
            PlayerStats.Money += turretBlueprint.upgradeCost / 2;
        else
            PlayerStats.Money += turretBlueprint.cost / 2;

        Destroy(turret);
        turret = null;
        isUpgraded = false;
        GameObject turretEffect = (GameObject)Instantiate(BuildManager.instance.buildEffect, transform.position, Quaternion.identity);
        Destroy(turretEffect, 1.5f);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (!buildManager.HasMoney || turret != null)
        {
            rend.material.color = Color.red;
            return;
        }

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }

    }

    void OnMouseExit()
    {
        rend.material.color = startClor;
    }


}
