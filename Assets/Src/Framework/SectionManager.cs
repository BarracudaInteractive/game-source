using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class SectionManager : MonoBehaviour
{
    [Header("Section Canvas")]
    public GameObject SectionSetCanvas;
    
    [Header("Camara")]
    public GameObject gCamera;
    
    [Header("Checkpoints")]
    public List<GameObject> CheckpointArray;
    public GameObject[] CollidersArray;
    
    [Header("Pacenotes buttons")]
    public GameObject gStraight; 
    public GameObject gShallowCurve;
    public GameObject gTightCurve;
    public GameObject gVeryTightCurve;
    public GameObject gHairpin;
    public GameObject gShallowCurveReverse;
    public GameObject gTightCurveReverse;
    public GameObject gVeryTightCurveReverse;
    public GameObject gHairpinReverse;
    
    [Header("Temper slider")]
    public GameObject gTemperSlider;
    
    [Header("Submit button")] 
    public GameObject gSubmitButton;

    [Header("Text field")]
    public TMP_Text tText;
    
    private GameManager _GameManager;
    private CameraMovComponentFH _CameraMovComponentFH;
    
    private Transform _tCurrentWaypoint;
    private List<Transform> _NodesList = new List<Transform> ();
    private const short BASE_HEIGHT = 12;
    private List<float> _StepsTuple = new List<float> { 8.62f, 10.0f, 27.5f, 52.0f, 23.33f, 50.0f, 5.0f, 50.0f, 65.0f};
    
    private int _iTimeout = 0;
    private int _iRenderTimer = 30;
    
    private Button _Button0;
    private Button _Button1;
    private Button _Button2;
    private Button _Button3;
    private Button _Button4;
    private Button _Button6;
    private Button _Button7;
    private Button _Button8;
    private Button _Button9;
    private int _iCurrentPacenote = 0;
    
    private Slider _TemperSlider;
    private float _fTemperValue = 0.2f;
    
    private Button _Button5;

    private int _iCurrentCheckpoint = 0;
    private int _iCurrentPosition = 0;
    private int _iPositions = 0;
    private int _iPositionsContra = 0;
    private float _fCurrentHeight = 0;
    private GameObject[] _PositionsArray;
    private List<Vector3> CurrentPos;
    private List<Vector3> CurrentRot;
    
    public void SetPacenote(short val) { _iCurrentPacenote = val; }
    
    public void SetTemper(float val) { _fTemperValue = val; }
    
    public void HideUIs()
    {
        foreach (GameObject checkpoint in CheckpointArray)
            checkpoint.GetComponent<CheckpointHandler>().HideUIs();
    }
    
    public void MoveNodes(int _iPacenoteSelected, int id, Vector3 _v3Pos, Vector3 _v3Rot, float _iHigh)
    {
        Vector3 mov;
        id *= 5;

        //Might be needed in the future
        //for (var i = 0; i < 5; i++)
        //    _NodesList[id + i].rotation = Quaternion.identity;
        
        switch (_iPacenoteSelected)
        {
            case 0:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                    _NodesList[id + i].position = mov;
                    _NodesList[id + i].Rotate(0.0f, _v3Rot.y, _v3Pos.z + 0.0f, Space.Self);
                    _NodesList[id + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 1:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                    _NodesList[id + i].position = mov;
                    //Rotate 8.62f degrees around the Y axis left side
                    _NodesList[id + i].Rotate(0.0f, -_StepsTuple[0] + _v3Rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    _NodesList[id + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 2:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                    _NodesList[id + i].position = mov;
                    //Rotate 8.62f degrees around the Y axis left side
                    _NodesList[id + i].Rotate(0.0f, _StepsTuple[0] + _v3Rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    _NodesList[id + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 3:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(_StepsTuple[1], 0.0f, 30.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(0.0f, 0.0f, 64.11f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(-_StepsTuple[2], 0.0f, 103.88f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(-_StepsTuple[3], 0.0f, 109.17f, Space.Self);
                
                break;
            case 4:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(-_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(-_StepsTuple[1], 0.0f, 30.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(0.0f, 0.0f, 64.11f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(_StepsTuple[2], 0.0f, 103.88f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(_StepsTuple[3], 0.0f, 109.17f, Space.Self);
                break;
            case 5:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(_StepsTuple[1], 0.0f, 38.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(0.0f, 0.0f, 75.5f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(-_StepsTuple[4], 0.0f, 100.0f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(-_StepsTuple[5], 0.0f, 80.0f, Space.Self);
                break;
            case 6:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(-_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(-_StepsTuple[1], 0.0f, 38.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(0.0f, 0.0f, 75.5f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(_StepsTuple[4], 0.0f, 100.0f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(_StepsTuple[5], 0.0f, 80.0f, Space.Self);
                break;
            case 7:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(_StepsTuple[1], 0.0f, 15.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(_StepsTuple[6], 0.0f, 48.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(-_StepsTuple[1], 0.0f, 63.0f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(-57.5f+7.5f, 0.0f, 55.0f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(-70+7.5f, 0.0f, 33.0f, Space.Self);
                break;
            case 8:
                mov = new Vector3(_v3Pos.x, _iHigh + _fCurrentHeight, _v3Pos.z);
                _NodesList[id].position = mov;
                _NodesList[id].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id].Translate(-_StepsTuple[1], 0.0f, 15.0f, Space.Self);
                
                _NodesList[id + 1].position = mov;
                _NodesList[id + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 1].Translate(-_StepsTuple[6], 0.0f, 48.0f, Space.Self);
                
                _NodesList[id + 2].position = mov;
                _NodesList[id + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 2].Translate(_StepsTuple[1], 0.0f, 63.0f, Space.Self);
                
                _NodesList[id + 3].position = mov;
                _NodesList[id + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 3].Translate(57.5f-7.5f, 0.0f, 55.0f, Space.Self);
                
                _NodesList[id + 4].position = mov;
                _NodesList[id + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[id + 4].Translate(70-7.5f, 0.0f, 33.0f, Space.Self);
                break;
        }
    }

    public void SetAccelerations(int id, float acc, int note)
    {
        CollidersArray[id].GetComponent<CollisionSettings>().SetAcc = acc; 
        CollidersArray[id].GetComponent<CollisionSettings>().SetPacenote = note;
    }
    
    public void DisplayAllAsSelected()
    {
        foreach (GameObject checkpoint in CheckpointArray)
        {
            checkpoint.GetComponent<CheckpointHandler>().DisplayAsSelected();
            int i = 0;
            foreach (GameObject o in checkpoint.GetComponent<CheckpointHandler>().CollidersArray)
            {
                checkpoint.GetComponent<CheckpointHandler>().ShowPacenoteSelected(_GameManager.SectionPacenoteList[o.GetComponent<CollisionSettings>().GetId], i);
                i++;
            }
        }
    }
    
    private void _Submit()
    {
        if (_iPositions > 1)
        {
            CheckpointArray[_iCurrentCheckpoint].GetComponent<CheckpointHandler>().ShowPacenoteSelected(_iCurrentPacenote, _iPositionsContra);
            _GameManager.SectionIsSelectedList[_iCurrentPosition] = true;
            _GameManager.SectionPacenoteList[_iCurrentPosition] = _iCurrentPacenote;
            _GameManager.SectionHeightList[_iCurrentPosition] = BASE_HEIGHT + _fCurrentHeight;
            _GameManager.SectionAccelerationList[_iCurrentPosition] = _fTemperValue;
            _GameManager.SectionPositionsList[_iCurrentPosition] = CurrentPos[_iPositionsContra];
            _GameManager.SectionRotationsList[_iCurrentPosition] = CurrentRot[_iPositionsContra];
            _iCurrentPosition++;
            _iPositionsContra++;
            _iPositions--;
            _SetText();
            _CameraMovComponentFH.CameraBetweenCheckpoints(CollidersArray[_iCurrentPosition], true);
        }
        // Last checkpoint
        else 
        {
            CheckpointArray[_iCurrentCheckpoint].GetComponent<CheckpointHandler>().ShowPacenoteSelected(_iCurrentPacenote, _iPositionsContra);
            _GameManager.SectionIsSelectedList[_iCurrentPosition] = true;
            _GameManager.SectionPacenoteList[_iCurrentPosition] = _iCurrentPacenote;
            _GameManager.SectionHeightList[_iCurrentPosition] = BASE_HEIGHT + _fCurrentHeight;
            _GameManager.SectionAccelerationList[_iCurrentPosition] = _fTemperValue;
            _GameManager.SectionPositionsList[_iCurrentPosition] = CurrentPos[_iPositionsContra];
            _GameManager.SectionRotationsList[_iCurrentPosition] = CurrentRot[_iPositionsContra];
            SectionSetCanvas.SetActive(false);
            CheckpointArray[_iCurrentCheckpoint].GetComponent<CheckpointHandler>().DisplayAsSelected();
            _CameraMovComponentFH.CameraBetweenCheckpoints(gCamera, false);
            
            if(_GameManager.AllCheckpointsSelected())
                _GameManager.DisplayPlayButton();
        }
        
    }
    
    private void _InitButtons()
    {
        //Pacenotes
        _Button0 = gStraight.GetComponent<Button>();
        _Button0.onClick.AddListener(() => SetPacenote(0));
        _Button1 = gShallowCurve.GetComponent<Button>();
        _Button1.onClick.AddListener(() => SetPacenote(1));
        _Button2 = gTightCurve.GetComponent<Button>();
        _Button2.onClick.AddListener(() => SetPacenote(3));
        _Button3 = gVeryTightCurve.GetComponent<Button>();
        _Button3.onClick.AddListener(() => SetPacenote(5));
        _Button4 = gHairpin.GetComponent<Button>();
        _Button4.onClick.AddListener(() => SetPacenote(7));
        _Button6 = gShallowCurveReverse.GetComponent<Button>();
        _Button6.onClick.AddListener(() => SetPacenote(2));
        _Button7 = gTightCurveReverse.GetComponent<Button>();
        _Button7.onClick.AddListener(() => SetPacenote(4));
        _Button8 = gVeryTightCurveReverse.GetComponent<Button>();
        _Button8.onClick.AddListener(() => SetPacenote(6));
        _Button9 = gHairpinReverse.GetComponent<Button>();
        _Button9.onClick.AddListener(() => SetPacenote(8));
        
        //Submit
        _Button5 = gSubmitButton.GetComponent<Button>();
        _Button5.onClick.AddListener(() => _Submit());
        
        //Temper slider
        _TemperSlider = gTemperSlider.GetComponent<Slider>();
        _TemperSlider.onValueChanged.AddListener(delegate { SetTemper(_TemperSlider.value); });
    }
    
    private void _SetText() { tText.text = $"CHECKPOINT {(_iPositionsContra + 1).ToString()}"; }
    
    private void _FindIds(Vector3 pos, Vector3 rot)
    {
        _iPositionsContra = 0;
        CurrentPos = new List<Vector3>();
        CurrentRot = new List<Vector3>();
        for (int i = _iCurrentPosition; i < _iCurrentPosition + _iPositions; i++)
        {
            CurrentPos.Add(CollidersArray[i].GetComponent<Transform>().position);
            CurrentRot.Add(CollidersArray[i].GetComponent<Transform>().rotation.eulerAngles);
        }
    }
    
    public void SelectCheckpoint(int id, float height, Vector3 pos, Vector3 rot, GameObject g)
    {
        _iCurrentCheckpoint = id;
        _fCurrentHeight = height;
        _iCurrentPosition = CheckpointArray[id].GetComponent<CheckpointHandler>().CollidersArray[0].GetComponent<CollisionSettings>().GetId;
        _iPositions = CheckpointArray[id].GetComponent<CheckpointHandler>().CollidersArray.Length;
        _CameraMovComponentFH.CameraBetweenCheckpoints(CollidersArray[_iCurrentPosition], true);
        _FindIds(pos, rot);
        _SetText();
    }
    
    private void Awake()
    {
        _InitButtons();
        _GameManager = GetComponent<GameManager>();
        _CameraMovComponentFH = gCamera.GetComponent<CameraMovComponentFH>();
    }

    private void Start()
    {
        _NodesList = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>().NodesList;
    }
}
