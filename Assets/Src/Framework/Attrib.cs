using System;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Attrib : MonoBehaviour
{
    [Header("Game manager")]
    public GameObject gGameManager;
    
    [Header("Section attributes")] 
    public short iId = 0;
    
    [Header("Text to write in")] 
    public TMP_Text tText;
    
    [Header("Assets")]
    public float fSteerForce = 0.0f;
    public int iDistanceOffset = 0;
    
    private float _fAcceleration = 0.0f;
    private float _fChrono = 0.0f;
    private float _fHigh = 0.0f;

    private void _SetText() { tText.text = $"Section {iId.ToString()}: {Math.Round(_fChrono, 2).ToString()}"; }

    public short GetId => iId;
    
    public float GetHigh => _fHigh;

    public float SetAcc { set => _fAcceleration = value; }
    
    public float SetHigh { set => _fHigh = value; }

    private void Awake() { SetHigh = transform.position.y; }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("AI"))
        {
            _fChrono = gGameManager.GetComponent<GameManager>().GetTime;
            _SetText();

            coll.gameObject.GetComponent<InputManager>().SetAcceleration = _fAcceleration;
            coll.gameObject.GetComponent<InputManager>().SetSteerForce = fSteerForce;
            coll.gameObject.GetComponent<InputManager>().SetDistanceOffset = iDistanceOffset;
        }
    }
}