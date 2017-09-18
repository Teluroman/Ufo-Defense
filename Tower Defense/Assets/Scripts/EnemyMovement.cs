using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    //Variables//
    private Transform target;
    private int waypointIndex = 0; //Índice para los waypoints

    private Enemy enemy;

    //Variables exclusivas
    private bool hasTeleported = false;

    //--------------------------------------------------------------------------------------------------
    void Start()
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0]; //empezamos apuntando al primer waypoint
    }


    void Update()
    {
        //Calculo de teleportación
        if (enemy.teleporter && !hasTeleported) //Si el enemigo se puede teleportar
        {
            int destination = Random.Range(1, Waypoints.points.Length/2 + 1);

            transform.position = Waypoints.points[destination].transform.position;
            waypointIndex = destination + 1;
            target = Waypoints.points[waypointIndex];

            GameObject efect = Instantiate(enemy.teleportEffect,transform.position, Quaternion.identity);
            Destroy(efect, 0.05f);

            hasTeleported = true;
            return;
        }

        //Cálculos de movimiento//
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) //Si estamos en el waypoint objetivo
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed; //Restablece la velocidad
    }

    
    void GetNextWaypoint() //Se aumenta el índice y se apunta al siguiente waypoint objetivo
    {
        if (waypointIndex >= Waypoints.points.Length - 1) //Si se han acabado los waypoints
        {
            EndPath();
            return; //Evita errores por posible retraso en la destrucción
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }


    void EndPath() //Al final del camino resta 1 vida al jugador, resta al número de enemigos de la oleada y se destruye.
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
