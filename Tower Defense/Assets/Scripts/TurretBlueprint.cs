using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint { //Clase que tiene los datos básicos de las torretas

    //Atributos//
    //Torreta y coste
    public GameObject prefab;
    public int cost;

    //Torreta mejorada y coste de mejora
    public GameObject upgradedPrefab;
    public int upgradeCost;

    //------------------
    public int GetSellAmount()
    {
        return cost / 2;
    }


}
