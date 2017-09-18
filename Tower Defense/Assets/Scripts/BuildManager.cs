using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {

    //Singleton*******************************
    public static BuildManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one build manager in scene");
            return;
        }
        instance = this;
    }
    //*****************************************

    
    //Variables//
    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild; //modelo de torreta
    private Node selectedNode;

    public NodeUI nodeUi;

    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    


    //-----------------------------------------------------------------------------------------------------------------
    public void SelectTurretToBuild(TurretBlueprint turret) //Se le pasa el modelo de la torreta a construir
    {
        turretToBuild = turret;
        DeselectNode();
    }


    //--------
    public void SelectNode(Node node) //seleccionamos un nodo y hacemos aparecer el menu de mejora y venta.
    {
        if (selectedNode == node) //Deselecciona el nodo ý hace desaparecer el menú si ya estaba seleccionado
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUi.SetTarget(node);
    }


    //--------------------
    public void DeselectNode() //deshace la selección y oculta el menú de nodo.
    {
        selectedNode = null;
        nodeUi.Hide();
    }


    //------------------
    public TurretBlueprint GetTurretToBuild() //Devuelve la la torreta a construir
    {
        return turretToBuild;
    }
}
