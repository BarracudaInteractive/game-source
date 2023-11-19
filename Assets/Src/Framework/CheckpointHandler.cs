using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class CheckpointHandler : MonoBehaviour
{
    [Header("Game manager")]
    public GameObject gGameManager;
    
    [Header("Section set canvas")]
    public GameObject gSectionSetCanvas;
    
    [Header("Positions and colliders")]
    public GameObject[] PositionsArray;
    public GameObject[] CollidersArray;
    
    [Header("Checkpoint canvas")]
    public GameObject gCheckpointCanvas;
    public GameObject gArrow;
    public GameObject gSelected;
    public GameObject gNoSelected;
    
    [Header("Checkpoint id")] 
    public short iId = 0;
    
    private SectionManager SectionManager;
    private Button _bArrow;
    private float _fHeight = 0.0f;
    
    public float SetHigh { set => _fHeight = value; }

    private void _SectionSetActive()
    {
        gSectionSetCanvas.SetActive(true);
        SectionManager.SelectCheckpoint(iId, _fHeight, transform.position, transform.rotation.eulerAngles, gameObject); 
    }
    
    public void DisplayAsSelected()
    {
        gArrow.SetActive(true);
        gSelected.SetActive(true);
        gNoSelected.SetActive(false);
    }
    
    private void _InitButtons()
    {
        _bArrow = gArrow.GetComponent<Button>();
        _bArrow.onClick.AddListener(() => _SectionSetActive());
    }
    
    private void Awake()
    {
        SetHigh = transform.position.y;
        _InitButtons();
        SectionManager = gGameManager.GetComponent<SectionManager>();
    }
    
    private void Update()
    {
        _bArrow.transform.Rotate(Vector3.up * (Time.deltaTime * 100));
    }
}
