using UnityEngine;

public class Shop : MonoBehaviour
{
    // Blueprints for the different turrets
    [Header("Blueprints")]
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserTurret;

    BuildManager buildManager;

    // Sets the build manager to the variable so it will know to go inside
    // of the script and be easier to sort and write the logic (code)
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Selects the standard turret and writes it in the log as selected
    public void SelectStandardTurret()
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    // Selects the missle launcher and writes it in the log as selected
    public void SelectMissileLauncher()
    {
        Debug.Log("Missile Turret Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    // Selects the laser turret and writes it in the log as selected
    public void SelectLaserTurret()
    {
        Debug.Log("Laser Turret Selected");
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
