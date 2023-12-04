using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PrefsManager : MonoBehaviour
{
    [Header("Camera")] public GameObject gCameraObject;
    public GameObject gFourthCameraPosition;
    public GameObject gThirdCameraPosition;
    public GameObject gSecondCameraPosition;
    public GameObject gStartCameraPosition;

    [Header("Deafault Canvas")] public GameObject gDeafaultCanvas;
    public GameObject gDefaultSettings;
    public GameObject gDefaultExit;
    public GameObject gDefaultExitOrder;
    public GameObject gDefaultExitY;
    public GameObject gDefaultExitN;
    public GameObject gDefaultBack;
    public GameObject gDefaultProfile;
    
    [Header("Vehicle Canvas")] public GameObject gVehicleSelectCanvas;
    public GameObject gVehicleSettings;
    public GameObject gVehicleExit;
    public GameObject gVehicleExitOrder;
    public GameObject gVehicleExitY;
    public GameObject gVehicleExitN;
    public GameObject gVehicleBack;
    public VehicleList ListOfVehicles;
    public GameObject gToRotate;
    
    [Header("Leg Canvas")] public GameObject gMapSelectorCanvas;
    public GameObject gLegSettings;
    public GameObject gLegExit;
    public GameObject gLegExitOrder;
    public GameObject gLegExitY;
    public GameObject gLegExitN;
    public GameObject gLegBack;
    public GameObject gLegPlay;
    
    [Header("Stage Canvas")] public GameObject gStageCanvas;
    public GameObject gStageSettings;
    public GameObject gStageExit;
    public GameObject gStageExitOrder;
    public GameObject gStageExitY;
    public GameObject gStageExitN;
    public GameObject gStageBack;
    public GameObject gStageOne;
    public GameObject gStageTwo;
    public GameObject gStageThree;
    public GameObject gStageRight;
    public GameObject gStageLeft;
    public GameObject gStageRace;
    
    [Header("Settings Canvas")] public GameObject gSettingsCanvas;
    public GameObject gSettingsClose;
    public GameObject gSettingsApply;
    public GameObject gSettingsMusic;
    public GameObject gSettingsEffects;
    public GameObject gSettingsSound;
    public GameObject gSettingsLanguage;
    public GameObject gSettingsCredits;

    [Header("Credits Canvas")] public GameObject gCreditsCanvas;
    public GameObject gCreditsClose;
    
    public GameObject gAudioManager;
    
    //Objects
    private Button _bDefaultSettings;
    private Button _bDefaultExit;
    private Button _bDefaultExitY;
    private Button _bDefaultExitN;
    private Button _bDefaultBack;
    private Button _bDefaultProfile;
    
    private Button _bVehicleSettings;
    private Button _bVehicleExit;
    private Button _bVehicleExitY;
    private Button _bVehicleExitN;
    private Button _bVehicleBack;
    
    private Button _bLegSettings;
    private Button _bLegExit;
    private Button _bLegExitY;
    private Button _bLegExitN;
    private Button _bLegBack;
    
    private Button _bStageSettings;
    private Button _bStageExit;
    private Button _bStageExitY;
    private Button _bStageExitN;
    private Button _bStageBack;
    private Button _bStageRight;
    private Button _bStageLeft;
    private Button _bStageRace;
    
    private Button _bSettingsClose;
    private Button _bSettingsApply;
    private Slider _sSettingsMusic;
    private Slider _sSettingsEffects;
    private Slider _sSettingsSound;
    private TMP_Dropdown _dSettingsLanguage;
    private Button _bSettingsCredits;

    private Button _bCreditsClose;
    
    private AudioSource _aSourceMusic;
    private SoundManager _SoundManager;
    
    private float fLerpTime = 2.0f;
    private float _fRotateSpeed = 5f;
    private bool _fourth;
    private bool _third;
    private bool _second;
    private bool _start;
    private Button _bPlayButton;
    
    private List<GameObject> _CanvasList = new List<GameObject>();
    private List<GameObject> _StagesList = new List<GameObject>();
    private int _iCurrentCanvas = 0;
    private int _iLastCanvas = 0;
    private char _cLanguage = 'e';
    private int _iStage = 0;

    public void ChangeScreen() { Screen.fullScreen = !Screen.fullScreen; }
    
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
    
    private void _StopCar() { GameObject.FindGameObjectWithTag("AI").GetComponent<Rigidbody>().isKinematic = true; }

    public void StageCanvas()
    {
        gDeafaultCanvas.SetActive(false);
        gVehicleSelectCanvas.SetActive(false);
        gMapSelectorCanvas.SetActive(false);
        gStageCanvas.SetActive(true);
        _start = false;
        _second = false;
        _third = false;
        _fourth = true;
    }
    
    public void MapCanvas()
    {
        gDeafaultCanvas.SetActive(false);
        gVehicleSelectCanvas.SetActive(false);
        gMapSelectorCanvas.SetActive(true);
        gStageCanvas.SetActive(false);
        _start = false;
        _second = false;
        _third = true;
        _fourth = false;
    }

    public void VehicleCanvas()
    {
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(false);
        gVehicleSelectCanvas.SetActive(true);
        gStageCanvas.SetActive(false);
        _start = true;
        _second = false;
        _third = false;
        _fourth = false;
    }

    public void DefaultCanvas()
    {
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(true);
        gVehicleSelectCanvas.SetActive(false);
        gStageCanvas.SetActive(false);
        _second = true;
        _start = false;
        _third = false;
        _fourth = false;
    }
    
    private void _CameraTransition()
    {
        if (_start)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position,
                gSecondCameraPosition.transform.position, fLerpTime * Time.deltaTime);
        
        if (_second)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position,
                gStartCameraPosition.transform.position, fLerpTime * Time.deltaTime);
        
        if (_third)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position, 
                gThirdCameraPosition.transform.position, fLerpTime * Time.deltaTime);
        
        if (_fourth)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position, 
                gFourthCameraPosition.transform.position, fLerpTime * Time.deltaTime);
    }

    private void _LoadMenu() 
    { 
        PlayerPrefs.SetString("Language", _cLanguage.ToString());
        PlayerPrefs.SetFloat("Music", _sSettingsMusic.value);
        PlayerPrefs.SetFloat("Effects", _sSettingsEffects.value);
        PlayerPrefs.SetFloat("Sound", _sSettingsSound.value);
        SceneManager.LoadScene("Menu"); 
    }

    private void _DeletePlayerPrefsLong()
    {
        string user = PlayerPrefs.GetString("User");
        string password = PlayerPrefs.GetString("Password");
        string language = PlayerPrefs.GetString("Language");
        float music = PlayerPrefs.GetFloat("Music");
        float effects = PlayerPrefs.GetFloat("Effects");
        float sound = PlayerPrefs.GetFloat("Sound");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("User", user);
        PlayerPrefs.SetString("Password", password);
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.SetFloat("Music", music);
        PlayerPrefs.SetFloat("Effects", effects);
        PlayerPrefs.SetFloat("Sound", sound);
    }
    
    private void _LoadStage()
    {
        _DeletePlayerPrefsLong();
        if (_iStage == 0) SceneManager.LoadScene("Day1M");
        else if (_iStage == 1) SceneManager.LoadScene("Day1N");
        else if (_iStage == 2) SceneManager.LoadScene("Day2A");
    }

    public void _BackLog(GameObject canvas)
    {
        if (_iCurrentCanvas == 0)
            _LoadMenu();
        else
        {
            canvas.SetActive(false);
            _CanvasList[_iCurrentCanvas - 1].SetActive(true);
        }
    }

    private void _ChangeStage(int val)
    {
        _iStage += val;
        if (_iStage < 0)
            _iStage = 2;
        _iStage %= 3;
        for (int i = 0; i < 3; i++)
        {
            if (i == _iStage)
                _StagesList[_iStage].SetActive(true);
            else
                _StagesList[i].SetActive(false);
        }
    }

    private void _LoadPlayerPrefs()
    {
        _cLanguage = Convert.ToChar(PlayerPrefs.GetString("Language"));
        if (_cLanguage == 'e')
            _dSettingsLanguage.value = 0;
        else if (_cLanguage == 's')
            _dSettingsLanguage.value = 1;
    }
    
    private void _InitButtons()
    {
        //Get buttons
        _bDefaultBack = gDefaultBack.GetComponent<Button>();
        _bDefaultExit = gDefaultExit.GetComponent<Button>();
        _bDefaultExitY = gDefaultExitY.GetComponent<Button>();
        _bDefaultExitN = gDefaultExitN.GetComponent<Button>();
        _bDefaultSettings = gDefaultSettings.GetComponent<Button>();
        _bDefaultProfile = gDefaultProfile.GetComponent<Button>();
        
        _bVehicleBack = gVehicleBack.GetComponent<Button>();
        _bVehicleExit = gVehicleExit.GetComponent<Button>();
        _bVehicleExitY = gVehicleExitY.GetComponent<Button>();
        _bVehicleExitN = gVehicleExitN.GetComponent<Button>();
        _bVehicleSettings = gVehicleSettings.GetComponent<Button>();
        
        _bLegBack = gLegBack.GetComponent<Button>();
        _bLegExit = gLegExit.GetComponent<Button>();
        _bLegExitY = gLegExitY.GetComponent<Button>();
        _bLegExitN = gLegExitN.GetComponent<Button>();
        _bLegSettings = gLegSettings.GetComponent<Button>();
        _bSettingsCredits = gSettingsCredits.GetComponent<Button>();

        _bCreditsClose = gCreditsClose.GetComponent<Button>();
        
        _bStageBack = gStageBack.GetComponent<Button>();
        _bStageExit = gStageExit.GetComponent<Button>();
        _bStageExitY = gStageExitY.GetComponent<Button>();
        _bStageExitN = gStageExitN.GetComponent<Button>();
        _bStageSettings = gStageSettings.GetComponent<Button>();
        _bStageRight = gStageRight.GetComponent<Button>();
        _bStageLeft = gStageLeft.GetComponent<Button>();
        _bStageRace = gStageRace.GetComponent<Button>();
        
        _bSettingsClose = gSettingsClose.GetComponent<Button>();
        _bSettingsApply = gSettingsApply.GetComponent<Button>();
        _sSettingsMusic = gSettingsMusic.GetComponent<Slider>();
        _sSettingsEffects = gSettingsEffects.GetComponent<Slider>();
        _sSettingsSound = gSettingsSound.GetComponent<Slider>();
        _dSettingsLanguage = gSettingsLanguage.GetComponent<TMP_Dropdown>();
        
        //Actions
        _bDefaultBack.onClick.AddListener(() => _BackLog(gDeafaultCanvas));
        _bDefaultExit.onClick.AddListener(() => gDefaultExitOrder.SetActive(true));
        _bDefaultExitY.onClick.AddListener(() => _ExitGame());
        _bDefaultExitN.onClick.AddListener(() => gDefaultExitOrder.SetActive(false));
        _bDefaultSettings.onClick.AddListener(() => _EnterSettings(gDeafaultCanvas));
        //_bDefaultProfile.onClick.AddListener(() => );
        
        _bVehicleBack.onClick.AddListener(() => _BackLog(gVehicleSelectCanvas));
        _bVehicleExit.onClick.AddListener(() => gVehicleExitOrder.SetActive(true));
        _bVehicleExitY.onClick.AddListener(() => _ExitGame());
        _bVehicleExitN.onClick.AddListener(() => gVehicleExitOrder.SetActive(false));
        _bVehicleSettings.onClick.AddListener(() => _EnterSettings(gVehicleSelectCanvas));
        
        _bLegBack.onClick.AddListener(() => _BackLog(gMapSelectorCanvas));
        _bLegExit.onClick.AddListener(() => gLegExitOrder.SetActive(true));
        _bLegExitY.onClick.AddListener(() => _ExitGame());
        _bLegExitN.onClick.AddListener(() => gLegExitOrder.SetActive(false));
        _bLegSettings.onClick.AddListener(() => _EnterSettings(gMapSelectorCanvas));
        
        _bStageBack.onClick.AddListener(() => _BackLog(gStageCanvas));
        _bStageExit.onClick.AddListener(() => gStageExitOrder.SetActive(true));
        _bStageExitY.onClick.AddListener(() => _ExitGame());
        _bStageExitN.onClick.AddListener(() => gStageExitOrder.SetActive(false));
        _bStageSettings.onClick.AddListener(() => _EnterSettings(gStageCanvas));
        _bStageRight.onClick.AddListener(() => _ChangeStage(1));
        _bStageLeft.onClick.AddListener(() => _ChangeStage(-1));
        _bStageRace.onClick.AddListener(() => _LoadStage());
        
        _SoundManager = gAudioManager.GetComponent<SoundManager>();
        
        _bSettingsClose.onClick.AddListener(() => _ExitSettings(_CanvasList[_iLastCanvas]));
        _bSettingsApply.onClick.AddListener(() => _ExitSettings(_CanvasList[_iLastCanvas]));
        _sSettingsMusic.onValueChanged.AddListener(delegate { _aSourceMusic.volume = _sSettingsMusic.value; });
        _sSettingsEffects.onValueChanged.AddListener(delegate { _SoundManager.SetVolume(_sSettingsEffects.value); });
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
        _CanvasList.Add(gDeafaultCanvas);
        _CanvasList.Add(gVehicleSelectCanvas);
        _CanvasList.Add(gMapSelectorCanvas);
        _CanvasList.Add(gStageCanvas);
        _CanvasList.Add(gSettingsCanvas);
    }
    
    private void _InitStagesList()
    {
        _StagesList.Add(gStageOne);
        _StagesList.Add(gStageTwo);
        _StagesList.Add(gStageThree);
    }

    private void _InitAudio()
    {
        _aSourceMusic = gCameraObject.GetComponent<AudioSource>();
        _sSettingsMusic.value = PlayerPrefs.GetFloat("Music");
        _sSettingsEffects.value = PlayerPrefs.GetFloat("Effects");
        _sSettingsSound.value = PlayerPrefs.GetFloat("Sound");
    }
    
    private void Awake()
    {
        Application.targetFrameRate = 60;
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(true);
        gVehicleSelectCanvas.SetActive(false);
        gStageCanvas.SetActive(false);
        _InitButtons();
        _InitCanvasList();
        _InitStagesList();
        GameObject childObject = Instantiate(ListOfVehicles.Vehicles[0], Vector3.zero, gToRotate.transform.rotation) as GameObject;
        childObject.transform.parent = gToRotate.transform;
        _StopCar();
        _LoadPlayerPrefs();
        for (int i = 0; i < 3; i++)
        {
            if (i == _iStage)
                _StagesList[_iStage].SetActive(true);
            else
                _StagesList[i].SetActive(false);
        }
    }
    
    private void Start()
    {
        _InitAudio();
    }

    private void Update()
    {
        for (int i = 0; i < _CanvasList.Count; i++)
            if (_CanvasList[i].activeSelf)
                _iCurrentCanvas = i;
    }
    
    private void FixedUpdate()
    {
        gToRotate.transform.Rotate(Vector3.up * _fRotateSpeed * Time.deltaTime);
        _CameraTransition();
    }
}