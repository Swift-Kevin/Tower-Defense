using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    // Keeps track of instances of BuildManagers so they will
    // default back to this main default.
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    // Defining different objects and scripts to use for this script
    [Header("Effects")]
    public GameObject buildEffect;
    public GameObject upgradeEffect;
    public GameObject sellEffect;

    [Header("Node UI")]
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
    public NodeUI nodeUI;

    // Getter for if the node can be built on
    public bool CanBuild { get { return turretToBuild != null; } }
   
    // Bool for if the player has enough money to do certain actions
    // such as build on the node or upgrade.
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    // Selects the node that is from the class Node and sets whatever node is selected to the one 
    // passed through it and the turretToBuild to null so it can disable switching turrets while building
    // This makes it easier to place multiple turrets down of the same type and not have to repeatedly click
    // the turret icon to place every turret down.
    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    // Hides the Upgrade/Sell menu when a turret is clicked on, and sets the node selected to null
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    // Out of the menu options for turrets to build, the user can select a turret to be built, and stores it for placing
    // also does a node deselect so it doesn't alter a node while selecting turret
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }
    
    // Returns turret to build
    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }
}