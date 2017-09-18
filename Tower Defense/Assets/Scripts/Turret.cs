using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    //Atributos//
    private Transform target;
    private Enemy targetEnemy;

    [Header("General")]

    public float range = 15f;

    [Header("Use Bullets (default")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;

    public int damageOverTime = 30;
    public float slowAmount = 0.5f;

    public LineRenderer lineRenderer; //Una linea que hace el efecto de un láser
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Target Far enemy")]
    public bool farEnemy = false;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    //Rotación
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public Transform firePoint;



    //---------------------------------------------------------------------------------------------------
	void Start () {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        /*if (farEnemy)//Cada medio segundo invoca UpdateTarget o UpdateFarTarget dependiendo de la configuración
        {
            InvokeRepeating("UpdateFarTarget", 0f, 0.5f);
        }
        else
        {
            InvokeRepeating("UpdateTarget", 0f, 0.5f); 
        }
        */
    }


    //------------Detecta a todos los enemigos y selecciona el que está a menor distancia en el rango
    void UpdateTarget()
    {
        if (farEnemy)
        {
            UpdateFarTarget();
            return;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Todos los tag enemigo
        float shortestDistance = Mathf.Infinity; //Distancia infinita para evitar bugs
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies) //Mira a todos los enemigos y busca al más cercano
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range) //Si está en el rango actualiza la variable target con el enemigo
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            //Si no hay ningún enemigo en el rango
            target = null;
        }

    }


    //------------------Detecta a todos los enemigos y ataca al que esté a mayor distancia dentro del rango
    void UpdateFarTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag); //Todos los tag enemigo
        float farestDistance = 0; //Distancia infinita para evitar bugs
        GameObject farestEnemy = null;

        foreach (GameObject enemy in enemies) //Mira a todos los enemigos y busca al más cercano
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy > farestDistance)
            {
                farestDistance = distanceToEnemy;
                farestEnemy = enemy;
            }
        }

        if (farestEnemy != null && farestDistance <= range) //Si está en el rango actualiza la variable target con el enemigo
        {
            target = farestEnemy.transform;
            targetEnemy = farestEnemy.GetComponent<Enemy>();
        }
        else
        {
            //Si no hay ningún enemigo en el rango
            target = null;
        }
    }
    

    //------------------
	void Update () {

        fireCountdown -= Time.deltaTime;

        if (target == null) //¿Hay objetivo?
        {
            if (useLaser) //¿Es un láser?
            {
                if (lineRenderer.enabled) 
                {
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                    impactEffect.Stop();
                }
            }

            return;
        }
            

        LockOnTarget(); //Apunta al objetivo

        if (useLaser)
        {
            Laser(); //Dispara el láser
        }else
        {
            if (fireCountdown <= 0f) //Dispara esperando a el ratio de disparo
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

        }

	}

    //------------------
    void LockOnTarget() //Apunta al objetivo
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        //Rotación suave
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    //------------------
    void Laser() //El laser hace daño cada frame y ralentiza
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled) //Activa la línea si está desactivada
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
        }

        lineRenderer.SetPosition(0, firePoint.position); //De donde sale el laser
        lineRenderer.SetPosition(1, target.position); //A donde va

        Vector3 dir = firePoint.position - target.position; //dirección para rotar el impactEffect

        impactEffect.transform.position = target.position + dir.normalized; //para que no aparezca dentro del enemigo

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    //------------------
    void Shoot() //Instancia la bala
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(target); //Le pasamos el objetivo a la bala
    }

    //Para el editor. Dibuja el rango-----
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
