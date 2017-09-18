using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //Variables//
    private Transform target;

    public float speed = 70f;

    public int damage = 50;

    public float explosionRadius = 0f;
    public GameObject impactEffect;

    //------------------------------------------------------------------------------------------------------------------------
    public void Seek(Transform _target) //El objetivo a por el que irá (Se le tiene que pasar al crear la bala)
    {
        target = _target;
    }
	
	
	void Update ()
    {
		if (target == null) //Si el objetivo ya ha sido destruido la bala se destruye.
        {
            Destroy(gameObject);
            return;
        }

        //Cálculos para el movimiento//
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
        //Fin//
	}


    void HitTarget() //Cuando llega al objetivo
    {
        GameObject effectT = Instantiate(impactEffect, transform.position, transform.rotation); //Efecto de partículas al chocar
        Destroy(effectT, 5f);

        if (explosionRadius > 0f) //Si tiene radio de explosión (es un misil o bomba) se llama a Explode, si no hace daño normal.
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject); //Destruye la bala al final.
    }


    void Explode() //Hace daño en area
    {
        //Coje un todos los colliders en el radio de explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if(collider.tag == "Enemy") //Si es un collider enemigo causa daños.
            {
                Damage(collider.transform);
            }
        }
    }


    void Damage (Transform enemy) //Daño al enemigo
    {
        Enemy e = enemy.GetComponent<Enemy>(); //Coje el componente enemigo

        if (e != null) //Si no ha sido destruido ya lo daña.
        {
            e.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Building")
        {
            //Instanciar un efecto
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected() //SOLO EDITOR// Para vel el radio de la explosión
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
