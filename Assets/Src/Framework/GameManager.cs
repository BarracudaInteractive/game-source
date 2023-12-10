using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Android controllers")] public GameObject gAndroidJoystick;
    public GameObject gAndroidDPad;
    public GameObject gAndroidTutorialA;
    public GameObject gAndroidTutorialB;
    
    [Header("Car")] public GameObject gCar;

    [Header("Gasoline bar")] // 0.2 - 1.0
    public GameObject gGasolineBar;

    [Header("Damage bar")] // 0.2 - 1.0
    public GameObject gDamageBar;

    [Header("Recon")] public GameObject gReconCanvas;
    public GameObject gFreezeButton;
    public GameObject gRestart;
    public GameObject gSettings;
    public GameObject gReconAdvice;
    public GameObject gReconFeedback;
    public GameObject gReconLoadLong;
    public GameObject gReconLoadY;
    public GameObject gReconLoadN;

    [Header("Checkpoint Selection Canvas")] public GameObject gCheckpointSelectionCanvas;
    
    [Header("Ingame")] public GameObject gIngame;
    public TMP_Text tText;
    public GameObject gIngameGear;
    public GameObject gIngameKPH;
    public GameObject gIngameResumePlay;
    public GameObject gIngameNormal;
    public GameObject gIngameFast;
    public GameObject gIngameCanvas0;
    public GameObject gIngameCanvas1;
    public GameObject gIngameCanvas2;
    public GameObject gIngameCanvas3;
    public GameObject gIngameCanvas4;
    public GameObject gIngameCanvas5;
    public GameObject gCameraChange;
    public GameObject gNavigationBar;

    [Header("Game Over")] public GameObject gCarView;
    public GameObject gFreeView;
    public GameObject gGameOver;
    public TMP_Text tDefeat;
    public GameObject gRestartButton;

    [Header("Settings")] public GameObject gSettingsCanvas;
    public GameObject gReturn;
    public GameObject gSettingsMusic;
    public GameObject gSettingsEffects;
    public GameObject gSettingsSound;
    public GameObject gSettingsLanguage;
    public GameObject gExit;
    public GameObject gSettingsCredits;
    public GameObject gSettingsControls;
    public GameObject gSettingsExitControls;

    [Header("Credits Canvas")] public GameObject gCreditsCanvas;
    public GameObject gCreditsClose;

    [Header("End screen")] public GameObject gEndCanvas;
    public GameObject gEndExit;
    public GameObject gEndContinue;
    public GameObject gEndRow1Name;
    public GameObject gEndRow1Time;
    public GameObject gEndRow1Fuel;
    public GameObject gEndRow1Damage;
    public GameObject gEndRow2Name;
    public GameObject gEndRow2Time;
    public GameObject gEndRow2Fuel;
    public GameObject gEndRow2Damage;
    public GameObject gEndRow3Name;
    public GameObject gEndRow3Time;
    public GameObject gEndRow3Fuel;
    public GameObject gEndRow3Damage;
    public GameObject gEndRow4Name;
    public GameObject gEndRow4Time;
    public GameObject gEndRow4Fuel;
    public GameObject gEndRow4Damage;
    public GameObject gEndRow5Name;
    public GameObject gEndRow5Time;
    public GameObject gEndRow5Fuel;
    public GameObject gEndRow5Damage;

    [Header("Audio Manager")] public GameObject gAudioManager;

    [Header("Checkpoints")] public List<bool> SectionIsSelectedList;
    public List<int> SectionPacenoteList;
    public List<float> SectionHeightList;
    public List<float> SectionAccelerationList;
    public List<Vector3> SectionPositionsList;
    public List<Vector3> SectionRotationsList;

    private Button _bReturn;
    private Button _bExit;
    private TMP_Text _tReconAdvice;
    private Button _bReconLoadY;
    private Button _bReconLoadN;

    private Button _bRestart;
    private Button _bSettings;
    private Slider _sSettingsMusic;
    private Slider _sSettingsEffects;
    private Slider _sSettingsSound;
    private TMP_Dropdown _dSettingsLanguage;
    private Button _bSettingsCredits;
    private Button _bSettingsControls;
    private Button _bSettingsExitControls;

    private Button _bCreditsClose;

    private AudioSource _aSource;

    private Button _RestartButton;
    private Button _FreezeButton;
    private float _fTime = 0.0f;
    private float _fTimeSpeed = 1.0f;
    private bool _hasStarted = false;

    private Controller _Controller;
    private InputManager _InputManager;

    private float _fGasoline = 100.0f;
    private float _fDamage = 0.0f;
    private bool _isOut = false;

    private Slider _GasSlider;
    private Slider _DmgSlider;
    private TMP_Text _tIngameGear;
    private TMP_Text _tIngameKPH;
    private Button _bIngameResumePlay;
    private Button _bIngameNormal;
    private Button _bIngameFast;
    private Button _bEndExit;
    private Button _bEndContinue;
    private TMP_Text _tEndRow1Name;
    private TMP_Text _tEndRow1Time;
    private TMP_Text _tEndRow1Fuel;
    private TMP_Text _tEndRow1Damage;
    private TMP_Text _tEndRow2Name;
    private TMP_Text _tEndRow2Time;
    private TMP_Text _tEndRow2Fuel;
    private TMP_Text _tEndRow2Damage;
    private TMP_Text _tEndRow3Name;
    private TMP_Text _tEndRow3Time;
    private TMP_Text _tEndRow3Fuel;
    private TMP_Text _tEndRow3Damage;
    private TMP_Text _tEndRow4Name;
    private TMP_Text _tEndRow4Time;
    private TMP_Text _tEndRow4Fuel;
    private TMP_Text _tEndRow4Damage;
    private TMP_Text _tEndRow5Name;
    private TMP_Text _tEndRow5Time;
    private TMP_Text _tEndRow5Fuel;
    private TMP_Text _tEndRow5Damage;
    private CameraChange _CameraChange;
    private CameraMovComponentFH _CameraMovComponentFH;
    private CameraController _CameraController;

    private SectionManager _SectionManager;
    private SoundManager _SoundManager;

    private char _cLanguage = 'e';
    private string _sFilePath;
    private int NUM_CHECKPOINTS;
    private bool _hasEnded = false;
    
    [DllImport("__Internal")]
    private static extern bool IsMobile();
    
    public bool IsMobileWebGL()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            return IsMobile();
