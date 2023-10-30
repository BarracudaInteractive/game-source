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
    
    [Header("Temper slider")]
    public GameObject gTemperSlider;
    
    [Header("Submit button")] 
    public GameObject gSubmitButton;

    [Header("Text field")] //straight, shallow curve, tight curve, very tight curve, hairpin
    public TMP_Text tText;
    
    private short _iHigh = 12;
    private Transform _tCurrentWaypoint;
    private List<Transform> _NodesList = new List<Transform> ();
    private List<float> _StepsTuple = new List<float> { 8.62f, 8.53f, -10.0f, 36.87f, 70.28f, 4.90f, 53.67f, 138.80f, -4.76f, 30.0f};
    
    private int _iTimeout = 0;
    private int _iRenderTimer = 30;
    private GameObject _gHit;
    
    private Button _Button0;
    private Button _Button1;
    private Button _Button2;
    private Button _Button3;
    private Button _Button4;
    private short _iPacenoteSelected = 0;
    
    private Slider _TemperSlider;
    private float _fTemperValue = 0.2f;
    
    private Button _Button5;
    
    private short _iSectionId = 0;
    private int _iIdx = 0;
    private bool _isRev = false;
    private Vector3 _v3Pos;
    private Vector3 _v3Rot;
    
    private void _SetPacenoteValue(short val) { _iPacenoteSelected = val; }
    
    private void _SetTemperValue(float val) { _fTemperValue = val; }

    private void _SwapSideStepsValues() { for (int i = 0; i < _StepsTuple.Count; i++) _StepsTuple[i] = -_StepsTuple[i]; }
    
    private void _MoveNodes()
    {
        Vector3 mov;
        if (_isRev) _SwapSideStepsValues();
        switch (_iPacenoteSelected)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh, _v3Pos.z);
                    _NodesList[_iIdx + i].position = mov;
                    _NodesList[_iIdx + i].Rotate(0.0f, _v3Rot.y, _v3Pos.z + 0.0f, Space.Self);
                    _NodesList[_iIdx + i].Translate(0.0f, 0.0f, (33.3f * (i + 1)), Space.Self);
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    mov = new Vector3(_v3Pos.x, _iHigh, _v3Pos.z);
                    _NodesList[_iIdx + i].position = mov;
                    _NodesList[_iIdx + i].Rotate(0.0f, _StepsTuple[0] + _v3Rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    _NodesList[_iIdx + i].Translate(0.0f, 0.0f, (33.3f * (i + 1)), Space.Self);
                }
                break;
            case 2:
                mov = new Vector3(_v3Pos.x, _iHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, _StepsTuple[1] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[2], 0.0f, (60.66f), Space.Self);
                _NodesList[_iIdx + 1].position = _NodesList[_iIdx].position;
                _NodesList[_iIdx + 1].Rotate(0.0f, _StepsTuple[3] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(0.0f, 0.0f, (50.0f), Space.Self);
                _NodesList[_iIdx + 2].position = _NodesList[_iIdx + 1].position;
                _NodesList[_iIdx + 2].Rotate(0.0f, _StepsTuple[4] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, (50.0f), Space.Self);
                break;
            case 3:
                mov = new Vector3(_v3Pos.x, _iHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, _StepsTuple[5] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[2], 0.0f, (70.26f), Space.Self);
                _NodesList[_iIdx + 1].position = _NodesList[_iIdx].position;
                _NodesList[_iIdx + 1].Rotate(0.0f, _StepsTuple[6] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(0.0f, 0.0f, (59.0f), Space.Self);
                _NodesList[_iIdx + 2].position = _NodesList[_iIdx + 1].position;
                _NodesList[_iIdx + 2].Rotate(0.0f, _StepsTuple[7] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(0.0f, 0.0f, (33.09f), Space.Self);
                break;
            case 4:
                mov = new Vector3(_v3Pos.x, _iHigh, _v3Pos.z);
                _NodesList[_iIdx].position = mov;
                _NodesList[_iIdx].Rotate(0.0f, _StepsTuple[8] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx].Translate(_StepsTuple[2], 0.0f, (60.21f), Space.Self);
                _NodesList[_iIdx + 1].position = mov;
                _NodesList[_iIdx + 1].Rotate(0.0f, _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 1].Translate(_StepsTuple[9], 0.0f, (75.50f), Space.Self);
                _NodesList[_iIdx + 2].position = mov;
                _NodesList[_iIdx + 2].Rotate(0.0f, -_StepsTuple[8] + _v3Rot.y, 0.0f, Space.Self);
                _NodesList[_iIdx + 2].Translate(_StepsTuple[9] * 2 - _StepsTuple[2], 0.0f, (60.21f), Space.Self);
                break;
        }
        if (_isRev) _SwapSideStepsValues();
    }
    
    private void _Submit() { _StartSectionSet(false); _MoveNodes(); _gHit.GetComponent<Attrib>().SetSf = _fTemperValue; }
    
    private void _InitButtons()
    {
        _Button0 = gStraight.GetComponent<Button>();
        _Button0.onClick.AddListener(() => _SetPacenoteValue(0));
        _Button1 = gShallowCurve.GetComponent<Button>();
        _Button1.onClick.AddListener(() => _SetPacenoteValue(1));
        _Button2 = gTightCurve.GetComponent<Button>();
        _Button2.onClick.AddListener(() => _SetPacenoteValue(2));
        _Button3 = gVeryTightCurve.GetComponent<Button>();
        _Button3.onClick.AddListener(() => _SetPacenoteValue(3));
        _Button4 = gHairpin.GetComponent<Button>();
        _Button4.onClick.AddListener(() => _SetPacenoteValue(4));
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
                if (hit.collider.tag == "section" && _iTimeout > 0)
                {
                    _iTimeout = -_iRenderTimer;
                    _iSectionId = hit.collider.GetComponent<Attrib>().GetId;
                    _isRev = hit.collider.GetComponent<Attrib>().IsReverse;
                    _iIdx = _iSectionId * 3;
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
