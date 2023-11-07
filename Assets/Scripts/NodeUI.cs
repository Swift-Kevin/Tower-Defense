using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    // Different entries for the inspector on the NodeUI
    [Header("Insert UI Elements")]
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text SellAmount;

    private Node target;

    // Method to call and get the position of the node to display the Upgrade/Sell UI
    public void SetTarget(Node _target)
    {
        // Sets node to passed through target
        target = _target;

        transform.position = target.GetBuildPosition();

        // Checks to see if the target is not already upgraded
        if (!target.isUpgraded)
        {
            // Sets the textbox in the upgrade button to the amount of money
            // it costs to upgrade the turret
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            // If the turret is already upgraded then it will set the cost
            // upgrade to done and not let the user upgrade it again
            upgradeCost.text = "Done";
            upgradeButton.interactable = false;
        }

        // Gets the sell amount and sets the text box to that
        SellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        // Loads the UI for the Sell/Upgraed menu
        ui.SetActive(true);
    }

    // Hides the UI
    public void Hide()
    {
        ui.SetActive(false);
    }
    
    // Runs the upgrade turret and deselects the node after completeing the upgrade
    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    // Deletes the turret and also deselects the node, switches isUpgraded to false
    // so another placed turret on that node can be upgraded
    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
        target.isUpgraded = false;
    }

}
