using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    //Atributos//
    public GameObject ui;
    public GameObject shop;

    public string nameMenuScene = "MainMenu";

    public SceneFader sceneFader;


    //-----------------------------------------------------------------------------
    private void Update()
    {
        if (GameManager.PreparationPhase) //En la fase de preparación no se puede pausar
            return;

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) //Cuando se pulsa escape o P llama a Toggle
        {
            Toggle();
        }
    }


    //---------------
    public void Toggle() //Hace aparecer o desaparecer el menú de pausa.
    {
        ui.SetActive(!ui.activeSelf); //Cambia el estado de activación

        if (ui.activeSelf) //Si está activo la escala de tiempo es 0, 1 si está inactivo.
        {
            shop.SetActive(false);
            Time.timeScale = 0f;
        } else
        {
            shop.SetActive(true);
            Time.timeScale = 1f;
            
        }
    }

    
    //Métodos de botones--------------------------------------------------------------
    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        sceneFader.FadeTo(nameMenuScene);
    }

    public void PauseBtn()
    {
        Toggle();
    }
}
