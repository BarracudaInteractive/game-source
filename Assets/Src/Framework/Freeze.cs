using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    [Header("Text field")]
    public TMP_Text tText;
    
    private float _fTime = 0.0f;
    private bool _hasStarted = false;
    private Button _FreezeButton;

    private void _SetText()
    {
        string zeroU = "";
        string zeroD = zeroU;
        if (Mathf.FloorToInt(_fTime % 60) < 10) zeroU = "0"; 
        if (Mathf.FloorToInt(_fTime / 60) < 10) zeroD = "0";
        tText.text = $"{zeroD}{Mathf.FloorToInt(_fTime / 60).ToString()}:{zeroU}{Mathf.FloorToInt(_fTime % 60).ToString()}";
    }
    
    private void PauseGame() { Time.timeScale = 0; }

    private void ResumeGame() { Time.timeScale = 1; _hasStarted = true; }
    
    private void Awake()
    {
        PauseGame();
        _FreezeButton = GetComponent<Button>();        
        _FreezeButton.onClick.AddListener(() => ResumeGame());
    }

    private void Update()
    {
        if (_hasStarted) { _fTime += Time.deltaTime; _SetText(); }
    }
}