using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    //Atributos//
    public SceneFader fader;

    public Button[] levelButtons;


    //----------------------------------------------------------------------
    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1); //Para saber el nivel al que se ha llegado

        for (int i = 0; i < levelButtons.Length; i++) //Bloquea todos los botones a partir del último nivel desbloqueado.
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false; //Hace al botón no interactuable.
            }
        }
    }

    //Método de botón
    public void Select(string levelName) //Lleva hasta el nivel seleccionado (Se indica en el editor)
    {
        fader.FadeTo(levelName);
    }
}
