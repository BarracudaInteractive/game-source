using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour 
{
    [Header("Camera settings")]
    //[Range (0, 50)] public float fSmothTime = 4;
    
    private GameObject _gCar;
    private Controller _Controller;
    private GameObject _gCameraLookAt,_gCameraPos;
    private bool _hasFinished = false;

    //Rotation variables
    private bool _rightMouseDown = false;
    private float _RotationAccumulation = 0;
    private Vector2 _vec2MouseDelta;
    private const float INTERNAL_ROTATION_SPEED = 4;
    private const float DISTANCE_TO_CAR = 10;
    
    public bool hasFinished { get => _hasFinished; set => _hasFinished = value; }
    
    public void OnRotate(InputAction.CallbackContext context) { _vec2MouseDelta = _rightMouseDown ? context.ReadValue<Vector2>() : Vector2.zero; }
    
    public void OnRotateToggle(InputAction.CallbackContext context) { _rightMouseDown = context.ReadValueAsButton(); }
    
    public void OnRotateDPad(InputAction.CallbackContext context) { _vec2MouseDelta = context.ReadValue<Vector2>() * INTERNAL_ROTATION_SPEED; }
    
    private void Awake () 
    {
        _gCar = GameObject.FindGameObjectWithTag("AI");
        _Controller = _gCar.GetComponent<Controller> ();
        _gCameraLookAt = _gCar.transform.Find("camera lookAt").gameObject;
        _gCameraPos = _gCar.transform.Find("camera constraint").gameObject;
        _gCameraPos.transform.LookAt(_gCameraLookAt.transform.position);
    }
    
    private void FixedUpdate()
    {
        float speed = _Controller.GetKPH / 8;
        speed = 5;
        gameObject.transform.position = Vector3.Lerp (transform.position, _gCameraPos.transform.position,  Time.deltaTime * speed);
        gameObject.transform.LookAt(_gCameraLookAt.transform.position);
    }

    private void LateUpdate()
    {
        if (!_hasFinished)
        {
            float moveRight = _vec2MouseDelta.x > 0 ? 1 : 0;
            float moveLeft = _vec2MouseDelta.x < 0 ? -1 : 0;
            _RotationAccumulation += moveRight + moveLeft;

            _gCameraPos.transform.position = _gCameraLookAt.transform.position + Quaternion.Euler(0f, _RotationAccumulation * INTERNAL_ROTATION_SPEED, 0f) * new Vector3(0f, 3f, -DISTANCE_TO_CAR);
        }
        else if (_hasFinished)
        {
            _gCameraPos.transform.position = _gCameraLookAt.transform.position + Quaternion.Euler(0f, Time.time * INTERNAL_ROTATION_SPEED, 0f) * new Vector3(0f, 0f, -5f);
        }
    }
    
}