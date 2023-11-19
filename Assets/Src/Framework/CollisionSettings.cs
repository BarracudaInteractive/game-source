using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CollisionSettings : MonoBehaviour
{
    [Header("Settings")] 
    public short iId = 0;
    public float fSteerForce = 0.0f;
    public int iDistanceOffset = 0;
    public GameObject gGameManager;
    public GameObject gFeedbackText;
    public GameObject gFeedbackGoodImg;
    public GameObject gFeedbackBadImg;
    public GameObject gFeedbackSound;
    public float _fGoodAcceleration = 0.0f;
    public int _fGoodPacenote = 0;
    
    float _fAcceleration = 0.0f;
    private int _iPacenote = 0;
    private TMP_Text _tFeedbackText;
    private bool _isWorse = false;
    private SoundManager _SoundManager;
    private GameManager _GameManager;
    
    private const int LAST_CHECKPOINT = 16;
    
    public float SetAcc { set => _fAcceleration = value; }
    
    public int SetPacenote { set => _iPacenote = value; }

    private void _HideFeedback()
    {
        if (_isWorse)
            gFeedbackBadImg.SetActive(false);
        else
            gFeedbackGoodImg.SetActive(false);
        gFeedbackText.SetActive(false); 
    }
    
    private void _ShowFeedback(string s)
    {
        gFeedbackText.SetActive(true);
        if (_isWorse)
        {
            gFeedbackBadImg.SetActive(true);
            _SoundManager.BadSelectionSE();
        }
        else
        {
            gFeedbackGoodImg.SetActive(true);
            _SoundManager.GoodSelectionSE();
        }
        _tFeedbackText.text = s;
        if (Time.timeScale == 0.0f)
            Invoke("_HideFeedback", 1.5f);
        else
            Invoke("_HideFeedback", 1.5f/Time.timeScale);
    }
    
    public int GetId => iId;

    private void Awake()
    {
        if (iId != LAST_CHECKPOINT)
        {
            _tFeedbackText = gFeedbackText.GetComponent<TMP_Text>();
            _SoundManager = gFeedbackSound.GetComponent<SoundManager>();
        }
        else
        {
            _GameManager = gGameManager.GetComponent<GameManager>();
        }
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("AI"))
        {
            if (iId != LAST_CHECKPOINT)
            {
                _isWorse = false;
                if (_fAcceleration <= _fGoodAcceleration && _iPacenote == _fGoodPacenote)
                    _ShowFeedback($"CHECKPOINT {iId.ToString()}\nGREAT JOB!");
                if (_fAcceleration <= _fGoodAcceleration && _iPacenote != _fGoodPacenote)
                    _ShowFeedback($"CHECKPOINT {iId.ToString()}\nGOOD ATTITUDE, BUT POOR PACENOTE");
                else if (_iPacenote == _fGoodPacenote && _fAcceleration > _fGoodAcceleration)
                    _ShowFeedback($"CHECKPOINT {iId.ToString()}\nEXCELLENT PACENOTE, BUT POOR ATTITUDE");
                else if (_fAcceleration > _fGoodAcceleration && _iPacenote != _fGoodPacenote)
                {
                    _isWorse = true;
                    _ShowFeedback($"CHECKPOINT {iId.ToString()}\nROOM FOR IMPROVEMENT IN PACENOTE AND ATTITUDE");
                }
            }
            
            coll.gameObject.GetComponent<InputManager>().SetAcceleration = iId == LAST_CHECKPOINT ? 0.0f : _fAcceleration;
            coll.gameObject.GetComponent<InputManager>().SetSteerForce = fSteerForce;
            coll.gameObject.GetComponent<InputManager>().SetDistanceOffset = iDistanceOffset;

            if (iId == LAST_CHECKPOINT)
                _GameManager.EndScreen(_GameManager.GetTime, _GameManager.GetGasoline, _GameManager.GetDamage);
        }
    }
}
