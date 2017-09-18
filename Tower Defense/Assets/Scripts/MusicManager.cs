using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    //Atributos//
    private AudioSource manager;

    public AudioClip win;
    public AudioClip lose;


    //--------------------------------------------------------------
    private void Start()
    {
        manager = GetComponent<AudioSource>();
    }

    public void WinClip() //Efecto de victoria
    {
        manager.clip = win;
        manager.loop = false;
        manager.Play();
    }

    public void LoseClip() //Efecto de derrota
    {
        manager.clip = lose;
        manager.loop = false;
        manager.Play();
    }
}
