using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject gCarView;
    public GameObject gFreeView;
    
    private Button _buttonChange;
    private bool _isFreeView = true;
    
    private void _ChangeCamera()
    {
        if (_isFreeView)
        {
            gFreeView.SetActive(false);
            gCarView.SetActive(true);
            _isFreeView = false;
        }
        else if (!_isFreeView)
        {
            gCarView.SetActive(false);
            gFreeView.SetActive(true);
            _isFreeView = true;
        }
    }
    
    private void Awake()
    {
        _buttonChange = this.GetComponent<Button>();
        _buttonChange.onClick.AddListener(() => _ChangeCamera());
    }
}
