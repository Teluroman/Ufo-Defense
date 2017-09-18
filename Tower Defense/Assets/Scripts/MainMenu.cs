using UnityEngine.SceneManagement;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class MainMenu : MonoBehaviour {


    //Atributos//
    public string levelToLoad = "Level1";

    public SceneFader sceneFader;

    private bool isConnectedToGoogleServices = false;


    private void Start()
    {
        

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

        if (!isConnectedToGoogleServices)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                isConnectedToGoogleServices = success;
            });
        }
    }

    //Métodos para botones-----------------------------------------
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void AchievementsButton()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        
    }
}
