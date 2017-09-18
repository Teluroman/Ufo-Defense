using UnityEngine;

[System.Serializable]
public class Wave { //Clase con los datos básicos de las oleadas

    public GameObject enemy;
    public int count; //Amount of enemies
    public float rate;
    public float nextWaveTime;

}
