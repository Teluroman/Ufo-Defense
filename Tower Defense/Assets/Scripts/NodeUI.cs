using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    //Atributos
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    private Node target;


    //----------------------------------------------------------------------------------------------------------------------
    public void SetTarget(Node _target) //Hace aparecer el menú en el nodo objetivo
    {
        target = _target;

        transform.position = target.GetBuildPosition(); //Coloca la interfaz donde se ha construido la torreta

        if (!target.isUpgraded) //Si no se ha mejorado se muestra el coste en el botón de mejora
        {
            upgradeCost.text = "€" + target.turretBlueprint.upgradeCost;
            upgradeButton.interactable = true;
        } else //Una vez mejorado el botón no se puede pulsar
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "€" + target.turretBlueprint.GetSellAmount(); //Muestra el precio de venta

        ui.SetActive(true);

    }

    //---------------
    public void Hide() //Desactiva la interfaz
    {
        ui.SetActive(false);
    }


    //Métodos de botones-----------------------------------------
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

    public void TargetFar()
    {
        target.TargetFarEnemy();
    }

    public void TargetNear()
    {
        target.TargetNearEnemy();
    }
    //------//
}
