using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scri : MonoBehaviour
{
    public GameObject splashScreen;
    public GameObject languageSelector;
    public GameObject midScreen;

    private bool screenActive = false;

    /*private void _NextScene()
    {
        logo.SetActive(true);
        idioma.SetActive(false);
    }
    
    private void LoadGame() { SceneManager.LoadScene("Prefs"); }
    
    void Awake()
    {
        logo.SetActive(false);
        _bEnglish = english.GetComponent<Button>();
        _bEnglish.onClick.AddListener(() => _NextScene());
    }*/

    void Update()
    {
        if (!screenActive && splashScreen.activeSelf)
        {
            // Verifica si se presiona cualquier tecla o se hace clic en el ratón
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
            {
                // Desactiva el splashScreen
                languageSelector.SetActive(false);

                // Activa el siguienteScreen
                languageSelector.SetActive(true);

                // Marca que la pantalla ha sido activada
                screenActive = true;
            }
        }

    }
}