#endif
        return false;
    }

    public float GetGasoline => _fGasoline;

    public float GetDamage => _fDamage;

    public float GetTime => _fTime;

    public void ChangeScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void _SetText()
    {
        string zeroU = "";
        string zeroD = zeroU;
        if (Mathf.FloorToInt(_fTime % 60) < 10) zeroU = "0";
        if (Mathf.FloorToInt(_fTime / 60) < 10) zeroD = "0";
        tText.text =
            $"{zeroD}{Mathf.FloorToInt(_fTime / 60).ToString()}:{zeroU}{Mathf.FloorToInt(_fTime % 60).ToString()}";
    }

    public void DisplayPlayButton() { gFreezeButton.SetActive(true); }
    
    public bool AllCheckpointsSelected()
    {
        for (int i = 2; i < SectionIsSelectedList.Count; i++)
        {
            if (!SectionIsSelectedList[i]) return false;
            if (SectionPacenoteList[i] == -1) return false;
            if (SectionHeightList[i] == -1) return false;
            if (SectionAccelerationList[i] == -1) return false;
            if (SectionPositionsList[i] == Vector3.zero) return false;
            if (SectionRotationsList[i] == Vector3.up) return false;
        }

        return true;
    }

    private void _SendInstructions()
    {
        for (int i = 2; i < SectionIsSelectedList.Count; i++)
        {
            _SectionManager.MoveNodes(SectionPacenoteList[i],
                i,
                SectionPositionsList[i],
                SectionRotationsList[i],
                SectionHeightList[i]);
            _SectionManager.SetAccelerations(i, SectionAccelerationList[i], SectionPacenoteList[i]);
            _SectionManager.HideUIs();
        }
    }

    private void _HideFeedback()
    {
        gReconFeedback.SetActive(false);
    }

    private void _PauseGame()
    {
        gCar.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void _ResumeGame()
    {
        if (AllCheckpointsSelected())
        {
            _SendInstructions();

            gCar.GetComponent<Rigidbody>().isKinematic = false;
            _hasStarted = true;
            gFreezeButton.SetActive(false);
            gCameraChange.SetActive(false);
            gIngame.SetActive(true);

            _CameraChange.ChangeCamera();
            _CameraMovComponentFH.MoveCameraToOrigin();
            gNavigationBar.SetActive(false);
            gCheckpointSelectionCanvas.SetActive(false);
            
            _SavePlayerPrefs();
            _ChangeTimeSpeed(2.0f);
        }
        else
        {
            _SoundManager.BadSelectionSE();
            gReconFeedback.SetActive(true);
            _tReconAdvice.text = $"SOME CHECKPOINTS ARE NOT SELECTED\nLOCATE THE RED AURA ABOVE THEM";
            Invoke("_HideFeedback", 1.5f);
        }
    }

    private void _ReloadScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OilUpdate()
    {
        _GasSlider.value = _fGasoline;
    }

    public void DmgUpdate()
    {
        _DmgSlider.value = _fDamage;
    }

    private void _WriteTimeTables(float time, float fuel, float damage)
    {
        /*
         *  WebGL cannot write nor read files
         *
        StreamWriter sw = new StreamWriter(_sFilePath, true);
        sw.WriteLine($"{PlayerPrefs.GetString("User")}|{Math.Round(time, 2).ToString()}|{Math.Round(fuel, 2).ToString()}|{Math.Round(damage, 2).ToString()}");
        sw.Close();
         */
    }

    private void _ReadTimeTables(float time, float fuel, float damage)
    {
        /*
         *  WebGL cannot write nor read files
         *
        StreamReader sr = new StreamReader(_sFilePath);
        string[] line = new string[4];
        string file = "";
        for (int i = 1; i <= 5; i++)
        {
            file = sr.ReadLine();
            if (file == null || file != "")
            {
                line = file.Split('|');
                switch (i)
                {
                    case 1:
                        _tEndRow1Name.text = line[0];
                        _tEndRow1Time.text = line[1];
                        _tEndRow1Fuel.text = line[2];
                        _tEndRow1Damage.text = line[3];
                        break;
                    case 2:
                        _tEndRow2Name.text = line[0];
                        _tEndRow2Time.text = line[1];
                        _tEndRow2Fuel.text = line[2];
                        _tEndRow2Damage.text = line[3];
                        break;
                    case 3:
                        _tEndRow3Name.text = line[0];
                        _tEndRow3Time.text = line[1];
                        _tEndRow3Fuel.text = line[2];
                        _tEndRow3Damage.text = line[3];
                        break;
                    case 4:
                        _tEndRow4Name.text = line[0];
                        _tEndRow4Time.text = line[1];
                        _tEndRow4Fuel.text = line[2];
                        _tEndRow4Damage.text = line[3];
                        break;
                    case 5:
                        _tEndRow5Name.text = line[0];
                        _tEndRow5Time.text = line[1];
                        _tEndRow5Fuel.text = line[2];
                        _tEndRow5Damage.text = line[3];
                        break;
                }
            }
        }

        sr.Close();
        */

        _tEndRow1Name.text = PlayerPrefs.GetString("User");
        _tEndRow1Time.text = Math.Round(time, 2).ToString();
        _tEndRow1Fuel.text = Math.Round(fuel, 2).ToString();
        _tEndRow1Damage.text = Math.Round(damage, 2).ToString();

        _tEndRow2Name.text = $"Alejandro";
        _tEndRow2Time.text = $"69,61";
        _tEndRow2Fuel.text = $"39,85";
        _tEndRow2Damage.text = $"67,68";

        _tEndRow3Name.text = $"Maria";
        _tEndRow3Time.text = $"131,23";
        _tEndRow3Fuel.text = $"17,50";
        _tEndRow3Damage.text = $"0";

        _tEndRow4Name.text = $"Pedro";
        _tEndRow4Time.text = $"123,89";
        _tEndRow4Fuel.text = $"21,05";
        _tEndRow4Damage.text = $"0";

        _tEndRow5Name.text = $"Alejandro";
        _tEndRow5Time.text = $"155,68";
        _tEndRow5Fuel.text = $"0,70";
        _tEndRow5Damage.text = $"0";
    }

    private void _GameOver(bool D, bool F, bool O)
    {
        Time.timeScale = 1.0f;
        gIngame.SetActive(false);
        gFreezeButton.SetActive(false);
        gGameOver.SetActive(true);
        if (D) tDefeat.text = "You lost because your car has suffered many breakdowns";
        else if (F) tDefeat.text = "You lost because you ran out of fuel";
        else if (O) tDefeat.text = "You have lost because you have gone completely off track";
    }

    private void _Settings()
    {
        gSettingsCanvas.SetActive(true);
    }

    private void _Exit()
    {
        gSettingsCanvas.SetActive(false);
    }

    private void _Return()
    {
        Time.timeScale = 1.0f;
        _DeletePlayerPrefsLong();
        SceneManager.LoadScene("Prefs");
    }

    public void ChangeGear()
    {
        _tIngameGear.text = $"GEAR: {_Controller.GetGear.ToString()}";
    }

    public void ChangeKPH()
    {
        _tIngameKPH.text = $"KPH: {((int)_Controller.GetKPH).ToString()}";
    }

    private void _ChangeTimeSpeed(float f)
    {
        _fTimeSpeed = f;
        Time.timeScale = f;
    }

    private void _LoadPlayerPrefs()
    {
        _cLanguage = Convert.ToChar(PlayerPrefs.GetString("Language"));
        if (_cLanguage == 'e')
            _dSettingsLanguage.value = 0;
        else
            _dSettingsLanguage.value = 1;
    }

    private void _LoadNextStage()
    {
        Time.timeScale = 1.0f;
        _DeletePlayerPrefsLong();
        if (SceneManager.GetActiveScene().name == "Day1M") SceneManager.LoadScene("Day1N");
        if (SceneManager.GetActiveScene().name == "Day1N") SceneManager.LoadScene("Day2A");
        if (SceneManager.GetActiveScene().name == "Day2A") SceneManager.LoadScene("Prefs");
    }

    public void EndScreen(float time, float fuel, float damage)
    {
        _hasEnded = true;
        _CameraController.hasFinished = true;
        gCarView.GetComponent<AudioListener>().enabled = true;
        gIngame.SetActive(false);
        gFreezeButton.SetActive(false);
        gEndCanvas.SetActive(true);
        _WriteTimeTables(time, fuel, damage);
        _ReadTimeTables(time, fuel, damage);
        _SoundManager.EndSE();
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

        _tReconAdvice = gReconAdvice.GetComponent<TMP_Text>();

        _bReconLoadY = gReconLoadY.GetComponent<Button>();
        _bReconLoadY.onClick.AddListener(() => _LoadPlayerPrefsLong());

        _bReconLoadN = gReconLoadN.GetComponent<Button>();
        _bReconLoadN.onClick.AddListener(() => _DeletePlayerPrefsLong());

        _bSettings = gSettings.GetComponent<Button>();
        _bSettings.onClick.AddListener(() => _Settings());

        _bSettingsCredits = gSettingsCredits.GetComponent<Button>();
        _bSettingsCredits.onClick.AddListener(() => gCreditsCanvas.SetActive(true));

        _bCreditsClose = gCreditsClose.GetComponent<Button>();
        _bCreditsClose.onClick.AddListener(() => gCreditsCanvas.SetActive(false));
        
        _bSettingsControls = gSettingsControls.GetComponent<Button>();
        _bSettingsControls.onClick.AddListener(() => gSettingsExitControls.SetActive(true));
        
        _bSettingsExitControls = gSettingsExitControls.GetComponent<Button>();
        _bSettingsExitControls.onClick.AddListener(() => gSettingsExitControls.SetActive(false));

        _bReturn = gReturn.GetComponent<Button>();
        _bReturn.onClick.AddListener(() => _Return());

        _aSource = gAudioManager.GetComponent<AudioSource>();

        _sSettingsMusic = gSettingsMusic.GetComponent<Slider>();
        _sSettingsMusic.onValueChanged.AddListener(delegate
        {
            _aSource.volume = _sSettingsMusic.value;
            _sSettingsEffects.value = _sSettingsMusic.value;
        });

        _sSettingsEffects = gSettingsEffects.GetComponent<Slider>();
        _sSettingsEffects.onValueChanged.AddListener(delegate
        {
            _SoundManager.SetVolume(_sSettingsEffects.value); 
            _sSettingsMusic.value = _sSettingsEffects.value;
            
        });

        _sSettingsSound = gSettingsSound.GetComponent<Slider>();
        _sSettingsSound.onValueChanged.AddListener(delegate { AudioListener.volume = _sSettingsSound.value; });

        _tIngameGear = gIngameGear.GetComponent<TMP_Text>();
        _tIngameKPH = gIngameKPH.GetComponent<TMP_Text>();

        _bIngameResumePlay = gIngameResumePlay.GetComponent<Button>();
        _bIngameResumePlay.onClick.AddListener(() => _ChangeTimeSpeed(0.0f));

        _bIngameNormal = gIngameNormal.GetComponent<Button>();
        _bIngameNormal.onClick.AddListener(() => _ChangeTimeSpeed(2.0f));

        _bIngameFast = gIngameFast.GetComponent<Button>();
        _bIngameFast.onClick.AddListener(() => _ChangeTimeSpeed(4.0f));

        _dSettingsLanguage = gSettingsLanguage.GetComponent<TMP_Dropdown>();
        _dSettingsLanguage.onValueChanged.AddListener(delegate
        {
            if (_dSettingsLanguage.value == 0) _cLanguage = 'e';
            else _cLanguage = 's';
        });

        _bEndExit = gEndExit.GetComponent<Button>();
        _bEndExit.onClick.AddListener(() => _Return());

        _bEndContinue = gEndContinue.GetComponent<Button>();
        _bEndContinue.onClick.AddListener(() => _LoadNextStage());

        _tEndRow1Name = gEndRow1Name.GetComponent<TMP_Text>();
        _tEndRow1Time = gEndRow1Time.GetComponent<TMP_Text>();
        _tEndRow1Fuel = gEndRow1Fuel.GetComponent<TMP_Text>();
        _tEndRow1Damage = gEndRow1Damage.GetComponent<TMP_Text>();
        _tEndRow2Name = gEndRow2Name.GetComponent<TMP_Text>();
        _tEndRow2Time = gEndRow2Time.GetComponent<TMP_Text>();
        _tEndRow2Fuel = gEndRow2Fuel.GetComponent<TMP_Text>();
        _tEndRow2Damage = gEndRow2Damage.GetComponent<TMP_Text>();
        _tEndRow3Name = gEndRow3Name.GetComponent<TMP_Text>();
        _tEndRow3Time = gEndRow3Time.GetComponent<TMP_Text>();
        _tEndRow3Fuel = gEndRow3Fuel.GetComponent<TMP_Text>();
        _tEndRow3Damage = gEndRow3Damage.GetComponent<TMP_Text>();
        _tEndRow4Name = gEndRow4Name.GetComponent<TMP_Text>();
        _tEndRow4Time = gEndRow4Time.GetComponent<TMP_Text>();
        _tEndRow4Fuel = gEndRow4Fuel.GetComponent<TMP_Text>();
        _tEndRow4Damage = gEndRow4Damage.GetComponent<TMP_Text>();
        _tEndRow5Name = gEndRow5Name.GetComponent<TMP_Text>();
        _tEndRow5Time = gEndRow5Time.GetComponent<TMP_Text>();
        _tEndRow5Fuel = gEndRow5Fuel.GetComponent<TMP_Text>();
        _tEndRow5Damage = gEndRow5Damage.GetComponent<TMP_Text>();

        _CameraController = gCarView.GetComponent<CameraController>();
        _CameraChange = gCameraChange.GetComponent<CameraChange>();
        _CameraMovComponentFH = gFreeView.GetComponent<CameraMovComponentFH>();
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
            _sSettingsEffects.value = 0.2f;
            AudioListener.volume = 1.0f;
        }
    }

    private void _InitLists(int size)
    {
        switch (size)
        {
            case 16:
                SectionIsSelectedList = new List<bool>
                {
                    false, false, false, false, false, false, false, false,
                    false, false, false, false, false, false, false, false
                };
                SectionPacenoteList = new List<int>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionHeightList = new List<float>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionAccelerationList = new List<float>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionPositionsList = new List<Vector3>
                {
                    Vector3.up, Vector3.up,
                    Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero,
                    Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero,
                };
                SectionRotationsList = new List<Vector3>
                {
                    Vector3.zero, Vector3.zero,
                    Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                    Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                };
                break;
            case 17:
                SectionIsSelectedList = new List<bool>
                {
                    false, false, false, false, false, false, false, false,
                    false, false, false, false, false, false, false, false, false
                };
                SectionPacenoteList = new List<int>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionHeightList = new List<float>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionAccelerationList = new List<float>
                {
                    0, 0,
                    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1
                };
                SectionPositionsList = new List<Vector3>
                {
                    Vector3.up, Vector3.up,
                    Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero,
                    Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero
                };
                SectionRotationsList = new List<Vector3>
                {
                    Vector3.zero, Vector3.zero,
                    Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up,
                    Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up, Vector3.up
                };
                break;
        }
        
    }

    private void _SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("Pacenote2", SectionPacenoteList[2]);
        PlayerPrefs.SetInt("Pacenote3", SectionPacenoteList[3]);
        PlayerPrefs.SetInt("Pacenote4", SectionPacenoteList[4]);
        PlayerPrefs.SetInt("Pacenote5", SectionPacenoteList[5]);
        PlayerPrefs.SetInt("Pacenote6", SectionPacenoteList[6]);
        PlayerPrefs.SetInt("Pacenote7", SectionPacenoteList[7]);
        PlayerPrefs.SetInt("Pacenote8", SectionPacenoteList[8]);
        PlayerPrefs.SetInt("Pacenote9", SectionPacenoteList[9]);
        PlayerPrefs.SetInt("Pacenote10", SectionPacenoteList[10]);
        PlayerPrefs.SetInt("Pacenote11", SectionPacenoteList[11]);
        PlayerPrefs.SetInt("Pacenote12", SectionPacenoteList[12]);
        PlayerPrefs.SetInt("Pacenote13", SectionPacenoteList[13]);
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetInt("Pacenote14", SectionPacenoteList[14]);
            PlayerPrefs.SetInt("Pacenote15", SectionPacenoteList[15]);
        }
        if (SceneManager.GetActiveScene().name == "Day1N") PlayerPrefs.SetInt("Pacenote16", SectionPacenoteList[16]);

        PlayerPrefs.SetFloat("Height2", SectionHeightList[2]);
        PlayerPrefs.SetFloat("Height3", SectionHeightList[3]);
        PlayerPrefs.SetFloat("Height4", SectionHeightList[4]);
        PlayerPrefs.SetFloat("Height5", SectionHeightList[5]);
        PlayerPrefs.SetFloat("Height6", SectionHeightList[6]);
        PlayerPrefs.SetFloat("Height7", SectionHeightList[7]);
        PlayerPrefs.SetFloat("Height8", SectionHeightList[8]);
        PlayerPrefs.SetFloat("Height9", SectionHeightList[9]);
        PlayerPrefs.SetFloat("Height10", SectionHeightList[10]);
        PlayerPrefs.SetFloat("Height11", SectionHeightList[11]);
        PlayerPrefs.SetFloat("Height12", SectionHeightList[12]);
        PlayerPrefs.SetFloat("Height13", SectionHeightList[13]);
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Height14", SectionHeightList[14]);
            PlayerPrefs.SetFloat("Height15", SectionHeightList[15]);
        }
        if (SceneManager.GetActiveScene().name == "Day1N") PlayerPrefs.SetFloat("Height16", SectionHeightList[16]);
        
        PlayerPrefs.SetFloat("Acceleration2", SectionAccelerationList[2]);
        PlayerPrefs.SetFloat("Acceleration3", SectionAccelerationList[3]);
        PlayerPrefs.SetFloat("Acceleration4", SectionAccelerationList[4]);
        PlayerPrefs.SetFloat("Acceleration5", SectionAccelerationList[5]);
        PlayerPrefs.SetFloat("Acceleration6", SectionAccelerationList[6]);
        PlayerPrefs.SetFloat("Acceleration7", SectionAccelerationList[7]);
        PlayerPrefs.SetFloat("Acceleration8", SectionAccelerationList[8]);
        PlayerPrefs.SetFloat("Acceleration9", SectionAccelerationList[9]);
        PlayerPrefs.SetFloat("Acceleration10", SectionAccelerationList[10]);
        PlayerPrefs.SetFloat("Acceleration11", SectionAccelerationList[11]);
        PlayerPrefs.SetFloat("Acceleration12", SectionAccelerationList[12]);
        PlayerPrefs.SetFloat("Acceleration13", SectionAccelerationList[13]);
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Acceleration14", SectionAccelerationList[14]);
            PlayerPrefs.SetFloat("Acceleration15", SectionAccelerationList[15]);
        }
        if (SceneManager.GetActiveScene().name == "Day1N") PlayerPrefs.SetFloat("Acceleration16", SectionAccelerationList[16]);
        
        PlayerPrefs.SetFloat("Position2X", SectionPositionsList[2].x);
        PlayerPrefs.SetFloat("Position2Y", SectionPositionsList[2].y);
        PlayerPrefs.SetFloat("Position2Z", SectionPositionsList[2].z);
        PlayerPrefs.SetFloat("Position3X", SectionPositionsList[3].x);
        PlayerPrefs.SetFloat("Position3Y", SectionPositionsList[3].y);
        PlayerPrefs.SetFloat("Position3Z", SectionPositionsList[3].z);
        PlayerPrefs.SetFloat("Position4X", SectionPositionsList[4].x);
        PlayerPrefs.SetFloat("Position4Y", SectionPositionsList[4].y);
        PlayerPrefs.SetFloat("Position4Z", SectionPositionsList[4].z);
        PlayerPrefs.SetFloat("Position5X", SectionPositionsList[5].x);
        PlayerPrefs.SetFloat("Position5Y", SectionPositionsList[5].y);
        PlayerPrefs.SetFloat("Position5Z", SectionPositionsList[5].z);
        PlayerPrefs.SetFloat("Position6X", SectionPositionsList[6].x);
        PlayerPrefs.SetFloat("Position6Y", SectionPositionsList[6].y);
        PlayerPrefs.SetFloat("Position6Z", SectionPositionsList[6].z);
        PlayerPrefs.SetFloat("Position7X", SectionPositionsList[7].x);
        PlayerPrefs.SetFloat("Position7Y", SectionPositionsList[7].y);
        PlayerPrefs.SetFloat("Position7Z", SectionPositionsList[7].z);
        PlayerPrefs.SetFloat("Position8X", SectionPositionsList[8].x);
        PlayerPrefs.SetFloat("Position8Y", SectionPositionsList[8].y);
        PlayerPrefs.SetFloat("Position8Z", SectionPositionsList[8].z);
        PlayerPrefs.SetFloat("Position9X", SectionPositionsList[9].x);
        PlayerPrefs.SetFloat("Position9Y", SectionPositionsList[9].y);
        PlayerPrefs.SetFloat("Position9Z", SectionPositionsList[9].z);
        PlayerPrefs.SetFloat("Position10X", SectionPositionsList[10].x);
        PlayerPrefs.SetFloat("Position10Y", SectionPositionsList[10].y);
        PlayerPrefs.SetFloat("Position10Z", SectionPositionsList[10].z);
        PlayerPrefs.SetFloat("Position11X", SectionPositionsList[11].x);
        PlayerPrefs.SetFloat("Position11Y", SectionPositionsList[11].y);
        PlayerPrefs.SetFloat("Position11Z", SectionPositionsList[11].z);
        PlayerPrefs.SetFloat("Position12X", SectionPositionsList[12].x);
        PlayerPrefs.SetFloat("Position12Y", SectionPositionsList[12].y);
        PlayerPrefs.SetFloat("Position12Z", SectionPositionsList[12].z);
        PlayerPrefs.SetFloat("Position13X", SectionPositionsList[13].x);
        PlayerPrefs.SetFloat("Position13Y", SectionPositionsList[13].y);
        PlayerPrefs.SetFloat("Position13Z", SectionPositionsList[13].z);
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Position14X", SectionPositionsList[14].x);
            PlayerPrefs.SetFloat("Position14Y", SectionPositionsList[14].y);
            PlayerPrefs.SetFloat("Position14Z", SectionPositionsList[14].z);
            PlayerPrefs.SetFloat("Position15X", SectionPositionsList[15].x);
            PlayerPrefs.SetFloat("Position15Y", SectionPositionsList[15].y);
            PlayerPrefs.SetFloat("Position15Z", SectionPositionsList[15].z);
        }
        if (SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Position16X", SectionPositionsList[16].x);
            PlayerPrefs.SetFloat("Position16Y", SectionPositionsList[16].y);
            PlayerPrefs.SetFloat("Position16Z", SectionPositionsList[16].z);
        }

        PlayerPrefs.SetFloat("Rotation2X", SectionRotationsList[2].x);
        PlayerPrefs.SetFloat("Rotation2Y", SectionRotationsList[2].y);
        PlayerPrefs.SetFloat("Rotation2Z", SectionRotationsList[2].z);
        PlayerPrefs.SetFloat("Rotation3X", SectionRotationsList[3].x);
        PlayerPrefs.SetFloat("Rotation3Y", SectionRotationsList[3].y);
        PlayerPrefs.SetFloat("Rotation3Z", SectionRotationsList[3].z);
        PlayerPrefs.SetFloat("Rotation4X", SectionRotationsList[4].x);
        PlayerPrefs.SetFloat("Rotation4Y", SectionRotationsList[4].y);
        PlayerPrefs.SetFloat("Rotation4Z", SectionRotationsList[4].z);
        PlayerPrefs.SetFloat("Rotation5X", SectionRotationsList[5].x);
        PlayerPrefs.SetFloat("Rotation5Y", SectionRotationsList[5].y);
        PlayerPrefs.SetFloat("Rotation5Z", SectionRotationsList[5].z);
        PlayerPrefs.SetFloat("Rotation6X", SectionRotationsList[6].x);
        PlayerPrefs.SetFloat("Rotation6Y", SectionRotationsList[6].y);
        PlayerPrefs.SetFloat("Rotation6Z", SectionRotationsList[6].z);
        PlayerPrefs.SetFloat("Rotation7X", SectionRotationsList[7].x);
        PlayerPrefs.SetFloat("Rotation7Y", SectionRotationsList[7].y);
        PlayerPrefs.SetFloat("Rotation7Z", SectionRotationsList[7].z);
        PlayerPrefs.SetFloat("Rotation8X", SectionRotationsList[8].x);
        PlayerPrefs.SetFloat("Rotation8Y", SectionRotationsList[8].y);
        PlayerPrefs.SetFloat("Rotation8Z", SectionRotationsList[8].z);
        PlayerPrefs.SetFloat("Rotation9X", SectionRotationsList[9].x);
        PlayerPrefs.SetFloat("Rotation9Y", SectionRotationsList[9].y);
        PlayerPrefs.SetFloat("Rotation9Z", SectionRotationsList[9].z);
        PlayerPrefs.SetFloat("Rotation10X", SectionRotationsList[10].x);
        PlayerPrefs.SetFloat("Rotation10Y", SectionRotationsList[10].y);
        PlayerPrefs.SetFloat("Rotation10Z", SectionRotationsList[10].z);
        PlayerPrefs.SetFloat("Rotation11X", SectionRotationsList[11].x);
        PlayerPrefs.SetFloat("Rotation11Y", SectionRotationsList[11].y);
        PlayerPrefs.SetFloat("Rotation11Z", SectionRotationsList[11].z);
        PlayerPrefs.SetFloat("Rotation12X", SectionRotationsList[12].x);
        PlayerPrefs.SetFloat("Rotation12Y", SectionRotationsList[12].y);
        PlayerPrefs.SetFloat("Rotation12Z", SectionRotationsList[12].z);
        PlayerPrefs.SetFloat("Rotation13X", SectionRotationsList[13].x);
        PlayerPrefs.SetFloat("Rotation13Y", SectionRotationsList[13].y);
        PlayerPrefs.SetFloat("Rotation13Z", SectionRotationsList[13].z);
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Rotation14X", SectionRotationsList[14].x);
            PlayerPrefs.SetFloat("Rotation14Y", SectionRotationsList[14].y);
            PlayerPrefs.SetFloat("Rotation14Z", SectionRotationsList[14].z);
            PlayerPrefs.SetFloat("Rotation15X", SectionRotationsList[15].x);
            PlayerPrefs.SetFloat("Rotation15Y", SectionRotationsList[15].y);
            PlayerPrefs.SetFloat("Rotation15Z", SectionRotationsList[15].z);
        }
        if (SceneManager.GetActiveScene().name == "Day1N")
        {
            PlayerPrefs.SetFloat("Rotation16X", SectionRotationsList[16].x);
            PlayerPrefs.SetFloat("Rotation16Y", SectionRotationsList[16].y);
            PlayerPrefs.SetFloat("Rotation16Z", SectionRotationsList[16].z);
        }
    }

    private void _LoadPlayerPrefsLong()
    {
        for (int i = 2; i < SectionIsSelectedList.Count; i++)
            SectionIsSelectedList[i] = true;
        
        gReconLoadLong.SetActive(false);

        SectionPacenoteList[2] = PlayerPrefs.GetInt("Pacenote2");
        SectionPacenoteList[3] = PlayerPrefs.GetInt("Pacenote3");
        SectionPacenoteList[4] = PlayerPrefs.GetInt("Pacenote4");
        SectionPacenoteList[5] = PlayerPrefs.GetInt("Pacenote5");
        SectionPacenoteList[6] = PlayerPrefs.GetInt("Pacenote6");
        SectionPacenoteList[7] = PlayerPrefs.GetInt("Pacenote7");
        SectionPacenoteList[8] = PlayerPrefs.GetInt("Pacenote8");
        SectionPacenoteList[9] = PlayerPrefs.GetInt("Pacenote9");
        SectionPacenoteList[10] = PlayerPrefs.GetInt("Pacenote10");
        SectionPacenoteList[11] = PlayerPrefs.GetInt("Pacenote11");
        SectionPacenoteList[12] = PlayerPrefs.GetInt("Pacenote12");
        SectionPacenoteList[13] = PlayerPrefs.GetInt("Pacenote13");
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            SectionPacenoteList[14] = PlayerPrefs.GetInt("Pacenote14");
            SectionPacenoteList[15] = PlayerPrefs.GetInt("Pacenote15");
        }
        if (SceneManager.GetActiveScene().name == "Day1N") SectionPacenoteList[16] = PlayerPrefs.GetInt("Pacenote16");

        SectionHeightList[2] = PlayerPrefs.GetFloat("Height2");
        SectionHeightList[3] = PlayerPrefs.GetFloat("Height3");
        SectionHeightList[4] = PlayerPrefs.GetFloat("Height4");
        SectionHeightList[5] = PlayerPrefs.GetFloat("Height5");
        SectionHeightList[6] = PlayerPrefs.GetFloat("Height6");
        SectionHeightList[7] = PlayerPrefs.GetFloat("Height7");
        SectionHeightList[8] = PlayerPrefs.GetFloat("Height8");
        SectionHeightList[9] = PlayerPrefs.GetFloat("Height9");
        SectionHeightList[10] = PlayerPrefs.GetFloat("Height10");
        SectionHeightList[11] = PlayerPrefs.GetFloat("Height11");
        SectionHeightList[12] = PlayerPrefs.GetFloat("Height12");
        SectionHeightList[13] = PlayerPrefs.GetFloat("Height13");
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            SectionHeightList[14] = PlayerPrefs.GetFloat("Height14");
            SectionHeightList[15] = PlayerPrefs.GetFloat("Height15");
        }
        if (SceneManager.GetActiveScene().name == "Day1N") SectionHeightList[16] = PlayerPrefs.GetFloat("Height16");

        SectionAccelerationList[2] = PlayerPrefs.GetFloat("Acceleration2");
        SectionAccelerationList[3] = PlayerPrefs.GetFloat("Acceleration3");
        SectionAccelerationList[4] = PlayerPrefs.GetFloat("Acceleration4");
        SectionAccelerationList[5] = PlayerPrefs.GetFloat("Acceleration5");
        SectionAccelerationList[6] = PlayerPrefs.GetFloat("Acceleration6");
        SectionAccelerationList[7] = PlayerPrefs.GetFloat("Acceleration7");
        SectionAccelerationList[8] = PlayerPrefs.GetFloat("Acceleration8");
        SectionAccelerationList[9] = PlayerPrefs.GetFloat("Acceleration9");
        SectionAccelerationList[10] = PlayerPrefs.GetFloat("Acceleration10");
        SectionAccelerationList[11] = PlayerPrefs.GetFloat("Acceleration11");
        SectionAccelerationList[12] = PlayerPrefs.GetFloat("Acceleration12");
        SectionAccelerationList[13] = PlayerPrefs.GetFloat("Acceleration13");
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            SectionAccelerationList[14] = PlayerPrefs.GetFloat("Acceleration14");
            SectionAccelerationList[15] = PlayerPrefs.GetFloat("Acceleration15");
        }
        if (SceneManager.GetActiveScene().name == "Day1N") SectionAccelerationList[16] = PlayerPrefs.GetFloat("Acceleration16");

        SectionPositionsList[2] = new Vector3(PlayerPrefs.GetFloat("Position2X"), PlayerPrefs.GetFloat("Position2Y"),
            PlayerPrefs.GetFloat("Position2Z"));
        SectionPositionsList[3] = new Vector3(PlayerPrefs.GetFloat("Position3X"), PlayerPrefs.GetFloat("Position3Y"),
            PlayerPrefs.GetFloat("Position3Z"));
        SectionPositionsList[4] = new Vector3(PlayerPrefs.GetFloat("Position4X"), PlayerPrefs.GetFloat("Position4Y"),
            PlayerPrefs.GetFloat("Position4Z"));
        SectionPositionsList[5] = new Vector3(PlayerPrefs.GetFloat("Position5X"), PlayerPrefs.GetFloat("Position5Y"),
            PlayerPrefs.GetFloat("Position5Z"));
        SectionPositionsList[6] = new Vector3(PlayerPrefs.GetFloat("Position6X"), PlayerPrefs.GetFloat("Position6Y"),
            PlayerPrefs.GetFloat("Position6Z"));
        SectionPositionsList[7] = new Vector3(PlayerPrefs.GetFloat("Position7X"), PlayerPrefs.GetFloat("Position7Y"),
            PlayerPrefs.GetFloat("Position7Z"));
        SectionPositionsList[8] = new Vector3(PlayerPrefs.GetFloat("Position8X"), PlayerPrefs.GetFloat("Position8Y"),
            PlayerPrefs.GetFloat("Position8Z"));
        SectionPositionsList[9] = new Vector3(PlayerPrefs.GetFloat("Position9X"), PlayerPrefs.GetFloat("Position9Y"),
            PlayerPrefs.GetFloat("Position9Z"));
        SectionPositionsList[10] = new Vector3(PlayerPrefs.GetFloat("Position10X"), PlayerPrefs.GetFloat("Position10Y"),
            PlayerPrefs.GetFloat("Position10Z"));
        SectionPositionsList[11] = new Vector3(PlayerPrefs.GetFloat("Position11X"), PlayerPrefs.GetFloat("Position11Y"),
            PlayerPrefs.GetFloat("Position11Z"));
        SectionPositionsList[12] = new Vector3(PlayerPrefs.GetFloat("Position12X"), PlayerPrefs.GetFloat("Position12Y"),
            PlayerPrefs.GetFloat("Position12Z"));
        SectionPositionsList[13] = new Vector3(PlayerPrefs.GetFloat("Position13X"), PlayerPrefs.GetFloat("Position13Y"),
            PlayerPrefs.GetFloat("Position13Z"));
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            SectionPositionsList[14] = new Vector3(PlayerPrefs.GetFloat("Position14X"),
                PlayerPrefs.GetFloat("Position14Y"),
                PlayerPrefs.GetFloat("Position14Z"));
            SectionPositionsList[15] = new Vector3(PlayerPrefs.GetFloat("Position15X"),
                PlayerPrefs.GetFloat("Position15Y"),
                PlayerPrefs.GetFloat("Position15Z"));
        }
        if (SceneManager.GetActiveScene().name == "Day1N") 
            SectionPositionsList[16] = new Vector3(PlayerPrefs.GetFloat("Position16X"), PlayerPrefs.GetFloat("Position16Y"),
            PlayerPrefs.GetFloat("Position16Z"));

        SectionRotationsList[2] = new Vector3(PlayerPrefs.GetFloat("Rotation2X"), PlayerPrefs.GetFloat("Rotation2Y"),
            PlayerPrefs.GetFloat("Rotation2Z"));
        SectionRotationsList[3] = new Vector3(PlayerPrefs.GetFloat("Rotation3X"), PlayerPrefs.GetFloat("Rotation3Y"),
            PlayerPrefs.GetFloat("Rotation3Z"));
        SectionRotationsList[4] = new Vector3(PlayerPrefs.GetFloat("Rotation4X"), PlayerPrefs.GetFloat("Rotation4Y"),
            PlayerPrefs.GetFloat("Rotation4Z"));
        SectionRotationsList[5] = new Vector3(PlayerPrefs.GetFloat("Rotation5X"), PlayerPrefs.GetFloat("Rotation5Y"),
            PlayerPrefs.GetFloat("Rotation5Z"));
        SectionRotationsList[6] = new Vector3(PlayerPrefs.GetFloat("Rotation6X"), PlayerPrefs.GetFloat("Rotation6Y"),
            PlayerPrefs.GetFloat("Rotation6Z"));
        SectionRotationsList[7] = new Vector3(PlayerPrefs.GetFloat("Rotation7X"), PlayerPrefs.GetFloat("Rotation7Y"),
            PlayerPrefs.GetFloat("Rotation7Z"));
        SectionRotationsList[8] = new Vector3(PlayerPrefs.GetFloat("Rotation8X"), PlayerPrefs.GetFloat("Rotation8Y"),
            PlayerPrefs.GetFloat("Rotation8Z"));
        SectionRotationsList[9] = new Vector3(PlayerPrefs.GetFloat("Rotation9X"), PlayerPrefs.GetFloat("Rotation9Y"),
            PlayerPrefs.GetFloat("Rotation9Z"));
        SectionRotationsList[10] = new Vector3(PlayerPrefs.GetFloat("Rotation10X"), PlayerPrefs.GetFloat("Rotation10Y"),
            PlayerPrefs.GetFloat("Rotation10Z"));
        SectionRotationsList[11] = new Vector3(PlayerPrefs.GetFloat("Rotation11X"), PlayerPrefs.GetFloat("Rotation11Y"),
            PlayerPrefs.GetFloat("Rotation11Z"));
        SectionRotationsList[12] = new Vector3(PlayerPrefs.GetFloat("Rotation12X"), PlayerPrefs.GetFloat("Rotation12Y"),
            PlayerPrefs.GetFloat("Rotation12Z"));
        SectionRotationsList[13] = new Vector3(PlayerPrefs.GetFloat("Rotation13X"), PlayerPrefs.GetFloat("Rotation13Y"),
            PlayerPrefs.GetFloat("Rotation13Z"));
        if (SceneManager.GetActiveScene().name == "Day1M" || SceneManager.GetActiveScene().name == "Day1N")
        {
            SectionRotationsList[14] = new Vector3(PlayerPrefs.GetFloat("Rotation14X"),
                PlayerPrefs.GetFloat("Rotation14Y"),
                PlayerPrefs.GetFloat("Rotation14Z"));
            SectionRotationsList[15] = new Vector3(PlayerPrefs.GetFloat("Rotation15X"),
                PlayerPrefs.GetFloat("Rotation15Y"),
                PlayerPrefs.GetFloat("Rotation15Z"));
        }
        if (SceneManager.GetActiveScene().name == "Day1N") 
            SectionRotationsList[16] = new Vector3(PlayerPrefs.GetFloat("Rotation16X"), PlayerPrefs.GetFloat("Rotation16Y"),
            PlayerPrefs.GetFloat("Rotation16Z"));
        
        for (int id = 2; id < SectionIsSelectedList.Count; id++)
            _SectionManager.MoveNodes(SectionPacenoteList[id], id, SectionPositionsList[id], SectionRotationsList[id], SectionHeightList[id]);
        
        _SectionManager.DisplayAllAsSelected();
        
        gFreezeButton.SetActive(true);
    }

    private void _DeletePlayerPrefsLong()
    {
        gReconLoadLong.SetActive(false);

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

    public void EndTutorial() { gReconCanvas.SetActive(true); }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "Day1M") NUM_CHECKPOINTS = 16;
        if (SceneManager.GetActiveScene().name == "Day1N") NUM_CHECKPOINTS = 17;
        if (SceneManager.GetActiveScene().name == "Day2A") NUM_CHECKPOINTS = 14;
        
        Application.targetFrameRate = 60;
        _sFilePath = $"{Application.dataPath}\\Src\\Framework\\FileTimeTables.txt";
        _InitLists(NUM_CHECKPOINTS);
        _InitButtons();
        _LoadPlayerPrefs();

        _PauseGame();
        _SectionManager = GetComponent<SectionManager>();
        _SoundManager = gAudioManager.GetComponent<SoundManager>();
        ChangeGear();
        ChangeKPH();
        if (PlayerPrefs.HasKey("Position9Z") && PlayerPrefs.GetFloat("Position9Z") != 0.0f)
            gReconLoadLong.SetActive(true);
    }

    private void Start()
    {
        _InitAudio();
        StartCoroutine(GetOil());
        try
        {
            if (!IsMobile()) return;
            gAndroidJoystick.SetActive(true);
            gAndroidDPad.SetActive(true);
            gAndroidTutorialA.SetActive(false);
            gAndroidTutorialB.SetActive(true);
        }
        catch {}
    }

    private void FixedUpdate()
    {
        _fDamage = _Controller.GetDmg;
        DmgUpdate();
        bool isDamaged = _fDamage >= 100.0f;
        bool noFuel = _fGasoline < 0.0f;
        // Add || _isOut when roads are finished
        if ((isDamaged || noFuel || _isOut) && !_hasEnded)
        {
            _GameOver(isDamaged, noFuel, _isOut);
            _Controller.StopCar();
        }
    }

    private void Update()
    {
        if (_hasStarted)
        {
            _fTime += (Time.deltaTime);
            _SetText();
        }
    }

    private IEnumerator GetOil()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _fGasoline = _Controller.GetGas;
            _isOut = _Controller.IsOutOfTrack;
            OilUpdate();
        }
    }
}