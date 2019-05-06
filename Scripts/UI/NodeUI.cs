using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {
    public GameObject nodeCanvas;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text sellCost;
    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.transform.position;

        if (target.isUpgraded)
        {
            upgradeCost.text = "MAX";
            sellCost.text = (target.turretBlueprint.upgradeCost / 2).ToString();
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeCost.text = target.turretBlueprint.upgradeCost.ToString();
            sellCost.text = (target.turretBlueprint.cost / 2).ToString();
            upgradeButton.interactable = true;
        }

        nodeCanvas.SetActive(true);
    }

    public void Hide()
    {
        nodeCanvas.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
