using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour 
{
    [Header("Camera settings")]
    [Range (0, 50)] public float fSmothTime = 8;
    
    private GameObject _gCar;
    private Controller _Controller;
    private GameObject _gCameraLookAt,_gCameraPos;
    private float _fSpeed = 0;
    private float _fDefaltFOV = 0;
    private float _fDesiredFOV = 0;

    private void _Follow () 
    {
        _fSpeed = _Controller.GetKPH / fSmothTime;
        gameObject.transform.position = Vector3.Lerp (transform.position, _gCameraPos.transform.position ,  Time.deltaTime * _fSpeed);
        gameObject.transform.LookAt (_gCameraLookAt.gameObject.transform.position);
    }
    
    private void _BoostFOV () { Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, _fDefaltFOV, Time.deltaTime * 5); }
    
    private void Awake () 
    {
        _gCar = GameObject.FindGameObjectWithTag("AI");
        _Controller = _gCar.GetComponent<Controller> ();
        _gCameraLookAt = _gCar.transform.Find("camera lookAt").gameObject;
        _gCameraPos = _gCar.transform.Find("camera constraint").gameObject;
        _fDefaltFOV = Camera.main.fieldOfView;
        _fDesiredFOV = _fDefaltFOV + 15;
    }

    private void FixedUpdate () { _Follow(); _BoostFOV(); }
}