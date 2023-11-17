using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class CameraMovComponentFH : MonoBehaviour
{
    public GameObject gFirstCameraPosition;
    public GameObject gSecondCameraPosition;
    public GameObject gThirdCameraPosition;
    
    private float _fLerpTime = 2.0f;
    private float LookOffset = 1;
    private float CameraAngle = 30;
    private float RotationSpeed = 6;
    private Camera _actualCamera;
    private Vector3 _cameraPositionTarget = Vector3.zero;
    
    private float _fHeight = 20.0f;
    private Vector3 _vec3MovDirection;
    private Vector3 _vec3MovTarget;
    private Vector3 _v3RotInit;
    
    private const float INTERNAL_MOVE_TARGET_SPEED = 32;
    private const float INTERNAL_MOVE_SPEED = 16;
    
    //Rotation variables
    private bool _rightMouseDown = false;
    private const float InternalRotationSpeed = 8;
    private Quaternion _rotationTarget;
    private Vector2 _mouseDelta;

    private bool _playingTutorial = true;
    private bool _pos1 = true;
    private bool _pos2 = false;
    private bool _pos3 = false;
    
    public void OnRotateToggle(InputAction.CallbackContext context)
    {
        _rightMouseDown = context.ReadValueAsButton();
    }
    
    public void OnRotate(InputAction.CallbackContext context)
    {
        // If the right mouse is down then we'll read the mouse delta value. If it is not, we'll clear it out.
        // Note: Clearing the mouse delta prevents a 'death spin' 
        //from occurring if the player flings the mouse really fast in a direction.
        _mouseDelta = _rightMouseDown ? context.ReadValue<Vector2>() : Vector2.zero;
        
    }
    
    /// <summary>
    /// Sets the direction of movement based on the input provided by the player
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        _vec3MovDirection = new Vector3(value.x, 0, value.y);
    }
    
    private void _CameraTransition()
    {
        if (_pos1)
            _actualCamera.transform.position = Vector3.Lerp(_actualCamera.transform.position,
                gFirstCameraPosition.transform.position, _fLerpTime/4 * Time.deltaTime);
        
        if (_pos2)
            _actualCamera.transform.position = Vector3.Lerp(_actualCamera.transform.position,
                gSecondCameraPosition.transform.position, _fLerpTime * Time.deltaTime);
        
        if (_pos3)
            _actualCamera.transform.position = Vector3.Lerp(_actualCamera.transform.position, 
                gThirdCameraPosition.transform.position, _fLerpTime * Time.deltaTime);
    }
    
    public void Awake()
    {
        _actualCamera = GetComponentInChildren<Camera>();
        _actualCamera.transform.rotation = Quaternion.AngleAxis(CameraAngle, Vector3.right);

        transform.position += new Vector3(0, _fHeight, 0);
        _cameraPositionTarget = (Vector3.up * LookOffset + new Vector3(0,_fHeight,0)) + (Quaternion.AngleAxis(CameraAngle, 
            Vector3.right) * Vector3.back);
        _actualCamera.transform.position = _cameraPositionTarget;

        _rotationTarget = transform.rotation;
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

            transform.rotation =
                Quaternion.Slerp(transform.rotation, _rotationTarget, Time.deltaTime * InternalRotationSpeed);
        }
        else
        {
            _CameraTransition();
        }
    }
}
