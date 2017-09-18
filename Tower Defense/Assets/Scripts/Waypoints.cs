using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount]; //Inicializa el array con el número de hijos que tenga el objeto waypoints
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i); //asigna a la posición i de points el hijo i de este objeto
        }
    }
}
