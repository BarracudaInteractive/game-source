using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Serialization;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraMovComponentFH : MonoBehaviour
{
    [Header("Tutorial")]
    public GameObject gFirstCameraPosition;
    public GameObject gSecondCameraPosition;
    public GameObject gThirdCameraPosition;
    public GameObject gInitCameraPosition;
    public GameObject gTutorialCanvas;
    public GameObject gTutorialExit;
    public GameObject gTutorial0;
    public GameObject gTutorial1;
    public GameObject gTutorial2;
    public GameObject gTutorial3;
    public GameObject gTutorial0Continue;
    public GameObject gTutorial1Continue;
    public GameObject gTutorial2Continue;
    public GameObject gTutorial3Continue;
    public GameObject gTutorial1Back;
    public GameObject gTutorial2Back;
    public GameObject gTutorial3Back;
    public GameObject gGameManager;
    
    private Button _bTutorialExit;
    private Button _bTutorial0Continue;
    private Button _bTutorial1Continue;
    private Button _bTutorial2Continue;
    private Button _bTutorial3Continue;
    private Button _bTutorial1Back;
    private Button _bTutorial2Back;
    private Button _bTutorial3Back;
    
    private float _fLerpTime = 2.0f;
    private float LookOffset = 1;
    private float CameraAngle = 30;
    private float RotationSpeed = 12;
    private Camera _CurrentCamera;
    private Vector3 _CameraPositionTarget = Vector3.zero;
    
    private float _fHeight = 20.0f;
    private Vector3 _vec3MovDirection;
    private Vector3 _vec3MovTarget;
    private Vector3 _v3RotInit;
    
    private const float INTERNAL_MOVE_TARGET_SPEED = 64;
    private const float INTERNAL_MOVE_SPEED = 32;
    
    //Rotation variables
    private bool _rightMouseDown = false;
    private const float InternalRotationSpeed = 16;
    private Quaternion _rotationTarget;
    private Vector2 _mouseDelta;

    private bool _playingTutorial = true;
    private bool _pos1 = true;
    private bool _pos2 = false;
    private bool _pos3 = false;
    private bool _pos4 = false;
    
    public void OnRotateToggle(InputAction.CallbackContext context) { _rightMouseDown = context.ReadValueAsButton(); }
    
    public void OnRotate(InputAction.CallbackContext context) { _mouseDelta = _rightMouseDown ? context.ReadValue<Vector2>() : Vector2.zero; }
    
    public void OnMove(InputAction.CallbackContext context) { Vector2 value = context.ReadValue<Vector2>(); _vec3MovDirection = new Vector3(value.x, 0, value.y); }
    
    private void _CameraTransition()
    {
        if (_pos1)
            _CurrentCamera.transform.position = Vector3.Lerp(_CurrentCamera.transform.position,
                gFirstCameraPosition.transform.position, _fLerpTime/4 * Time.deltaTime);
        
        if (_pos2)
            _CurrentCamera.transform.position = Vector3.Lerp(_CurrentCamera.transform.position,
                gSecondCameraPosition.transform.position, _fLerpTime * Time.deltaTime);
        
        if (_pos3)
            _CurrentCamera.transform.position = Vector3.Lerp(_CurrentCamera.transform.position, 
                gThirdCameraPosition.transform.position, _fLerpTime * Time.deltaTime);
        
        if (_pos4)
            _CurrentCamera.transform.position = Vector3.Lerp(_CurrentCamera.transform.position, 
                gInitCameraPosition.transform.position, _fLerpTime * Time.deltaTime);
    }

    private void _ContinueTutorial1()
    {
        gTutorial0.SetActive(false); 
        gTutorial1.SetActive(true);
        _pos1 = false;
        _pos2 = true;
        _pos3 = false;
        _pos4 = false;
    }

    private void _ContinueTutorial2()
    {
        gTutorial1.SetActive(false); 
        gTutorial2.SetActive(true);
        _pos1 = false;
        _pos2 = false;
        _pos3 = true;
        _pos4 = false;
    }

    private void _ContinueTutorial3()
    {
        gTutorial2.SetActive(false); 
        gTutorial3.SetActive(true);
        _pos1 = false;
        _pos2 = false;
        _pos3 = false;
        _pos4 = true;
    }

    private void _BackTutorial1()
    {
        gTutorial1.SetActive(false);
        gTutorial0.SetActive(true);
        _pos1 = true;
        _pos2 = false;
        _pos3 = false;
        _pos4 = false;
    }
    
    private void _BackTutorial2()
    {
        gTutorial2.SetActive(false);
        gTutorial1.SetActive(true);
        _pos1 = false;
        _pos2 = true;
        _pos3 = false;
        _pos4 = false;
    }
    
    private void _BackTutorial3()
    {
        gTutorial3.SetActive(false);
        gTutorial2.SetActive(true);
        _pos1 = false;
        _pos2 = false;
        _pos3 = true;
        _pos4 = false;
    }
    
    private void _StartTutorial()
    {
        gTutorialCanvas.SetActive(true);
    }

    private void _ExitTutorial()
    {
        gGameManager.GetComponent<GameManager>().EndTutorial();
        _CurrentCamera.GetComponent<LookAtConstraint>().enabled = false;
        _CurrentCamera.transform.position = _CameraPositionTarget;
        _CurrentCamera.transform.rotation = Quaternion.AngleAxis(CameraAngle, Vector3.right);
        _playingTutorial = false;
        gTutorialCanvas.SetActive(false);
    }
    
    private void _InitButtons()
    {
        _bTutorialExit = gTutorialExit.GetComponent<Button>();
        _bTutorial0Continue = gTutorial0Continue.GetComponent<Button>();
        _bTutorial1Continue = gTutorial1Continue.GetComponent<Button>();
        _bTutorial2Continue = gTutorial2Continue.GetComponent<Button>();
        _bTutorial3Continue = gTutorial3Continue.GetComponent<Button>();
        _bTutorial1Back = gTutorial1Back.GetComponent<Button>();
        _bTutorial2Back = gTutorial2Back.GetComponent<Button>();
        _bTutorial3Back = gTutorial3Back.GetComponent<Button>();
        
        _bTutorialExit.onClick.AddListener(() => _ExitTutorial());
        _bTutorial0Continue.onClick.AddListener(() => _ContinueTutorial1());
        _bTutorial1Continue.onClick.AddListener(() => _ContinueTutorial2());
        _bTutorial2Continue.onClick.AddListener(() => _ContinueTutorial3());
        _bTutorial3Continue.onClick.AddListener(() => _ExitTutorial());
        _bTutorial1Back.onClick.AddListener(() => _BackTutorial1());
        _bTutorial2Back.onClick.AddListener(() => _BackTutorial2());
        _bTutorial3Back.onClick.AddListener(() => _BackTutorial3());
    }
    
    public void Awake()
    {
        _InitButtons();
        
        _CurrentCamera = GetComponentInChildren<Camera>();
        _CurrentCamera.transform.rotation = Quaternion.AngleAxis(CameraAngle, Vector3.right);

        transform.position += new Vector3(0, _fHeight, 0);
        _CameraPositionTarget = (Vector3.up * LookOffset + new Vector3(0,_fHeight,0)) + (Quaternion.AngleAxis(CameraAngle, 
            Vector3.right) * Vector3.back);
        _CurrentCamera.transform.position = _CameraPositionTarget;

        _rotationTarget = transform.rotation;
        
        if (_playingTutorial)
        {
            Invoke("_StartTutorial", 2.0f);
        }
    }

    private void FixedUpdate()
    {
        _vec3MovTarget += (transform.forward * _vec3MovDirection.z + transform.right * 
            _vec3MovDirection.x) * (Time.fixedDeltaTime * INTERNAL_MOVE_TARGET_SPEED);
    }

    private void LateUpdate()
    {
        if (!_playingTutorial)
        {
            transform.position = Vector3.Lerp(transform.position, _vec3MovTarget, Time.deltaTime * INTERNAL_MOVE_SPEED);
            transform.position = new Vector3(transform.position.x, _fHeight, transform.position.z);
            _rotationTarget *= Quaternion.AngleAxis(_mouseDelta.x * Time.deltaTime * RotationSpeed, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, Time.deltaTime * InternalRotationSpeed);
        }
        else
        {
            _CameraTransition();
        }
    }
}
