using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Controller : MonoBehaviour
{
    private enum DriveType 
    { 
        Front,
        Rear,
        AllWheel
    }
    
    [Header("Variables")]
    [SerializeField] private DriveType _Drive;
    public float[] gears;
    public float[] gearChangeSpeed;
    public float _fMinRPM;
    public float _fMaxRPM;
    public AnimationCurve enginePower;
    
    [Header("Workshop values")]
    public int _iCarPrice;
    public string _sCarName;
    
    private GameManager _GameManager;
    private InputManager _InputManager;
    //private CarEffects CarEffects;
    private int _iGearNum = 1;
    private float _fKPH;
    private float _fEngineRPM;
    private float _fGas = 0.0f;
    private float _fDemo = 100.0f; //demo mode to see the gas drop
    private float _fDmg = 0.0f;
    private float _fAccDmg = 0.0f;
    private bool _isReverse = false;
    private bool _isAcc = false;
    private GameObject _gWheelMeshes;
    private GameObject _gWheelColliders;
    private WheelCollider[] _Wheels = new WheelCollider[4];
    private GameObject[] _WheelMesh = new GameObject[4];
    private GameObject _gCenterOfMass;
    private Rigidbody _Rigidbody;
    
    private float _fSmoothTime = 0.1f;

    private WheelFrictionCurve _ForwardFriction;
    private WheelFrictionCurve _SidewaysFriction;
    private float _fRadius = 6.0f;
    private float _fBrakPower = 0.0f;
    private float _fDownForceValue = 10.0f;
    private float _fWheelsRPM;
    private float _fDriftFactor;
    private float _fLastEngineRPM;
    private float _fHorizontal;
    private float _fVertical;
    private float _fTotalPower;
    private float _fLastKPH = 0.0f;
    private float _fSpeedBaseDmg = 15.0f;
    private bool _isBelowRPM = false;
    
    public bool IsAcc => _isAcc;
    
    public float GetMaxRPM => _fMaxRPM;
    
    public float GetDmg => _fDmg;
    
    public float SetDmg { set => _fDmg = value; }
    
    public float GetGas => _fGas;
    
    public bool IsReverse => _isReverse;
    
    public float GetEngineRPM => _fEngineRPM;
    
    public float GetKPH => _fKPH;
    
    public int GetGear => _iGearNum;
    
    public string GetCarName => _sCarName;
    
    public int GetCarPrice => _iCarPrice;
    
    private void _CalculateEnginePower()
    {
        _CalculateWheelRPMnChangeGear();
        
        if (_fVertical != 0 ) _Rigidbody.drag = 0.005f; 
        if (_fVertical == 0) _Rigidbody.drag = 0.1f; 
        
        _fTotalPower = 3.6f * enginePower.Evaluate(_fEngineRPM) * (_fVertical);
        float velocity  = 0.0f;
        
        if (_fEngineRPM >= _fMaxRPM || _isBelowRPM)
        {
            _fEngineRPM = Mathf.SmoothDamp(_fEngineRPM, _fMaxRPM - 500, ref velocity, 0.05f);
            _isBelowRPM = (_fEngineRPM >= _fMaxRPM - 450);
            _isAcc = (_fLastEngineRPM > _fEngineRPM);
        }
        else 
        { 
            _fEngineRPM = Mathf.SmoothDamp(_fEngineRPM,1000 + (Mathf.Abs(_fWheelsRPM) * 3.6f * (gears[_iGearNum])), ref velocity , _fSmoothTime);
            _isAcc = false;
        }
        if (_fEngineRPM >= _fMaxRPM + 1000) _fEngineRPM = _fMaxRPM + 1000;
        
        _MoveVehicle();
        _Shifter();
    }

    private void _CalculateWheelRPMnChangeGear()
    {
        float sum = 0;
        int R = 0;
        
        for (int i = 0; i < 4; i++)
        {
            sum += _Wheels[i].rpm;
            R++;
        }
        
        _fWheelsRPM = (R != 0) ? sum / R : 0;
 
        if (_fWheelsRPM < 0 && !_isReverse )
        {
            _isReverse = true;
            //_GameManager.changeGear();
        }
        else if (_fWheelsRPM > 0 && _isReverse)
        {
            _isReverse = false;
            //_GameManager.changeGear();
        }
    }

    private bool _CheckGears() { return _fKPH >= gearChangeSpeed[_iGearNum]; }

    private void _Shifter()
    {
        if(!_IsGrounded())return;
        
        //automatic
        if(_fEngineRPM > _fMaxRPM && _iGearNum < gears.Length - 1 && !_isReverse && _CheckGears() )
        {
            _iGearNum++;
            //_GameManager.changeGear();
            return;
        }
        
        if(_fEngineRPM < _fMinRPM && _iGearNum > 0)
        {
            _iGearNum--;
            //_GameManager.changeGear();
        }
    }
 
    private bool _IsGrounded()
    {
        if (_Wheels[0].isGrounded && _Wheels[1].isGrounded && _Wheels[2].isGrounded && _Wheels[3].isGrounded) return true;
        else return false;
    }

    private void _MoveVehicle()
    {
        _BrakeVehicle();

        if (_Drive == DriveType.AllWheel)
        {
            for (int i = 0; i < _Wheels.Length; i++)
            {
                _Wheels[i].motorTorque = _fTotalPower / 4;
                _Wheels[i].brakeTorque = _fBrakPower;
            }
        }
        else if(_Drive == DriveType.Rear)
        {
            _Wheels[2].motorTorque = _fTotalPower / 2;
            _Wheels[3].motorTorque = _fTotalPower / 2;

            for (int i = 0; i < _Wheels.Length; i++)
                _Wheels[i].brakeTorque = _fBrakPower;
        }
        else
        {
            _Wheels[0].motorTorque = _fTotalPower / 2;
            _Wheels[1].motorTorque = _fTotalPower / 2;

            for (int i = 0; i < _Wheels.Length; i++)
                _Wheels[i].brakeTorque = _fBrakPower;
        }
        _fKPH = _Rigidbody.velocity.magnitude * 3.6f;
    }

    private void _BrakeVehicle()
    {
        if (_fVertical < 0) 
            _fBrakPower =(_fKPH >= 10)? 500 : 0;
        else if (_fVertical == 0 && (_fKPH <= 10 || _fKPH >= -10))
            _fBrakPower = 10;
        else
            _fBrakPower = 0;
    }
  
    private void _SteerVehicle()
    {
        //Ackerman steering formula
        if (_fHorizontal > 0 ) 
        {
            _Wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_fRadius + (1.5f / 2))) * _fHorizontal;
            _Wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_fRadius - (1.5f / 2))) * _fHorizontal;
        } 
        else if (_fHorizontal < 0 ) 
        {                                                          
            _Wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_fRadius - (1.5f / 2))) * _fHorizontal;
            _Wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (_fRadius + (1.5f / 2))) * _fHorizontal;
        } 
        else 
        {
            _Wheels[0].steerAngle =0;
            _Wheels[1].steerAngle =0;
        }
    }

    private void _AnimateWheels ()
	{
		Vector3 wheelPosition = Vector3.zero;
		Quaternion wheelRotation = Quaternion.identity;

		for (int i = 0; i < 4; i++) 
        {
			_Wheels [i].GetWorldPose (out wheelPosition, out wheelRotation);
			_WheelMesh [i].transform.position = wheelPosition;
			_WheelMesh [i].transform.rotation = wheelRotation;
		}
	}
   
    private void _GetObjects()
    {
        //CarEffects = GetComponent<CarEffects>();
        _InputManager = GetComponent<InputManager>();
        _GameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        _Rigidbody = GetComponent<Rigidbody>();
        _gWheelColliders = gameObject.transform.Find("wheelColliders").gameObject;
        _gWheelMeshes = gameObject.transform.Find("wheelMeshes").gameObject;
        
        _Wheels[0] = _gWheelColliders.transform.Find("0").gameObject.GetComponent<WheelCollider>();
        _Wheels[1] = _gWheelColliders.transform.Find("1").gameObject.GetComponent<WheelCollider>();
        _Wheels[2] = _gWheelColliders.transform.Find("2").gameObject.GetComponent<WheelCollider>();
        _Wheels[3] = _gWheelColliders.transform.Find("3").gameObject.GetComponent<WheelCollider>();
        
        _WheelMesh[0] = _gWheelMeshes.transform.Find("0").gameObject;
        _WheelMesh[1] = _gWheelMeshes.transform.Find("1").gameObject;
        _WheelMesh[2] = _gWheelMeshes.transform.Find("2").gameObject;
        _WheelMesh[3] = _gWheelMeshes.transform.Find("3").gameObject;
        
        _gCenterOfMass = gameObject.transform.Find("mass").gameObject;
        _Rigidbody.centerOfMass = _gCenterOfMass.transform.localPosition;   
    }

    private void _AddDownForce() { _Rigidbody.AddForce(-transform.up * (_fDownForceValue * _Rigidbody.velocity.magnitude)); }

    private void _DmgLoop ()
    {
        if (_fKPH > _fSpeedBaseDmg) _fAccDmg += .033f * (_fKPH/_fSpeedBaseDmg);
        else _fAccDmg += .033f;
    }
    
    private void Awake() 
    {
        if(SceneManager.GetActiveScene().name == "Prefs") return;
        _GetObjects();
        StartCoroutine(_SteerRadiusUpdate());
        StartCoroutine(_OilUpdate());
    }
    
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Prefs") return;
        if (!_IsGrounded()) _DmgLoop();
        else { _fDmg += _fAccDmg; _fAccDmg = 0.0f; }
    }

    private void Update() 
    {
        if (SceneManager.GetActiveScene().name == "Prefs") return;
        _fHorizontal = _InputManager.GetHorizontal;
        _fVertical = _InputManager.GetVertical;
        _fLastEngineRPM = _fEngineRPM;
        _AddDownForce();
        _AnimateWheels();
        _SteerVehicle();
        _CalculateEnginePower();
    }
    
    private IEnumerator _SteerRadiusUpdate()
    {
		while(true)
        {
			yield return new WaitForSeconds(.7f);
            _fRadius = 6 + _fKPH / 20;
		}
	}

    private IEnumerator _OilUpdate()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            if (_IsGrounded()) //flying does not consume oil
            {
                //decelerate
                if (_fLastKPH > _fKPH) _fGas += (float)0.015*_fDemo; 
                //accelerate
                else if (_fKPH / gearChangeSpeed[_iGearNum] > 0.8f) _fGas += (float)0.02*_fDemo; 
                //accelerate slightly
                else if (_fKPH / gearChangeSpeed[_iGearNum] <= 0.8f || _fKPH > 20.0f) _fGas += (float)0.015*_fDemo; 
            }
            _fLastKPH = _fKPH;
        }
    }
}
