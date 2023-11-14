using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Studio Canvas")] 
    public GameObject gStudioCanvas;
    public GameObject gStudioExit;
    public GameObject gStudioExitOrder;
    public GameObject gStudioExitY;
    public GameObject gStudioExitN;
    public GameObject gStudioSettings;
    public GameObject gStudioEnter;

    [Header("Language Canvas")] 
    public GameObject gLanguageCanvas;
    public GameObject gLanguageExit;
    public GameObject gLanguageExitOrder;
    public GameObject gLanguageExitY;
    public GameObject gLanguageExitN;
    public GameObject gLanguageSettings;
    public GameObject gLanguageEnglish;
    public GameObject gLanguageSpanish;

    [Header("Game Canvas")] 
    public GameObject gGameCanvas;
    public GameObject gGameExit;
    public GameObject gGameExitOrder;
    public GameObject gGameExitY;
    public GameObject gGameExitN;
    public GameObject gGameSettings;
    public GameObject gGameContinue;

    [Header("Selector Canvas")] 
    public GameObject gSelectorCanvas;
    public GameObject gSelectorExit;
    public GameObject gSelectorExitOrder;
    public GameObject gSelectorExitY;
    public GameObject gSelectorExitN;
    public GameObject gSelectorSettings;
    public GameObject gSelectorSingIn;
    public GameObject gSelectorLogIn;

    [Header("Sign In Canvas")] 
    public GameObject gSignInCanvas;
    public GameObject gSignInExit;
    public GameObject gSignInExitOrder;
    public GameObject gSignInExitY;
    public GameObject gSignInExitN;
    public GameObject gSignInSettings;
    public GameObject gSignInBack;
    public GameObject gSignInContinue;

    [Header("Log In Canvas")] 
    public GameObject gLogInCanvas;
    public GameObject gLogInExit;
    public GameObject gLogInExitOrder;
    public GameObject gLogInExitY;
    public GameObject gLogInExitN;
    public GameObject gLogInSettings;
    public GameObject gLogInBack;
    public GameObject gLogInContinue;

    [Header("Log In Canvas")] 
    public GameObject gSettingsCanvas;
    public GameObject gSettingsClose;
    public GameObject gSettingsApply;
    
    //Buttons
    private Button _bStudioExit;
    private Button _bStudioExitY;
    private Button _bStudioExitN;
    private Button _bStudioSettings;
    private Button _bStudioEnter;

    private Button _bLanguageExit;
    private Button _bLanguageExitY;
    private Button _bLanguageExitN;
    private Button _bLanguageSettings;
    private Button _bLanguageEnglish;
    private Button _bLanguageSpanish;
    
    private Button _bGameExit;
    private Button _bGameExitY;
    private Button _bGameExitN;
    private Button _bGameSettings;
    private Button _bGameContinue;
    
    private Button _bSelectorExit;
    private Button _bSelectorExitY;
    private Button _bSelectorExitN;
    private Button _bSelectorSettings;
    private Button _bSelectorSignIn;
    private Button _bSelectorLogIn;

    private Button _bSignInExit;
    private Button _bSignInExitY;
    private Button _bSignInExitN;
    private Button _bSignInSettings;
    private Button _bSignInBack;
    private Button _bSignInContinue;

    private Button _bLogInExit;
    private Button _bLogInExitY;
    private Button _bLogInExitN;
    private Button _bLogInSettings;
    private Button _bLogInBack;
    private Button _bLogInContinue;
    
    private Button _bSettingsClose;
    private Button _bSettingsApply;

    

    private List<GameObject> _CanvasList = new List<GameObject>();
    private int _iCurrentCanvas = 0;
    private int _iLastCanvas = 0;
    private char _cLanguage = 'e';
    
    private void _ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void _EnterSettings(GameObject canvas)
    {
        _iLastCanvas = _iCurrentCanvas;
        canvas.SetActive(false);
        gSettingsCanvas.SetActive(true);
    }
    
    private void _ExitSettings(GameObject canvas)
    {
        gSettingsCanvas.SetActive(false);
        canvas.SetActive(true);
    }

    private void _StudioNext()
    {
        gStudioCanvas.SetActive(false);
        gLanguageCanvas.SetActive(true);
    }
    
    private void _LanguageNext(char language)
    {
        gLanguageCanvas.SetActive(false);
        gGameCanvas.SetActive(true);
        _cLanguage = language;
    }
    
    private void _GameNext()
    {
        gGameCanvas.SetActive(false);
        gSelectorCanvas.SetActive(true);
    }
    
    private void _LogNext(char l_s)
    {
        gSelectorCanvas.SetActive(false);
        
        if (l_s == 'l')
            gLogInCanvas.SetActive(true);
        if (l_s == 's')
            gSignInCanvas.SetActive(true);
    }

    private void _BackLog(GameObject canvas)
    {
        canvas.SetActive(false);
        gSelectorCanvas.SetActive(true);
    }

    private void _LoadPrefs() { SceneManager.LoadScene("Prefs"); }
    
    private void _InitButtons()
    {
        //Get buttons
        _bStudioExit = gStudioExit.GetComponent<Button>();
        _bStudioExitY = gStudioExitY.GetComponent<Button>();
        _bStudioExitN = gStudioExitN.GetComponent<Button>();
        _bStudioSettings = gStudioSettings.GetComponent<Button>();
        _bStudioEnter = gStudioEnter.GetComponent<Button>();
        
        _bLanguageExit = gLanguageExit.GetComponent<Button>();
        _bLanguageExitY = gLanguageExitY.GetComponent<Button>();
        _bLanguageExitN = gLanguageExitN.GetComponent<Button>();
        _bLanguageSettings = gLanguageSettings.GetComponent<Button>();
        _bLanguageEnglish = gLanguageEnglish.GetComponent<Button>();
        _bLanguageSpanish = gLanguageSpanish.GetComponent<Button>();
            
        _bGameExit = gGameExit.GetComponent<Button>();
        _bGameExitY = gGameExitY.GetComponent<Button>();
        _bGameExitN = gGameExitN.GetComponent<Button>();
        _bGameSettings = gGameSettings.GetComponent<Button>();
        _bGameContinue = gGameContinue.GetComponent<Button>();
        
        _bSelectorExit = gSelectorExit.GetComponent<Button>();
        _bSelectorExitY = gSelectorExitY.GetComponent<Button>();
        _bSelectorExitN = gSelectorExitN.GetComponent<Button>();
        _bSelectorSettings = gSelectorSettings.GetComponent<Button>();
        _bSelectorSignIn = gSelectorSingIn.GetComponent<Button>();
        _bSelectorLogIn = gSelectorLogIn.GetComponent<Button>();
        
        _bSignInExit = gSignInExit.GetComponent<Button>();
        _bSignInExitY = gSignInExitY.GetComponent<Button>();
        _bSignInExitN = gSignInExitN.GetComponent<Button>();
        _bSignInSettings = gSignInSettings.GetComponent<Button>();
        _bSignInBack = gSignInBack.GetComponent<Button>();
        _bSignInContinue = gSignInContinue.GetComponent<Button>();
        
        _bLogInExit = gLogInExit.GetComponent<Button>();
        _bLogInExitY = gLogInExitY.GetComponent<Button>();
        _bLogInExitN = gLogInExitN.GetComponent<Button>();
        _bLogInSettings = gLogInSettings.GetComponent<Button>();
        _bLogInBack = gLogInBack.GetComponent<Button>();
        _bLogInContinue = gLogInContinue.GetComponent<Button>();
        
        _bSettingsClose = gSettingsClose.GetComponent<Button>();
        _bSettingsApply = gSettingsApply.GetComponent<Button>();
        
        //Actions
        _bStudioExit.onClick.AddListener(() => gStudioExitOrder.SetActive(true));
        _bStudioExitY.onClick.AddListener(() => _ExitGame());
        _bStudioExitN.onClick.AddListener(() => gStudioExitOrder.SetActive(false));
        _bStudioSettings.onClick.AddListener(() => _EnterSettings(gStudioCanvas));
        _bStudioEnter.onClick.AddListener(() => _StudioNext());
        
        _bLanguageExit.onClick.AddListener(() => gLanguageExitOrder.SetActive(true));
        _bLanguageExitY.onClick.AddListener(() => _ExitGame());
        _bLanguageExitN.onClick.AddListener(() => gLanguageExitOrder.SetActive(false));
        _bLanguageSettings.onClick.AddListener(() => _EnterSettings(gLanguageCanvas));
        _bLanguageEnglish.onClick.AddListener(() => _LanguageNext('e'));
        _bLanguageSpanish.onClick.AddListener(() => _LanguageNext('s'));
        
        _bGameExit.onClick.AddListener(() => gGameExitOrder.SetActive(true));
        _bGameExitY.onClick.AddListener(() => _ExitGame());
        _bGameExitN.onClick.AddListener(() => gGameExitOrder.SetActive(false));
        _bGameSettings.onClick.AddListener(() => _EnterSettings(gGameCanvas));
        _bGameContinue.onClick.AddListener(() => _GameNext());
        
        _bSelectorExit.onClick.AddListener(() => gSelectorExitOrder.SetActive(true));
        _bSelectorExitY.onClick.AddListener(() => _ExitGame());
        _bSelectorExitN.onClick.AddListener(() => gSelectorExitOrder.SetActive(false));
        _bSelectorSettings.onClick.AddListener(() => _EnterSettings(gSelectorCanvas));
        _bSelectorSignIn.onClick.AddListener(() => _LogNext('s'));
        _bSelectorLogIn.onClick.AddListener(() => _LogNext('l'));
        
        _bSignInExit.onClick.AddListener(() => gSignInExitOrder.SetActive(true));
        _bSignInExitY.onClick.AddListener(() => _ExitGame());
        _bSignInExitN.onClick.AddListener(() => gSignInExitOrder.SetActive(false));
        _bSignInSettings.onClick.AddListener(() => _EnterSettings(gSignInCanvas));
        _bSignInBack.onClick.AddListener(() => _BackLog(gSignInCanvas));
        _bSignInContinue.onClick.AddListener(() => _LoadPrefs());

        _bLogInExit.onClick.AddListener(() => gLogInExitOrder.SetActive(true));
        _bLogInExitY.onClick.AddListener(() => _ExitGame());
        _bLogInExitN.onClick.AddListener(() => gLogInExitOrder.SetActive(false));
        _bLogInSettings.onClick.AddListener(() => _EnterSettings(gLogInCanvas));
        _bLogInBack.onClick.AddListener(() => _BackLog(gLogInCanvas));
        _bLogInContinue.onClick.AddListener(() => _LoadPrefs());
        
        _bSettingsClose.onClick.AddListener(() => _ExitSettings(_CanvasList[_iLastCanvas]));
        _bSettingsApply.onClick.AddListener(() => _ExitSettings(_CanvasList[_iLastCanvas]));
    }

    private void _InitCanvasList()
    {
        _CanvasList.Add(gStudioCanvas);
        _CanvasList.Add(gLanguageCanvas);
        _CanvasList.Add(gGameCanvas);
        _CanvasList.Add(gSelectorCanvas);
        _CanvasList.Add(gSignInCanvas);
        _CanvasList.Add(gLogInCanvas);
        _CanvasList.Add(gSettingsCanvas);
    }
    
    private void Awake()
    {
        _InitButtons();
        _InitCanvasList();
    }

    private void Update()
    {
        for (int i = 0; i < _CanvasList.Count; i++)
            if (_CanvasList[i].activeSelf)
                _iCurrentCanvas = i;
    }
}