using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    //Atributos//
    public SceneFader sceneFader;

    public string nameMenuScene = "MainMenu";


    //Métodos para botones.-------------------------------------------------------
    public void Retry() //Reintentar el nivel
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu() //Al menu
    {
        sceneFader.FadeTo(nameMenuScene);
    }
}
