using UnityEngine;
using UnityEngine.EventSystems;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Node : MonoBehaviour {

    //Atributos//
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset; //Para evitar que la torreta esté dentro del nodo

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Color startColor;
    private Renderer rend;

    private BuildManager buildManager;

    //ID Logro
    private const string Investment = "CgkIx7eV8tEGEAIQAg";


    //-------------------------------------------------------------------------------
    void Start() //Obtiene el renderer del nodo y guarda su color por defecto
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance; //Asigna el singleton del BuildManager
    }

    //---------------
    public Vector3 GetBuildPosition() //Devuelve la posición donde se construirá la torreta
    {
        return transform.position + positionOffset;
    }


    //---------------
    private void OnMouseDown() //Al hacer clic
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null) //Si hay torreta
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) //Si no se puede construir
            return;

        BuildTurret(buildManager.GetTurretToBuild()); //Construye la torreta
    }


    //---------------
    void BuildTurret(TurretBlueprint blueprint) //Construye la torreta
    {
        if (PlayerStats.Money < blueprint.cost) //¿Hay suficiente dinero?
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= blueprint.cost; //Resta el coste del dinero del jugador

        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); //Instancia la torreta
        turret = _turret;

        turretBlueprint = blueprint;

        //Efecto de construcción
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); 
        Destroy(effect, 5f);

        Debug.Log("Turret build. Money left: " + PlayerStats.Money);
    }


    //---------------
    public void UpgradeTurret() //Mejorar torreta
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost) //¿Hay suficiente dinero?
        {
            Debug.Log("Not enough money to upgrade that");
            return;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost; //Resta el dinero

        Destroy(turret); //Destruye la torreta antigua

        //Construye la nueva
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        //Efecto de construcción
        GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpgraded = true;

        //Logro Investment
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_investment, (bool success) =>
        {
            //TODO Algo
            if (success)
                Debug.Log("Invested");
            else
                Debug.Log("Error");
        });
        ////

        Debug.Log("Turret upgraded");
    }


    //---------------
    public void SellTurret() //Vender torreta
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount(); //Suma el valor de venta

        //Efecto de venta
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        //Destruye la torreta
        Destroy(turret);
        turretBlueprint = null;

        isUpgraded = false;
    }


    //-------------------
    public void TargetNearEnemy()
    {
        turret.GetComponent<Turret>().farEnemy = false;
    }


    //---------------------------
    public void TargetFarEnemy()
    {
        turret.GetComponent<Turret>().farEnemy = true;
    }

    //---------------
    private void OnMouseEnter() //Cuando el ratón pasa por encima se ilumina de un color dependiendo si se tiene dinero o no.
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }


    //---------------
    private void OnMouseExit() //Al quitar el ratón vuelve al color original
    {
        rend.material.color = startColor;
    }
}
