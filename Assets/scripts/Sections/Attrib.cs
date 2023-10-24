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
    public short id = 0;
    public bool reverse = false;
    public float sf = 0.0f;
    public TMP_Text txt;
    public float time = 0.0f;
    
    public void TextSet() { txt.text = "Section " + id + ": " + Math.Round(time, 2).ToString(); }
    
    private void Update()
    {
        time += Time.deltaTime; 
    }
    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "AI" && sf != 0.0f)
        {
            TextSet();
            coll.gameObject.GetComponent<InputManager>().steerForce = sf;
            coll.gameObject.GetComponent<InputManager>().acceleration = sf;
        }
    }
}