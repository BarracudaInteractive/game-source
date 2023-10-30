using System;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Attrib : MonoBehaviour
{
    [Header("Section attributes")] public short iId = 0;
    public bool isReverse = false;

    [Header("Text to write in")] public TMP_Text tText;

    private float _fSteerForce = 0.0f;
    private float _fChrono = 0.0f;

    private void _SetText()
    {
        tText.text = $"Section {iId.ToString()}: {Math.Round(_fChrono, 2).ToString()}";
    }

    public short GetId => iId;

    public bool IsReverse => isReverse;

    public float SetSf { set => _fSteerForce = value; }

private void Update() { _fChrono += Time.deltaTime; }
    
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "AI" && _fSteerForce != 0.0f)
        {
            _SetText();
            coll.gameObject.GetComponent<InputManager>().SetAcceleration = _fSteerForce;
            coll.gameObject.GetComponent<InputManager>().SetSteerForce = _fSteerForce;
        }
    }
}