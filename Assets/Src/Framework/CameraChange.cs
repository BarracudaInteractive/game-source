using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{
    public GameObject car_view;
    public GameObject free_view;
    
    private Button change;
    private bool main = true;
    
    void ChangeCamera()
    {
        if (main)
        {
            free_view.SetActive(false);
            car_view.SetActive(true);
            main = false;
        }
        else if (!main)
        {
            car_view.SetActive(false);
            free_view.SetActive(true);
            main = true;
        }
    }
    
    private void Start()
    {
        change = this.GetComponent<Button>();
        change.onClick.AddListener(() => ChangeCamera());
    }
}
