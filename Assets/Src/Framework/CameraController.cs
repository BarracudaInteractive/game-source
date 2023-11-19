using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraController : MonoBehaviour 
{
    [Header("Camera settings")]
    [Range (0, 50)] public float fSmothTime = 4;
    
    private GameObject _gCar;
    private Controller _Controller;
    private GameObject _gCameraLookAt,_gCameraPos;
    private float _fSpeed = 0;

    private void _Follow () 
    {
        _fSpeed = _Controller.GetKPH / fSmothTime;
        gameObject.transform.position = Vector3.Lerp (transform.position, _gCameraPos.transform.position ,  Time.deltaTime * _fSpeed);
        gameObject.transform.LookAt(_gCameraLookAt.gameObject.transform.position);
    }
    
    private void Awake () 
    {
        _gCar = GameObject.FindGameObjectWithTag("AI");
        _Controller = _gCar.GetComponent<Controller> ();
        _gCameraLookAt = _gCar.transform.Find("camera lookAt").gameObject;
        _gCameraPos = _gCar.transform.Find("camera constraint").gameObject;
    }

    private void FixedUpdate () { _Follow(); }
}