using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreparationPhase : MonoBehaviour {

    //Atributos//
    public GameObject shop; //Instancia de la tienda para activarla

    public GameObject pauseButton;

    public GameObject standardButton;
    public GameObject missileButton;
    public GameObject laserButton;
    public GameObject machineGunButton;

    public Text hintText;

    private bool standardTurretActive = false;
    private bool missileTurretActive = false;
    private bool laserTurretActive = false;
    private bool machineGunTurretActive = false;

    private int activeTurretsCount = 0; //Lleva la cuenta de los toggles que están a true;

    private AudioSource sEffectSource; //Efecto sonoro de error

    //-----------------------------------------------------------------------------------
    void Start()
    {
        sEffectSource = GetComponent<AudioSource>();
        pauseButton.SetActive(false);
    }


    public void UpdateShop() //Activa la tienda y visibiliza los botones pertinentes
    {
        if (activeTurretsCount == 0) //Comprueba que haya al menos una torre activa
        {
            hintText.text = "You need to select one or more towers.";

            sEffectSource.Play();

            return;
        }

        //Comprueba que no hay más de tres torretas activas
        if (activeTurretsCount > 3)
        {
            //Muestra un texto diciendo que debe elegir como máximo tres torretas
            hintText.text = "Max limit of 3 turrets surpassed.";

            sEffectSource.Play();

            return;
        }

        shop.SetActive(true);

        //Activa los botones pertinentes

        GameManager.PreparationPhase = false;

        activateShopButtons();

        pauseButton.SetActive(true);

        gameObject.SetActive(false);
    }

    //Métodos para Toggles------------
    public void SetStandardTurret()
    {
        if (standardTurretActive)
        {
            standardTurretActive = false;
            activeTurretsCount--;
        }
        else
        {
            standardTurretActive = true;
            activeTurretsCount++;
        }

        updateHintText();
    }

    public void SetMissileTurret()
    {
        if (missileTurretActive)
        {
            missileTurretActive = false;
            activeTurretsCount--;
        }
        else
        {
            missileTurretActive = true;
            activeTurretsCount++;
        }

        updateHintText();
    }

    public void SetLaserTurret()
    {
        if (laserTurretActive)
        {
            laserTurretActive = false;
            activeTurretsCount--;
        }
        else
        {
            laserTurretActive = true;
            activeTurretsCount++;
        }

        updateHintText();
    }

    public void SetMachineGunTurret()
    {
        if (machineGunTurretActive)
        {
            machineGunTurretActive = false;
            activeTurretsCount--;
        }
        else
        {
            machineGunTurretActive = true;
            activeTurretsCount++;
        }

        updateHintText();
    }


    //-------------------------------
    private void updateHintText()
    {
        hintText.text = activeTurretsCount.ToString() + "/3";
    }


    private void activateShopButtons()
    {
        standardButton.SetActive(standardTurretActive);
        missileButton.SetActive(missileTurretActive);
        laserButton.SetActive(laserTurretActive);
        machineGunButton.SetActive(machineGunTurretActive);
    }
}
