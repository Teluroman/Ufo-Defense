using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour {

    //Atributos//
    public static bool GameIsOver; //Para saber si el juego ha acabado (Victoria o derrota)
    public static bool PreparationPhase; //Si la fase de preparación está activa o no

    public GameObject gameOverUI;
    public GameObject completeLevelUI;

    public MusicManager musicManager;


    //--------------------------------------------------------------------------------------
    private void Awake()
    {
        PreparationPhase = true;
    }

    private void Start()
    {
        GameIsOver = false;
    }

    void Update () {
        if (GameIsOver)
            return;

		if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    void EndGame() //Game Over.
    {
        GameIsOver = true;

        musicManager.LoseClip(); //Suena música de derrota

        gameOverUI.SetActive(true); //Activa la interfaz de game over

    }

    public void WinLevel() //Victoria
    {
        GameIsOver = true;

        PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_initiated, (bool success) =>
        {

        });

        musicManager.WinClip(); //Suena música de victoria

        completeLevelUI.SetActive(true); //Activa la interfaz de victoria
    }
}
