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
    
    private short _iHigh = 12;
    private Transform _tCurrentWaypoint;
    private List<Transform> _NodesList = new List<Transform> ();
    private List<float> _StepsTuple = new List<float> { 8.62f, 10.0f, 27.5f, 52.0f, 23.33f, 50.0f, 5.0f, 50.0f, 65.0f};
    
    private int _iTimeout = 0;
    private int _iRenderTimer = 30;
    private GameObject _gHit;
    
    private Button _Button0;
    private Button _Button1;
    private Button _Button2;
    private Button _Button3;
    private Button _Button4;
    private Button _Button6;
    private Button _Button7;
    private Button _Button8;
    private Button _Button9;
    private short _iPacenoteSelected = 0;
    
    private Slider _TemperSlider;
    private float _fTemperValue = 0.2f;
    
    private Button _Button5;
    
    private short _iSectionId = 0;
    private int _iIdx = 0;
    private float _fSectionHigh = 0;
    private Vector3 _v3Pos;
    private Vector3 _v3Rot;
    
    private void _SetPacenoteValue(short val) { _iPacenoteSelected = val; }
    
    private void _SetTemperValue(float val) { _fTemperValue = val; }
    
    private void _MoveNodes()
    {
        Vector3 mov;
        switch (_iPacenoteSelected)
        {
            case 0:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                    _NodesList[_iIdx + i].position = mov;
                    _NodesList[_iIdx + i].Rotate(0.0f, _v3Rot.y, _v3Pos.z + 0.0f, Space.Self);
                    _NodesList[_iIdx + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 1:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                    _NodesList[_iIdx + i].position = mov;
                    //Rotate 8.62f degrees around the Y axis left side
                    _NodesList[_iIdx + i].Rotate(0.0f, -_StepsTuple[0] + _v3Rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    _NodesList[_iIdx + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 2:
                for (var i = 0; i < 5; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                    _NodesList[_iIdx + i].position = mov;
                    //Rotate 8.62f degrees around the Y axis left side
                    _NodesList[_iIdx + i].Rotate(0.0f, _StepsTuple[0] + _v3Rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    _NodesList[_iIdx + i].Translate(0.0f, 0.0f, (20.0f * i), Space.Self);
                }
                break;
            case 3:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(_StepsTuple[1], 0.0f, 30.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, 64.11f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(-_StepsTuple[2], 0.0f, 103.88f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(-_StepsTuple[3], 0.0f, 109.17f, Space.Self);
                
                break;
            case 4:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(-_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(-_StepsTuple[1], 0.0f, 30.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, 64.11f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(_StepsTuple[2], 0.0f, 103.88f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(_StepsTuple[3], 0.0f, 109.17f, Space.Self);
                break;
            case 5:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(_StepsTuple[1], 0.0f, 38.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, 75.5f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(-_StepsTuple[4], 0.0f, 100.0f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(-_StepsTuple[5], 0.0f, 80.0f, Space.Self);
                break;
            case 6:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(-_StepsTuple[1], 0.0f, 0.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(-_StepsTuple[1], 0.0f, 38.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, 75.5f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(_StepsTuple[4], 0.0f, 100.0f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(_StepsTuple[5], 0.0f, 80.0f, Space.Self);
                break;
            case 7:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[1], 0.0f, 15.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(_StepsTuple[6], 0.0f, 48.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(-_StepsTuple[1], 0.0f, 63.0f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(-57.5f+7.5f, 0.0f, 55.0f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(-70+7.5f, 0.0f, 33.0f, Space.Self);
                break;
            case 8:
                mov = new Vector3(_v3Pos.x, _iHigh + _fSectionHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(-_StepsTuple[1], 0.0f, 15.0f, Space.Self);
                
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(-_StepsTuple[6], 0.0f, 48.0f, Space.Self);
                
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(_StepsTuple[1], 0.0f, 63.0f, Space.Self);
                
                _NodesList[_iIdx + 3].position = mov;
                _NodesList[_iIdx + 3].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 3].Translate(57.5f-7.5f, 0.0f, 55.0f, Space.Self);
                
                _NodesList[_iIdx + 4].position = mov;
                _NodesList[_iIdx + 4].Rotate(0.0f, 0.0f + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 4].Translate(70-7.5f, 0.0f, 33.0f, Space.Self);
                break;
        }
    }
    
    private void _Submit() { _StartSectionSet(false); _MoveNodes(); _gHit.GetComponent<Attrib>().SetAcc = _fTemperValue; }
    
    private void _InitButtons()
    {
        _Button0 = gStraight.GetComponent<Button>();
        _Button0.onClick.AddListener(() => _SetPacenoteValue(0));
        _Button1 = gShallowCurve.GetComponent<Button>();
        _Button1.onClick.AddListener(() => _SetPacenoteValue(1));
        _Button2 = gTightCurve.GetComponent<Button>();
        _Button2.onClick.AddListener(() => _SetPacenoteValue(3));
        _Button3 = gVeryTightCurve.GetComponent<Button>();
        _Button3.onClick.AddListener(() => _SetPacenoteValue(5));
        _Button4 = gHairpin.GetComponent<Button>();
        _Button4.onClick.AddListener(() => _SetPacenoteValue(7));
        _Button6 = gShallowCurveReverse.GetComponent<Button>();
        _Button6.onClick.AddListener(() => _SetPacenoteValue(2));
        _Button7 = gTightCurveReverse.GetComponent<Button>();
        _Button7.onClick.AddListener(() => _SetPacenoteValue(4));
        _Button8 = gVeryTightCurveReverse.GetComponent<Button>();
        _Button8.onClick.AddListener(() => _SetPacenoteValue(6));
        _Button9 = gHairpinReverse.GetComponent<Button>();
        _Button9.onClick.AddListener(() => _SetPacenoteValue(8));
    }

    private void _InitSlider()
    {
        _TemperSlider = gTemperSlider.GetComponent<Slider>();
        _TemperSlider.onValueChanged.AddListener(delegate { _SetTemperValue(_TemperSlider.value); });
    }
    
    private void _StartSectionSet(bool b){ SectionSetCanvas.SetActive(b);  }
    
    private void _SetText() { tText.text = $"Section: {_iSectionId.ToString()}"; }
    
    private void Awake()
    {
        _InitButtons();
        _InitSlider();
        _Button5 = gSubmitButton.GetComponent<Button>();
        _Button5.onClick.AddListener(() => _Submit());
    }

    private void Start()
    {
        _NodesList = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>().NodesList;
    }

    private void Update()
    {
        _iTimeout += 1;
        _iTimeout %= _iRenderTimer;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 10000);
            if (hit.transform)
            {
                if (hit.collider.CompareTag("section") && _iTimeout > 0)
                {
                    _iTimeout = -_iRenderTimer;
                    _iSectionId = hit.collider.GetComponent<Attrib>().GetId;
                    _iIdx = _iSectionId * 5;
                    _fSectionHigh = hit.collider.GetComponent<Attrib>().GetHigh;
                    _StartSectionSet(true);
                    _SetText();
                    _v3Pos = hit.collider.transform.position;
                    _v3Rot = hit.collider.transform.rotation.eulerAngles;
                    _gHit = hit.collider.gameObject;
                }
            }
        }
    }
}
