using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour { 

    //Atributos//
    public Text roundText;

    //-------------------------------------------------------------
    private void OnEnable()
    {
        StartCoroutine(AnimateText());
    }

    IEnumerator AnimateText() //Anima el texto de 1 en 1 hasta el número de rondas superadas
    {
        roundText.text = "0";
        int round = 0;

        yield return new WaitForSeconds(.7f);

        while(round < PlayerStats.Rounds)
        {
            round++;
            roundText.text = round.ToString();

            yield return new WaitForSeconds(.05f);
        }

    }
}
