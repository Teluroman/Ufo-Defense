using UnityEngine;

public class Shop : MonoBehaviour {

    //Atributos// Un blueprint para cada torreta
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;
    public TurretBlueprint machineGun;

    BuildManager buildManager;


    //------------------------------------------------------------------------------
    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if (GameManager.GameIsOver)
            gameObject.SetActive(false);
    }

    //Métodos para botones (Uno para cada torreta)------------------------------------------------------------
    public void SelectStandardTurret() 
    {
        Debug.Log("Standard Turret Selected");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile launcher Selected");
        buildManager.SelectTurretToBuild(missileLauncher);
    }

    public void SelectLaserBeamer()
    {
        Debug.Log("Laser Beamer Selected");
        buildManager.SelectTurretToBuild(laserBeamer);
    }

    public void SelectMachineGun()
    {
        buildManager.SelectTurretToBuild(machineGun);
    }
}
