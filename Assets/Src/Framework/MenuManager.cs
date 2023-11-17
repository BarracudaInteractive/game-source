using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Camera")] public GameObject gCameraObject;
    
    [Header("Studio Canvas")] public GameObject gStudioCanvas;

    [Header("Language Canvas")] public GameObject gLanguageCanvas;
    public GameObject gLanguageExit;
    public GameObject gLanguageExitOrder;
    public GameObject gLanguageExitY;
    public GameObject gLanguageExitN;
    public GameObject gLanguageSettings;
    public GameObject gLanguageEnglish;
    public GameObject gLanguageSpanish;

    [Header("Game Canvas")] public GameObject gGameCanvas;
    public GameObject gGameExit;
    public GameObject gGameExitOrder;
    public GameObject gGameExitY;
    public GameObject gGameExitN;
    public GameObject gGameSettings;
    public GameObject gGameContinue;

    [Header("Selector Canvas")] public GameObject gSelectorCanvas;
    public GameObject gSelectorExit;
    public GameObject gSelectorExitOrder;
    public GameObject gSelectorExitY;
    public GameObject gSelectorExitN;
    public GameObject gSelectorSettings;
    public GameObject gSelectorSingIn;
    public GameObject gSelectorLogIn;

    [Header("Sign In Canvas")] public GameObject gSignInCanvas;
    public GameObject gSignInExit;
    public GameObject gSignInExitOrder;
    public GameObject gSignInExitY;
    public GameObject gSignInExitN;
    public GameObject gSignInSettings;
    public GameObject gSignInBack;
    public GameObject gSignInContinue;
    public GameObject gSignInUser;
    public GameObject gSignInPasswd;
    public GameObject gSignInAge;
    public GameObject gSignInAgeText;
    public GameObject gSignInSex;

    [Header("Log In Canvas")] public GameObject gLogInCanvas;
    public GameObject gLogInExit;
    public GameObject gLogInExitOrder;
    public GameObject gLogInExitY;
    public GameObject gLogInExitN;
    public GameObject gLogInSettings;
    public GameObject gLogInBack;
    public GameObject gLogInContinue;
    public GameObject gLogInUser;
    public GameObject gLogInPasswd;
    public GameObject gLogInError;

    [Header("Settings Canvas")] public GameObject gSettingsCanvas;
    public GameObject gSettingsClose;
    public GameObject gSettingsApply;
    public GameObject gSettingsMusic;
    public GameObject gSettingsEffects;
    public GameObject gSettingsSound;
    public GameObject gSettingsLanguage;
    public GameObject gSettingsCredits;
    
    [Header("Credits Canvas")]
    public GameObject gCreditsCanvas;
    public GameObject gCreditsClose;

    //Objects
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
    private TMP_InputField _iSignInUser;
    private TMP_InputField _iSignInPasswd;
    private Slider _sSignInAge;
    private TMP_Text _tSignInAge;
    private TMP_Dropdown _dSignInSex;

    private Button _bLogInExit;
    private Button _bLogInExitY;
    private Button _bLogInExitN;
    private Button _bLogInSettings;
    private Button _bLogInBack;
    private Button _bLogInContinue;
    private TMP_InputField _iLogInUser;
    private TMP_InputField _iLogInPasswd;

    private Button _bSettingsClose;
    private Button _bSettingsApply;
    private Slider _sSettingsMusic;
    private Slider _sSettingsEffects;
    private Slider _sSettingsSound;
    private TMP_Dropdown _dSettingsLanguage;
    private Button _bSettingsCredits;
    
    private Button _bCreditsClose;
    
    private AudioSource _aSourceMusic;
    
    private string _sFilePath;

    private List<GameObject> _CanvasList = new List<GameObject>();
    private int _iCurrentCanvas = 0;
    private int _iLastCanvas = 0;
    private char _cLanguage = 'e';
    private char _cLogOrSign = 'l';

    private void _ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private bool _FindInDB(string u, string p)
    {
        StreamReader sr = new StreamReader(_sFilePath);
        string[] line = new string[5];
        string file = "";
        while (file != null)
        {
            file = sr.ReadLine();
            if (file == null || file != "")
            {
                line = file.Split('|');
                if (line[0] == u && line[1] == p)
                {
                    sr.Close();
                    return true;
                }
            }
        }
        
        sr.Close();
        return false;
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
        if (_cLanguage == 'e')
            _dSettingsLanguage.value = 0;
        else
            _dSettingsLanguage.value = 1;
    }

    private void _GameNext()
    {
        gGameCanvas.SetActive(false);
        gSelectorCanvas.SetActive(true);
    }

    private void _LogNext(char l_s)
    {
        gSelectorCanvas.SetActive(false);
        _cLogOrSign = l_s;
        if (_cLogOrSign == 'l')
            gLogInCanvas.SetActive(true);
        if (_cLogOrSign == 's')
            gSignInCanvas.SetActive(true);
    }

    private void _BackLog(GameObject canvas)
    {
        canvas.SetActive(false);
        gSelectorCanvas.SetActive(true);
    }

    public void OnAgeChanged(Slider a) { _tSignInAge.text = $"{a.value.ToString()} y/o"; }

    private void _LoadPlayerPrefs()
    {
        _cLanguage = Convert.ToChar(PlayerPrefs.GetString("Language"));
        if (_cLanguage == 'e')
            _dSettingsLanguage.value = 0;
        else
            _dSettingsLanguage.value = 1;
    }
    
    private void _InitPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        if (_cLogOrSign == 's')
            PlayerPrefs.SetString("User", _iSignInUser.text);
        if (_cLogOrSign == 'l')
            PlayerPrefs.SetString("User", _iLogInUser.text);
        PlayerPrefs.SetString("Language", _cLanguage.ToString());
        PlayerPrefs.SetFloat("Music", _sSettingsMusic.value);
        PlayerPrefs.SetFloat("Effects", _sSettingsEffects.value);
        PlayerPrefs.SetFloat("Sound", _sSettingsSound.value);
    }
    
    private void _LoadPrefs()
    {
        if (_cLogOrSign == 's')
        {
            StreamWriter sw = new StreamWriter(_sFilePath, true);
            sw.WriteLine($"{_iSignInUser.text}|{_iSignInPasswd.text}|{_sSignInAge.value.ToString()}|{_dSignInSex.value.ToString()}|{DateTime.Now.ToString()}\n");
            sw.Close();
        }
        if (_cLogOrSign == 'l')
        {
            if (!_FindInDB(_iLogInUser.text, _iLogInPasswd.text))
            {
                gLogInError.SetActive(true);
                return;
            }
        }

        _InitPlayerPrefs();
        gLogInError.SetActive(false);
        SceneManager.LoadScene("Prefs");
    }

    private void _InitButtons()
    {
        //Get buttons
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
        _iSignInUser = gSignInUser.GetComponent<TMP_InputField>();
        _iSignInPasswd = gSignInPasswd.GetComponent<TMP_InputField>();
        _sSignInAge = gSignInAge.GetComponent<Slider>();
        _tSignInAge = gSignInAgeText.GetComponent<TMP_Text>();
        _dSignInSex = gSignInSex.GetComponent<TMP_Dropdown>();

        _bLogInExit = gLogInExit.GetComponent<Button>();
        _bLogInExitY = gLogInExitY.GetComponent<Button>();
        _bLogInExitN = gLogInExitN.GetComponent<Button>();
        _bLogInSettings = gLogInSettings.GetComponent<Button>();
        _bLogInBack = gLogInBack.GetComponent<Button>();
        _bLogInContinue = gLogInContinue.GetComponent<Button>();
        _iLogInUser = gLogInUser.GetComponent<TMP_InputField>();
        _iLogInPasswd = gLogInPasswd.GetComponent<TMP_InputField>();

        _bSettingsClose = gSettingsClose.GetComponent<Button>();
        _bSettingsApply = gSettingsApply.GetComponent<Button>();
        _sSettingsMusic = gSettingsMusic.GetComponent<Slider>();
        _sSettingsEffects = gSettingsEffects.GetComponent<Slider>();
        _sSettingsSound = gSettingsSound.GetComponent<Slider>();
        _dSettingsLanguage = gSettingsLanguage.GetComponent<TMP_Dropdown>();
        _bSettingsCredits = gSettingsCredits.GetComponent<Button>();
    
        _bCreditsClose = gCreditsClose.GetComponent<Button>();
        
        //Actions
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
        _sSettingsMusic.onValueChanged.AddListener(delegate { _aSourceMusic.volume = _sSettingsMusic.value; });
        _sSettingsSound.onValueChanged.AddListener(delegate { AudioListener.volume = _sSettingsSound.value; });
        _dSettingsLanguage.onValueChanged.AddListener
            (delegate
            {
                if (_dSettingsLanguage.value == 0) _cLanguage = 'e'; else _cLanguage = 's';
            });
        _bSettingsCredits.onClick.AddListener(() => gCreditsCanvas.SetActive(true));
        
        _bCreditsClose.onClick.AddListener(() => gCreditsCanvas.SetActive(false));
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

    private void _InitAudio()
    {
        _aSourceMusic = gCameraObject.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("User"))
        {
            _sSettingsMusic.value = PlayerPrefs.GetFloat("Music");
            _sSettingsEffects.value = PlayerPrefs.GetFloat("Effects");
            _sSettingsSound.value = PlayerPrefs.GetFloat("Sound");
        }
        else 
        {
            _sSettingsMusic.value = 0.1f;
            _sSettingsEffects.value = 0.4f;
            AudioListener.volume = 1.0f;
        }
    }
    
    private void Awake()
    {
        _sFilePath = $"{Application.dataPath}\\Src\\Framework\\FileSystem.txt";
        _InitButtons();
        _InitCanvasList();
        if (PlayerPrefs.HasKey("User"))
            _LoadPlayerPrefs();
        _InitAudio();
        Invoke("_StudioNext", 1.0f);
    }

    private void Update()
    {
        for (int i = 0; i < _CanvasList.Count; i++)
            if (_CanvasList[i].activeSelf)
                _iCurrentCanvas = i;
    }
}