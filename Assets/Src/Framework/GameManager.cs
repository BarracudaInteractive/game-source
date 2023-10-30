using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [Header("Gasoline bar")] // 0.2 - 1.0
    public GameObject _gGasolineBar;
    
    [Header("Damage bar")] // 0.2 - 1.0
    public GameObject _gDamageBar;
    
    private Controller _Controller;
    
    private float _fGasoline = 0.0f;
    private float _fDamage = 0.0f;
    
    private Slider _GasSlider;
    private Slider _DmgSlider;
    
    private void _LoadTesting() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    
    public void OilUpdate() { _GasSlider.value = _fGasoline; }
    
    public void DmgUpdate() { _DmgSlider.value = _fDamage; }
    
    private void Awake() 
    {
        Application.targetFrameRate = 60;
        _Controller = GameObject.FindGameObjectWithTag("AI").GetComponent<Controller>();
        _GasSlider = _gGasolineBar.GetComponent<Slider>();
        _DmgSlider = _gDamageBar.GetComponent<Slider>();
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "awakeScene") return; 
        StartCoroutine(GetOil());
    }

    private void FixedUpdate()
    {
        _fDamage = _Controller.GetDmg;
        DmgUpdate();
        if (_fDamage >= 100.0f || _fGasoline >= 100.0f) _LoadTesting();
    }

    private IEnumerator GetOil(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            _fGasoline = _Controller.GetGas;
            OilUpdate();
        }
    }
}