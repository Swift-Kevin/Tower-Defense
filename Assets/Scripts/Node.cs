using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // Defined different variables for the node when hovered and not enough money
    [Header("Hover Colors")]
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    // Hides the turret, turretblueprint, and upgraded bool in inspector, so it cannot be overridden
    #region Hiding
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;
    #endregion
    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    // When the script starts it will set the startColor of the node to the materials clor
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    // Gets the position of the node so it will build turrets directly on the center
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
    
    // Method for when the mouse is pressed down
    void OnMouseDown()
    {
        // If there is a overlay such as the shop or health then the button press down will do nothing
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If there is no turret on the node it will select the node
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        // If the node cannot be built on then it will do nothing
        if (!buildManager.CanBuild)
            return;

        // After all of that and selecting the node it will build the turret on the selected node
        BuildTurret(buildManager.GetTurretToBuild());
    }

    // Method for actions taken to build a turret (cost, effect, taking money)
    void BuildTurret (TurretBlueprint blueprint)
    {        
        // Checks if the player has enough money for the turret
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enough money to build that!");
            return;
        }

        // Takes the turrets cost out of the players money balance
        PlayerStats.Money -= blueprint.cost;

        // Creates the turret by instantiating (creating) the object
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        // Sets the blueprint to the methods passed blueprint var
        turretBlueprint = blueprint;

        // Creates the build effect around the turret (destroys after 5 seconds)
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // Says in the debug console that the turret was built
        Debug.Log("Turret build!");
    }

    // Method to upgrade the turret
    public void UpgradeTurret()
    {
        // Checks if the player has enough money to upgrade the turret
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enough money to upgrade!");
            return;
        }

        // Takes money out of the players account if there is enough
        PlayerStats.Money -= turretBlueprint.upgradeCost;
        
        // Gets rid of the old turret
        Destroy(turret);

        // Builds a new turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        // Creates a new effect for the turret being upgraded
        GameObject effect = Instantiate(buildManager.upgradeEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // Sets the bool of isUpgraded to true to tell us that it has been upgraded.
        isUpgraded = true;

        // Debugs in the log that a turret has been upgraded
        Debug.Log("Turret Upgraded!");
    }

    // Method to sell the turret
    public void SellTurret()
    {
        // Adds the money to the players money balance
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // Creates an effect for the selling of the turret (destroys after 5 seconds)
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // Sets the blueprint to null, and destroys the turret on the tile
        Destroy(turret);
        turretBlueprint = null;
    }

    // When the mouse is pressed this updates to see if the player has enough money to do certain
    // actions like if it can build and setting the color of the node to the right one
    void OnMouseEnter()
    {
        // Sees if the mouse is over an overlay object
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // Sees if the buildmanager cannot build on the node
        if (!buildManager.CanBuild)
            return;
        
        // If the money is available the tiles color will remain the same when hovered
        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        // otherwise it will turn to a color which signifies not enough money
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    // When the mouse exits hovering the node it will turn its color back to its starting color
    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

}