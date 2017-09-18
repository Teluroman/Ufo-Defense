using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour {

    //VARIABLES//
    public string menuSceneName = "MainMenu";

    public string nextLevel = "Level2";
    public int levelToUnlock = 2;

    public SceneFader sceneFader;


    //Métodos para botones------------------------------------------------------------------------------
    public void Continue() //Desbloquea el siguiente nivel y cambia a ese nivel
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu() //Al menu
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        sceneFader.FadeTo(menuSceneName);
    }
    //-------------------------------------------------------------------------------------------------
}
