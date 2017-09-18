using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour { //Actualiza el texto de las vidas

    public Text livesText;

    private void Update()
    {

        if (PlayerStats.Lives == 1) //Si queda 1 vida pondrá LIVE
        {
            livesText.text = PlayerStats.Lives.ToString() + " LIVE";
        }
        else
        {
            livesText.text = PlayerStats.Lives.ToString() + " LIVES";
        }
    }
}
