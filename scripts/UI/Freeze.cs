using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Freeze : MonoBehaviour
{
    private Button freezeGame;
    private int rtime = 60;
    [Header("Text field")] //straight, shallow curve, tight curve, very tight curve, hairpin
    public TMP_Text txt;
    public float time = 0.0f;
    private bool startTimer = false;
    public void textSet() { txt.text = Mathf.FloorToInt(time / 60).ToString() + ":" + Mathf.FloorToInt(time % 60).ToString(); }
    void PauseGame() { Time.timeScale = 0; }

    void ResumeGame() { Time.timeScale = 1; startTimer = true; }
    

    private void Awake()
    {
        PauseGame();
        Application.targetFrameRate = rtime;
        freezeGame = this.GetComponent<Button>();        
        freezeGame.onClick.AddListener(() => ResumeGame());
    }

    private void Update()
    {
        if (startTimer) { time += Time.deltaTime; textSet(); }
    }
}