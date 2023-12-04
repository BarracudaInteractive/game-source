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
    public GameObject gDot;
    
    [Header("Minor Checkpoint pacenotes")]
    public GameObject gMCheckpointPacenotes00;
    public GameObject gMCheckpointPacenotes01;
    public GameObject gMCheckpointPacenotes02;
    public GameObject gMCheckpointPacenotes03;
    public GameObject gMCheckpointPacenotes04;
    public GameObject gMCheckpointPacenotes05;
    public GameObject gMCheckpointPacenotes06;
    public GameObject gMCheckpointPacenotes07;
    public GameObject gMCheckpointPacenotes08;
    public GameObject gMCheckpointPacenotes10;
    public GameObject gMCheckpointPacenotes11;
    public GameObject gMCheckpointPacenotes12;
    public GameObject gMCheckpointPacenotes13;
    public GameObject gMCheckpointPacenotes14;
    public GameObject gMCheckpointPacenotes15;
    public GameObject gMCheckpointPacenotes16;
    public GameObject gMCheckpointPacenotes17;
    public GameObject gMCheckpointPacenotes18;
    public GameObject gMCheckpointPacenotes20;
    public GameObject gMCheckpointPacenotes21;
    public GameObject gMCheckpointPacenotes22;
    public GameObject gMCheckpointPacenotes23;
    public GameObject gMCheckpointPacenotes24;
    public GameObject gMCheckpointPacenotes25;
    public GameObject gMCheckpointPacenotes26;
    public GameObject gMCheckpointPacenotes27;
    public GameObject gMCheckpointPacenotes28;
    public GameObject gMCheckpointPacenotes30;
    public GameObject gMCheckpointPacenotes31;
    public GameObject gMCheckpointPacenotes32;
    public GameObject gMCheckpointPacenotes33;
    public GameObject gMCheckpointPacenotes34;
    public GameObject gMCheckpointPacenotes35;
    public GameObject gMCheckpointPacenotes36;
    public GameObject gMCheckpointPacenotes37;
    public GameObject gMCheckpointPacenotes38;
    
    [Header("Checkpoint id")] 
    public short iId = 0;
    
    private SectionManager SectionManager;
    private Button _bArrow;
    private Button _bDot;
    private float _fHeight = 0.0f;
    
    public float SetHigh { set => _fHeight = value; }
    
    public float GetId() { return iId; }

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
    
    public void HideUIs()
    {
        gCheckpointCanvas.SetActive(false);
        foreach (GameObject g in PositionsArray)
            g.transform.GetChild(2).gameObject.SetActive(false);
    }
    
    public void ShowPacenoteSelected(int p, int id)
    {
        switch (p)
        {
            case 0:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(true);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(true);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(true);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(true);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 1:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(true);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(true);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(true);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(true);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 2:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(true);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(true);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(true);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(true);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 3:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(true);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(true);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(true);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(true);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 4:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(true);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(true);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(true);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(true);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 5:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(true);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(true);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(true);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(true);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 6:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(true);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(true);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(true);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(true);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 7:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(true);
                    gMCheckpointPacenotes08.SetActive(false);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(true);
                    gMCheckpointPacenotes18.SetActive(false);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(true);
                    gMCheckpointPacenotes28.SetActive(false);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(true);
                    gMCheckpointPacenotes38.SetActive(false);
                }
                break;
            case 8:
                if (id == 0)
                {
                    gMCheckpointPacenotes00.SetActive(false);
                    gMCheckpointPacenotes01.SetActive(false);
                    gMCheckpointPacenotes02.SetActive(false);
                    gMCheckpointPacenotes03.SetActive(false);
                    gMCheckpointPacenotes04.SetActive(false);
                    gMCheckpointPacenotes05.SetActive(false);
                    gMCheckpointPacenotes06.SetActive(false);
                    gMCheckpointPacenotes07.SetActive(false);
                    gMCheckpointPacenotes08.SetActive(true);
                }
                else if (id == 1)
                {
                    gMCheckpointPacenotes10.SetActive(false);
                    gMCheckpointPacenotes11.SetActive(false);
                    gMCheckpointPacenotes12.SetActive(false);
                    gMCheckpointPacenotes13.SetActive(false);
                    gMCheckpointPacenotes14.SetActive(false);
                    gMCheckpointPacenotes15.SetActive(false);
                    gMCheckpointPacenotes16.SetActive(false);
                    gMCheckpointPacenotes17.SetActive(false);
                    gMCheckpointPacenotes18.SetActive(true);
                }
                else if (id == 2)
                {
                    gMCheckpointPacenotes20.SetActive(false);
                    gMCheckpointPacenotes21.SetActive(false);
                    gMCheckpointPacenotes22.SetActive(false);
                    gMCheckpointPacenotes23.SetActive(false);
                    gMCheckpointPacenotes24.SetActive(false);
                    gMCheckpointPacenotes25.SetActive(false);
                    gMCheckpointPacenotes26.SetActive(false);
                    gMCheckpointPacenotes27.SetActive(false);
                    gMCheckpointPacenotes28.SetActive(true);
                }
                else if (id == 3)
                {
                    gMCheckpointPacenotes30.SetActive(false);
                    gMCheckpointPacenotes31.SetActive(false);
                    gMCheckpointPacenotes32.SetActive(false);
                    gMCheckpointPacenotes33.SetActive(false);
                    gMCheckpointPacenotes34.SetActive(false);
                    gMCheckpointPacenotes35.SetActive(false);
                    gMCheckpointPacenotes36.SetActive(false);
                    gMCheckpointPacenotes37.SetActive(false);
                    gMCheckpointPacenotes38.SetActive(true);
                }
                break;
        }
    }
    
    private void _InitButtons()
    {
        _bArrow = gArrow.GetComponent<Button>();
        _bArrow.onClick.AddListener(() => _SectionSetActive());
        
        _bDot = gDot.GetComponent<Button>();
        _bDot.onClick.AddListener(() => _SectionSetActive());
    }

    private void _InitRotations()
    {
        gArrow.transform.localRotation	= Quaternion.Euler (0, 180, 0);
        if (PositionsArray.Length > 0)
        {
            gMCheckpointPacenotes00.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes01.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes02.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes03.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes04.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes05.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes06.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes07.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes08.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (PositionsArray.Length > 1)
        {
            gMCheckpointPacenotes10.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes11.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes12.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes13.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes14.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes15.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes16.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes17.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes18.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (PositionsArray.Length > 2)
        {
            gMCheckpointPacenotes20.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes21.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes22.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes23.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes24.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes25.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes26.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes27.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes28.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        if (PositionsArray.Length > 3)
        {
            gMCheckpointPacenotes30.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes31.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes32.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes33.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes34.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes35.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes36.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes37.transform.localRotation = Quaternion.Euler(0, 180, 0);
            gMCheckpointPacenotes38.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    
    private void Awake()
    {
        SetHigh = transform.position.y;
        _InitButtons();
        SectionManager = gGameManager.GetComponent<SectionManager>();
    }

    private void Start()
    {
        _InitRotations();
    }

    /*
     * Before the checkpoint arrow selector rotated
     * private void Update() { _bArrow.transform.Rotate(Vector3.up * (Time.deltaTime * 100)); }
     */
}
