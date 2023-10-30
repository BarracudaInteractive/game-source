using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class CameraMovComponentFH : MonoBehaviour
{
    [Header("Camera")]
    public GameObject gCamera;
    
    private float _fHeight = 20.0f;
    private float _fMovSpeed = 2.0f;
    private float _fRotSpeed = 2.0f;
    private Vector3 _v3RotInit;

    /// <summary>
    /// Method to move the camera.   	
    /// </summary>
    /// <param name=""> </param>
    private void _Move() 
    { 
        //A to move left
        if (Input.GetKey(KeyCode.A)) transform.position+= -transform.right * _fMovSpeed;
        //D to move right
        if (Input.GetKey(KeyCode.D)) transform.position += transform.right * _fMovSpeed;
        //W to move forward
        if (Input.GetKey(KeyCode.W)) transform.position += transform.forward * _fMovSpeed;
        //S to move backwards
        if (Input.GetKey(KeyCode.S)) transform.position += -transform.forward * _fMovSpeed;
    }

    /// <summary>
    /// Method to rotate the camera.   	
    /// </summary>
    /// <param name=""> </param>
    private void _Rotate()
    {
        //Use Q to rotate counte-clock wise
        if (Input.GetKey(KeyCode.Q)) transform.Rotate(new Vector3(0, -_fRotSpeed, 0));
        //Use E to rotate clock wise
        if (Input.GetKey(KeyCode.E)) transform.Rotate(new Vector3(0, _fRotSpeed, 0));
    }
    
    private void Awake()
    {
        _fMovSpeed *= 1.0f; 
        _fRotSpeed *= 1.0f;
        transform.position = new Vector3 (transform.position.x, _fHeight, transform.position.z);
        transform.rotation = Quaternion.identity;
        gCamera.transform.rotation = Quaternion.identity;
        gCamera.transform.Rotate(new Vector3(_v3RotInit.x, 0, _v3RotInit.z));
        transform.Rotate(new Vector3(0, _v3RotInit.y, 0));
    }
    
    private void Update() { _Move(); _Rotate(); }
}
