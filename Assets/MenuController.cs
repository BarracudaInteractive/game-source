using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject splashScreen;
    public GameObject languageSelector;
    public GameObject midScreen;
    public GameObject popUpInicioSesion;
    public GameObject popUpRegistro;
    public GameObject popUpLogIn;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject legSelection;
    public GameObject stageSelection;
    public Button spanishB;
    //public Button english;
    public Button pressToContinue;
    public Button backIS;
    public Button primeraVez;
    public Button iniciarSesion;
    public Button backR;
    public Button continuarR;
    public Button backL;
    public Button continuarL;
    public Button jugar;
    public Button settingsB;
    //public Button Perfil;
    public Button cambiarCuenta;
    public Button exitS;
    //public Button creditos;
    //public Button spanishS;
    //public Button englishS;
    public Button selectLeg;
    public Button exitLS;
    public Button backSS;
    public Button exitSS;
    public Button empezar;

    private bool screenActive = false;

    void Start()
    {
        // Activa el splashScreen al inicio del juego
        splashScreen.SetActive(true);
        languageSelector.SetActive(false);
        midScreen.SetActive(false);
        popUpInicioSesion.SetActive(false);
        popUpLogIn.SetActive(false);
        popUpRegistro.SetActive(false);
        mainMenu.SetActive(false);
        settings.SetActive(false);
        legSelection.SetActive(false);
        stageSelection.SetActive(false);

        // Asocia la función CambiarPantalla al evento onClick del botón
        spanishB.onClick.AddListener(spanish);
        pressToContinue.onClick.AddListener(midScreenContinue);
        backIS.onClick.AddListener(backToMS);
        primeraVez.onClick.AddListener(toRegistro);
        iniciarSesion.onClick.AddListener(toLogIn);
        backR.onClick.AddListener(toISfromR);
        continuarR.onClick.AddListener(toMMfromR);
        backL.onClick.AddListener(toISfromL);
        continuarL.onClick.AddListener(toMMfromL);
        jugar.onClick.AddListener(toLS);
        settingsB.onClick.AddListener(toSettings);
        cambiarCuenta.onClick.AddListener(toISfromS);
        exitS.onClick.AddListener(backToMMfromS);
        selectLeg.onClick.AddListener(toSS);
        exitLS.onClick.AddListener(backToMMfromLS);
        backSS.onClick.AddListener(backToLS);
        exitSS.onClick.AddListener(backToMMfromSS);
        empezar.onClick.AddListener(toPlay);
}

    void spanish()
    {
            languageSelector.SetActive(false);
            midScreen.SetActive(true);
    }

    void midScreenContinue()
    {
        midScreen.SetActive(false);
        popUpInicioSesion.SetActive(true);
    }

    void backToMS()
    {
        popUpInicioSesion.SetActive(false);
        midScreen.SetActive(true);
    }

    void toRegistro()
    {
        popUpInicioSesion.SetActive(false);
        popUpRegistro.SetActive(true);
    }

    void toLogIn()
    {
        popUpInicioSesion.SetActive(false);
        popUpLogIn.SetActive(true);
    }

    void toISfromR()
    {
        popUpRegistro.SetActive(false);
        popUpInicioSesion.SetActive(true);
    }

    void toMMfromR()
    {
        popUpRegistro.SetActive(false);
        mainMenu.SetActive(true);
    }

    void toISfromL()
    {
        popUpLogIn.SetActive(false);
        popUpInicioSesion.SetActive(true);
    }

    void toMMfromL()
    {
        popUpLogIn.SetActive(false);
        mainMenu.SetActive(true);
    }

    void toLS()
    {
        mainMenu.SetActive(false);
        legSelection.SetActive(true);
    }

    void toSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    void backToMMfromS()
    {
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    void toISfromS()
    {
        settings.SetActive(false);
        popUpInicioSesion.SetActive(true);
    }
    void toSS()
    {
        legSelection.SetActive(false);
        stageSelection.SetActive(true);
    }
    void backToMMfromLS()
    {
        legSelection.SetActive(false);
        mainMenu.SetActive(true);
    }
    void backToLS()
    {
        stageSelection.SetActive(false);
        legSelection.SetActive(true);
    }
    void backToMMfromSS()
    {
        stageSelection.SetActive(false);
        mainMenu.SetActive(true);
    }
    void toPlay()
    {
        SceneManager.LoadScene("Prefs");
    }

    void Update()
    {
        if (!screenActive && splashScreen.activeSelf)
        {
            // Verifica si se presiona cualquier tecla o se hace clic en el ratón
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                // Desactiva el splashScreen
                splashScreen.SetActive(false);

                // Activa el siguienteScreen
                languageSelector.SetActive(true);

                // Marca que la pantalla ha sido activada
                screenActive = true;
            }
        }

    }
}
