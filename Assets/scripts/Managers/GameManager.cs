using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [HideInInspector]public Controller RR;
    public Text gearNum;
    
    [Header("Gasoline bar")] // 0.2 - 1.0
    public GameObject gb;
    private Slider s0;
    
    [Header("Damage bar")] // 0.2 - 1.0
    public GameObject db;
    private Slider s1;
    
    private float gbValue = 0.0f;
    private float dbValue = 0.0f;
    
    public void changeGear() { gearNum.text = (!RR.reverse) ? (RR.gearNum + 1).ToString() : "R"; }

    public void loadAwakeScene(){ SceneManager.LoadScene("awakeScene"); }
    
    private void loadTesting(){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    
    public void oilUpdate() { s0.value = gbValue; }
    
    public void dmgUpdate() { s1.value = dbValue; }
    
    private void Awake() 
    {
        RR = GameObject.FindGameObjectWithTag("AI").GetComponent<Controller>();
        s0 = gb.GetComponent<Slider>();
        s1 = db.GetComponent<Slider>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "awakeScene") return; 
        StartCoroutine(oilGet());
    }

    private void FixedUpdate()
    {
        dbValue = RR.dmg;
        dmgUpdate();
        if (dbValue >= 100.0f || gbValue >= 100.0f) loadTesting();
    }

    private IEnumerator oilGet(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            gbValue = RR.gas;
            oilUpdate();
        }
    }
}