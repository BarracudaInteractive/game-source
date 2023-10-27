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
    [Header("Section attributes")]
    public short iId = 0;
    public bool isReverse = false;
    
    [Header("Text to write in")]
    public TMP_Text tText;
    
    private float fSteerForce = 0.0f;
    private float fChrono = 0.0f;
    
    public void SetText() { tText.text = $"Section {iId.ToString()}: {Math.Round(fChrono, 2).ToString()}"; }

    public short GetId() { return iId; }
    
    public bool IsReverse() { return isReverse; }
    
    public void SetSf(float pPrefsValue) { fSteerForce = pPrefsValue; }
    
    private void Update() { fChrono += Time.deltaTime; }
    
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "AI" && fSteerForce != 0.0f)
        {
            SetText();
            coll.gameObject.GetComponent<InputManager>().steerForce = fSteerForce;
            coll.gameObject.GetComponent<InputManager>().acceleration = fSteerForce;
        }
    }
}