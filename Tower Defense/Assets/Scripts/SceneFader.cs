using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

    //Atributos//
    public Image img;
    public AnimationCurve curve; //Curva para que el fadeIn/Out sea más suave


    //--------------------------------------------------------------------
    private void Start()
    {
        StartCoroutine(FadeIn()); //Al empezar hace un FadeIn
    }


    public void FadeTo(string scene) //Para cambiar de escena con el FadeOut
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn() 
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a); 
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene) //FadeOut con cambio de escena
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
