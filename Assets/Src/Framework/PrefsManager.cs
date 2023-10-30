using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PrefsManager : MonoBehaviour
{

    [Header("Camera")]
    public GameObject gCameraObject;
    public GameObject gFinalCameraPosition;
    public GameObject gStartCameraPosition;

    [Header("Deafault Canvas")]
    public GameObject gDeafaultCanvas;
    public Text tDeafaultCanvasCurrency;

    [Header("Map Canvas")]
    public GameObject gMapSelectorCanvas;

    [Header("Upgrades Canvas")]
    public Text tUpgradesCurrency;
    public GameObject gUpgradesCanvas;

    [Header("Vehicle Select Canvas")]
    public GameObject gVehicleSelectCanvas;
    public GameObject gBuyButton;
    public GameObject gStartButton;
    public VehicleList ListOfVehicles;
    public Text tCurrency;
    public Text tCarInfo;
    
    public GameObject gToRotate;
    
    private float fLerpTime = 2.0f;
    private float _fRotateSpeed = 5f;
    private int _iVehiclePointer = 0;
    private bool _finalToStart;
    private bool _startToFinal;

    private void _RightButton()
    {
        if (_iVehiclePointer < ListOfVehicles.Vehicles.Length-1)
        {
            Destroy(GameObject.FindGameObjectWithTag("AI"));
            _iVehiclePointer++;
            PlayerPrefs.SetInt("pointer",_iVehiclePointer);
            GameObject childObject = Instantiate(ListOfVehicles.Vehicles[_iVehiclePointer],Vector3.zero,gToRotate.transform.rotation) as GameObject;
            childObject.transform.parent = gToRotate.transform;
            _GetCarInfo();
        }
    }

    private void _LeftButton()
    {
        if (_iVehiclePointer > 0)
        {
            Destroy(GameObject.FindGameObjectWithTag("AI"));
            _iVehiclePointer--;
            PlayerPrefs.SetInt("pointer",_iVehiclePointer);
            GameObject childObject = Instantiate(ListOfVehicles.Vehicles[_iVehiclePointer],Vector3.zero,gToRotate.transform.rotation) as GameObject;
            childObject.transform.parent = gToRotate.transform;
            _GetCarInfo();
        }
    }

    private void _StartGameButton()
    {
        gDeafaultCanvas.SetActive(false);
        gVehicleSelectCanvas.SetActive(false);
        gMapSelectorCanvas.SetActive(true);
    }

    private void _BuyButton()
    {
        if (PlayerPrefs.GetInt("currency") >= ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarPrice)
        {
            PlayerPrefs.SetInt("currency", PlayerPrefs.GetInt("currency") - ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarPrice);
            PlayerPrefs.SetString(ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarName.ToString(), 
                ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarName.ToString());
            _GetCarInfo();
        }
    }

    private void _GetCarInfo()
    { 
        if (ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarName.ToString() == 
                                   PlayerPrefs.GetString(ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarName.ToString()))
        {
            tCarInfo.text = $"Owned";
            gStartButton.SetActive(true);
            gBuyButton.SetActive(false);
            tCurrency.text = $"${PlayerPrefs.GetInt("currency").ToString("")}";
            return;

        }
        
        tCurrency.text = $"${PlayerPrefs.GetInt("currency").ToString("")}";
        tCarInfo.text = $"{ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarName.ToString()} ${ListOfVehicles.Vehicles[PlayerPrefs.GetInt("pointer")].GetComponent<Controller>().GetCarPrice.ToString()}"; 
        gStartButton.SetActive(false);
        gBuyButton.SetActive(gBuyButton);
    }

    private void _DeafaultCanvasStartButton()
    {
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(false);
        gVehicleSelectCanvas.SetActive(true);
        gUpgradesCanvas.SetActive(false);
        _startToFinal = true;
        _finalToStart = false;
    }

    private void _VehicleSelectCanvasStartButton()
    {
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(true);
        gVehicleSelectCanvas.SetActive(false);
        _finalToStart = true;
        _startToFinal = false;
    }

    private void _UpgradesCanvasButton()
    {
        gUpgradesCanvas.SetActive(true);
        gVehicleSelectCanvas.SetActive(false);
    }

    private void _CameraTransition()
    {
        if (_startToFinal)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position,gFinalCameraPosition.transform.position,fLerpTime * Time.deltaTime); 
        if (_finalToStart)
            gCameraObject.transform.position = Vector3.Lerp(gCameraObject.transform.position,gStartCameraPosition.transform.position,fLerpTime * Time.deltaTime); 
    }
    
    private void LoadTesting() { SceneManager.LoadScene("Testing"); }
    
    private void Awake() 
    {
        Application.targetFrameRate = 60;
        
        gMapSelectorCanvas.SetActive(false);
        gDeafaultCanvas.SetActive(true);
        gVehicleSelectCanvas.SetActive(false);
        
        PlayerPrefs.SetInt("currency",80000);
        tDeafaultCanvasCurrency.text = $"${PlayerPrefs.GetInt("currency").ToString()}";
        tUpgradesCurrency.text = $"${PlayerPrefs.GetInt("currency").ToString()}";
        
        _iVehiclePointer = PlayerPrefs.GetInt("pointer");
        GameObject childObject = Instantiate(ListOfVehicles.Vehicles[_iVehiclePointer],Vector3.zero,gToRotate.transform.rotation) as GameObject;
        childObject.transform.parent = gToRotate.transform;
        _GetCarInfo();
    }

    private void FixedUpdate() 
    {
        gToRotate.transform.Rotate(Vector3.up * _fRotateSpeed * Time.deltaTime);
        _CameraTransition();
    }
}
