using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [Header("Car")]
    public GameObject gCar;
    
    [Header("Gasoline bar")] // 0.2 - 1.0
    public GameObject gGasolineBar;
    
    [Header("Damage bar")] // 0.2 - 1.0
    public GameObject gDamageBar;
    
    [Header("Recon")]
    public GameObject gFreezeButton;
    public GameObject gRestart;
    public GameObject gSettings;
    
    [Header("Ingame")]
    public GameObject gIngame;
    public TMP_Text tText;

    [Header("Game Over")]
    public GameObject gCarView;
    public GameObject gFreeView;
    public GameObject gGameOver;
    public TMP_Text tDefeat;
    public GameObject gRestartButton;

    [Header("Settings")] 
    public GameObject gSettingsCanvas;
    public GameObject gReturn;
    public GameObject gSettingsMusic;
    public GameObject gSettingsEffects;
    public GameObject gSettingsSound;
    public GameObject gSettingsLanguage;
    public GameObject gExit;
    
    [Header("Audio Manager")] 
    public GameObject gAudioManager;
    
    private Button _bReturn;
    private Button _bExit;
    
    private Button _bRestart;
    private Button _bSettings;
    private Slider _sSettingsMusic;
    private Slider _sSettingsEffects;
    private Slider _sSettingsSound;
    private TMP_Dropdown _dSettingsLanguage;
    
    private AudioSource _aSource;
    
    private Button _RestartButton;
    
    private Button _FreezeButton;
    private float _fTime = 0.0f;
    private bool _hasStarted = false;
    
    private Controller _Controller;
    private InputManager _InputManager;
    
    private float _fGasoline = 100.0f;
    private float _fDamage = 0.0f;
    private bool _isOut = false;
    
    private Slider _GasSlider;
    private Slider _DmgSlider;

    private char _cLanguage = 'e';
    
    public float GetTime => _fTime;
    
    private void _SetText()
    {
        string zeroU = "";
        string zeroD = zeroU;
        if (Mathf.FloorToInt(_fTime % 60) < 10) zeroU = "0"; 
        if (Mathf.FloorToInt(_fTime / 60) < 10) zeroD = "0";
        tText.text = $"{zeroD}{Mathf.FloorToInt(_fTime / 60).ToString()}:{zeroU}{Mathf.FloorToInt(_fTime % 60).ToString()}";
    }
    
    private void _PauseGame() { gCar.GetComponent<Rigidbody>().isKinematic = true; }

    private void _ResumeGame()
    {
        gCar.GetComponent<Rigidbody>().isKinematic = false; 
        _hasStarted = true;
        gIngame.SetActive(true);
    }
    
    private void _ReloadScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    
    public void OilUpdate() { _GasSlider.value = _fGasoline; }
    
    public void DmgUpdate() { _DmgSlider.value = _fDamage; }

    private void _GameOver(bool D, bool F, bool O)
    {
        gFreeView.SetActive(false);
        gFreeView.GetComponent<AudioListener>().enabled = false;
        gCarView.SetActive(true);
        gCarView.GetComponent<AudioListener>().enabled = true;
        gIngame.SetActive(false);
        gFreezeButton.SetActive(false);
        gGameOver.SetActive(true);
        if (D) tDefeat.text = "You lost because your car has suffered many breakdowns";
        else if (F) tDefeat.text = "You lost because you ran out of fuel";
        //else if (O) tDefeat.text = "You have lost because you have gone completely off track";
    }
    
    private void _Settings() { gSettingsCanvas.SetActive(true); }
    
    private void _Exit() { gSettingsCanvas.SetActive(false); }

    private void _Return()
    {
        PlayerPrefs.SetString("Language", _cLanguage.ToString());
        PlayerPrefs.SetFloat("Music", _sSettingsMusic.value);
        PlayerPrefs.SetFloat("Effects", _sSettingsEffects.value);
        PlayerPrefs.SetFloat("Sound", _sSettingsSound.value);
        SceneManager.LoadScene("Prefs");
    }

    private void _InitButtons()
    {
        _Controller = GameObject.FindGameObjectWithTag("AI").GetComponent<Controller>();
        _InputManager = GameObject.FindGameObjectWithTag("AI").GetComponent<InputManager>();
        
        _FreezeButton = gFreezeButton.GetComponent<Button>();        
        _FreezeButton.onClick.AddListener(() => _ResumeGame());
        
        _GasSlider = gGasolineBar.GetComponent<Slider>();
        _DmgSlider = gDamageBar.GetComponent<Slider>();
        
        _RestartButton = gRestartButton.GetComponent<Button>();        
        _RestartButton.onClick.AddListener(() => _ReloadScene());
        
        _bRestart = gRestart.GetComponent<Button>();
        _bRestart.onClick.AddListener(() => _ReloadScene());
        
        _bExit = gExit.GetComponent<Button>();
        _bExit.onClick.AddListener(() => _Exit());
        
        _bSettings = gSettings.GetComponent<Button>();
        _bSettings.onClick.AddListener(() => _Settings());
        
        _bReturn = gReturn.GetComponent<Button>();
        _bReturn.onClick.AddListener(() => _Return());
        
        _aSource = gAudioManager.GetComponent<AudioSource>();
        
        _sSettingsMusic = gSettingsMusic.GetComponent<Slider>();
        _sSettingsMusic.onValueChanged.AddListener(delegate { _aSource.volume = _sSettingsMusic.value; });
        
        _sSettingsEffects = gSettingsEffects.GetComponent<Slider>();
        _sSettingsEffects.onValueChanged.AddListener(delegate {  });
        
        _sSettingsSound = gSettingsSound.GetComponent<Slider>();
        _sSettingsSound.onValueChanged.AddListener(delegate { AudioListener.volume = _sSettingsSound.value; });
        
        _dSettingsLanguage = gSettingsLanguage.GetComponent<TMP_Dropdown>();
        _dSettingsLanguage.onValueChanged.AddListener(delegate
        {
            if (_dSettingsLanguage.value == 0) _cLanguage = 'e'; else _cLanguage = 's';
        });
    }
    
    void _LoadPlayerPrefs()
    {
        _cLanguage = Convert.ToChar(PlayerPrefs.GetString("Language"));
        if (_cLanguage == 'e')
            _dSettingsLanguage.value = 0;
        else
            _dSettingsLanguage.value = 1;
    }
    
    private void _InitAudio()
    {
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
        Application.targetFrameRate = 60;
        _InitButtons();
        _LoadPlayerPrefs();
        _InitAudio();
        _PauseGame();
    }

    private void Start()
    {
        StartCoroutine(GetOil());
    }
    
    private void FixedUpdate()
    {
        _fDamage = _Controller.GetDmg;
        DmgUpdate();
        bool isDamaged = _fDamage >= 100.0f;
        bool noFuel = _fGasoline < 0.0f;
        if (isDamaged || noFuel) // || _isOut
        {
            _GameOver(isDamaged, noFuel, _isOut);
            _Controller.StopCar();
        }
    }
    
    private void Update()
    {
        if (_hasStarted) { _fTime += Time.deltaTime; _SetText(); }
    }

    private IEnumerator GetOil(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            _fGasoline = _Controller.GetGas;
            _isOut = _Controller.IsOutOfTrack;
            OilUpdate();
        }
    }
}