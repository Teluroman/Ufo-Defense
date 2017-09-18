using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    //Atributos//
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    //public float timeBetweenWaves = 5f;
    private float countdown = 5f;

    public Text countdownText;

    private int waveNumber = 0;

    public GameManager gameManager;




    //----------------------------------------------------------------------------------
    private void Update()
    {
        
        if (/*EnemiesAlive > 0 ||*/ GameManager.GameIsOver || GameManager.PreparationPhase) //Si hay enemigos vivos no pasa nada
        {
            return;
        }

        if (EnemiesAlive > 0 && waveNumber == waves.Length)
        {
            countdownText.text = "00.00";
            return;
        }

        if (waveNumber == waves.Length) //si se acaban las oleadas se gana el nivel
        {
            gameManager.WinLevel();
            countdownText.text = "00.00";
            this.enabled = false;
        }

        if (countdown <= 0f){ //Si la cuenta atrás llega a 0 spawnea otra oleada
            countdown = waves[waveNumber].nextWaveTime;
            StartCoroutine(SpawnWave());
            //TODO llamar a parar agitar para conseguir dinero
            ShakeEvent.CanShake = false;
            return;
        }

        //TODO agitar para conseguir dinero
        ShakeEvent.CanShake = true;

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        countdownText.text = string.Format("{0:00.00}", countdown); //Actualiza e indica cómo se mostrará el temporizador
    }


    IEnumerator SpawnWave()
    {
        PlayerStats.Rounds++; //Aumenta el número de rondas sobrevividas

        Wave wave = waves[waveNumber]; //Coje la oleada pertinente
        waveNumber++;
        EnemiesAlive += wave.count;

        for (int i = 0; i < wave.count; i++) 
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }

        
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
