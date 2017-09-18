using UnityEngine;
using UnityEngine.UI;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class Enemy : MonoBehaviour {

    //Variables//
    public float startSpeed = 10f;

    [HideInInspector]
	public float speed;

    public float startHealth = 100;
    private float health;

    public int worth = 50;

    public GameObject deathEffect;

    [Header("Enemy Type")]
    public bool teleporter;

    public GameObject teleportEffect;

    [Header("Unity Stuff")]
    public Image healthBar;

    //Para evitar bug que hace que muera varias veces.
    private bool isDead = false;

    //--------------------------------------------------------------------------------
    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }


    public void TakeDamage (float amount) //El daño que hay que restarle al enemigo
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth; //Actualizar la barra de vida

        if(health <= 0 && !isDead) //Si la vida llega a 0
        {
            Die();
        }
    }


    public void Slow(float percentage) //Reducir velocidad enemigo
    {
        speed = startSpeed * (1 - percentage);
    }


    void Die() //Muerte del enemigo
    {
        isDead = true;

        //Logro  First of many
        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_first_of_many, (bool success) =>
        {
            //TODO Algo
            if (success)
                Debug.Log("1st DED");
            else
                Debug.Log("Error");
        });
        ///

        //Logros Destruir enemigos
        //Crusher
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_crusher, 1, (bool success) =>
        {
            if (success)
                Debug.Log("Crusher ++");
        });

        //Destroyer
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_destroyer, 1, (bool success) =>
        {
            if (success)
                Debug.Log("Destroyer ++");
        });

        //Obliterator
        PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_obliterator, 1, (bool success) =>
        {
            if (success)
                Debug.Log("Obliterator ++");
        });
        ////


        PlayerStats.Money += worth;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--; //Reduce el número de enemigos de la oleada

        Destroy(gameObject);
    }
}
