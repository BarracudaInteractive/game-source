using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private float _fLookOffset = 1;
    private float _fCameraAngle = 30;
    private float _fRotationSpeed = 6;
    private Camera _CurrentCamera;
    private Vector3 _vec3CameraPositionTarget = Vector3.zero;
    
    //Zoom variables
    private float _fDefaultZoom = 40;
    private float _fZoomMax = 20;
    private float _fZoomMin = 160;
    private float _fCurrentZoomAmount;
    private const float INTERNAL_ZOOM_SPEED = 256;
    
    //Move variables
    private float _fHeight = 20.0f;
    private Vector3 _vec3MovDirection;
    private Vector3 _vec3MovTarget;
    private Vector3 _vec3RotInit;
    private const float INTERNAL_MOVE_TARGET_SPEED = 64;
    private const float INTERNAL_MOVE_SPEED = 32;
    
    //Rotation variables
    private bool _rightMouseDown = false;
    private Quaternion _RotationTargetQuaternion;
    private Vector2 _vec2MouseDelta;
    private const float INTERNAL_ROTATION_SPEED = 8;

    private GameObject gLookAtTarget;
    private bool _selectingCheckpoint = false;
    private bool _playingTutorial = true;
    private bool _pos1 = true;
    private bool _pos2 = false;
    private bool _pos3 = false;
    private bool _pos4 = false;
    
    private LookAtConstraint _lookAtConstraint;
    
    public float CurrentZoom
    {
        get => _fCurrentZoomAmount;
        private set { _fCurrentZoomAmount = value; _UpdateCameraTarget(); }
    }

    public void MoveCameraToOrigin()
    { 
        transform.position = gInitCameraPosition.transform.position;
    }
    
    private void _UpdateCameraTarget() 
    { 
        _vec3CameraPositionTarget = (Vector3.up * _fLookOffset) + (Quaternion.AngleAxis(_fCameraAngle, Vector3.right) * Vector3.back) * _fCurrentZoomAmount;
    }
    
    public void OnZoom(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed)
            return;
        
        // Adjust the current zoom value based on the direction of the scroll - this is clamped to our zoom min/max. 
        CurrentZoom = Mathf.Clamp(_fCurrentZoomAmount - context.ReadValue<Vector2>().y, _fZoomMax, _fZoomMin);
    }
    
    public void OnRotateToggle(InputAction.CallbackContext context) { _rightMouseDown = context.ReadValueAsButton(); }
    
    public void OnRotate(InputAction.CallbackContext context) { _vec2MouseDelta = _rightMouseDown ? context.ReadValue<Vector2>() : Vector2.zero; }
    
    public void OnMove(InputAction.CallbackContext context) { Vector2 value = context.ReadValue<Vector2>(); _vec3MovDirection = new Vector3(value.x, 0, value.y); }
    
    public void CameraBetweenCheckpoints(GameObject o, bool b)
    {
        gLookAtTarget = o;
        _lookAtConstraint.enabled = b;
        _lookAtConstraint.SetSource(0, new ConstraintSource {sourceTransform = o.transform, weight = 1});
        _selectingCheckpoint = b;
    }
    
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
        _CurrentCamera.transform.position = _vec3CameraPositionTarget;
        _CurrentCamera.transform.rotation = Quaternion.AngleAxis(_fCameraAngle, Vector3.right);
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
        _CurrentCamera.transform.rotation = Quaternion.AngleAxis(_fCameraAngle, Vector3.right);

        transform.position += new Vector3(0, _fHeight, 0);
        _vec3CameraPositionTarget = (Vector3.up * _fLookOffset + new Vector3(0,_fHeight,0)) + (Quaternion.AngleAxis(_fCameraAngle, Vector3.right) * Vector3.back) * _fDefaultZoom;
        CurrentZoom = _fDefaultZoom;
        _CurrentCamera.transform.position = _vec3CameraPositionTarget;
        _RotationTargetQuaternion = transform.rotation;
        _lookAtConstraint = GetComponentInChildren<LookAtConstraint>();
        
        if (_playingTutorial)
        {
            Invoke("_StartTutorial", 2.0f);
        }
    }

    private void FixedUpdate()
    {
        _vec3MovTarget += (transform.forward * _vec3MovDirection.z + transform.right * _vec3MovDirection.x) * (Time.fixedDeltaTime * INTERNAL_MOVE_TARGET_SPEED);
    }

    private void LateUpdate()
    {
        if (!_playingTutorial)
        {
            if (_selectingCheckpoint)
            {
                _vec3MovTarget = gLookAtTarget.transform.position;
                transform.position = Vector3.Lerp(transform.position, _vec3MovTarget, _fLerpTime * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _vec3MovTarget, Time.deltaTime * INTERNAL_MOVE_SPEED);
            }
            _CurrentCamera.transform.localPosition = Vector3.Lerp(_CurrentCamera.transform.localPosition, _vec3CameraPositionTarget, Time.deltaTime * INTERNAL_ZOOM_SPEED);
            _RotationTargetQuaternion *= Quaternion.AngleAxis(_vec2MouseDelta.x * Time.deltaTime * _fRotationSpeed, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, _RotationTargetQuaternion, Time.deltaTime * INTERNAL_ROTATION_SPEED);
        }
        else
        {
            _CameraTransition();
        }
    }
}
