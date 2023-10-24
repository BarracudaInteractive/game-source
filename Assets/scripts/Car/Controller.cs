using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    internal enum driveType{ frontWheelDrive, rearWheelDrive, allWheelDrive }
    [SerializeField]private driveType drive;
    
    private GameManager manager;
    private InputManager IM;
    private CarEffects CarEffects;
    [HideInInspector]public bool test; 

    [Header("Variables")]
    public float handBrakeFrictionMultiplier = 2f;
    public float maxRPM , minRPM;
    public float[] gears;
    public float[] gearChangeSpeed;
    public AnimationCurve enginePower;

    [HideInInspector]public int gearNum = 1;
    [HideInInspector]public bool playPauseSmoke = false, hasFinished;
    [HideInInspector]public float KPH;
    [HideInInspector]public float engineRPM;
    [HideInInspector]public bool reverse = false;
    [HideInInspector]public float gas = 0.0f;
    [HideInInspector]private float demo = 100.0f; //demo mode to see the gas drop
    [HideInInspector]public float dmg = 0.0f, accDmg = 0.0f;

    private GameObject wheelMeshes,wheelColliders;
    private WheelCollider[] wheels = new WheelCollider[4];
    private GameObject[] wheelMesh = new GameObject[4];
    private GameObject centerOfMass;
    private Rigidbody rigidbody;

    //car Shop Values
    public int carPrice ;
    public string carName;
    private float smoothTime = 0.09f;
    
	private WheelFrictionCurve  forwardFriction,sidewaysFriction;
    private float radius = 6, brakPower = 0, DownForceValue = 10f,wheelsRPM ,driftFactor, lastValue ,horizontal , vertical, totalPower, lastKPH = 0;
    private bool flag = false;
    private float speedBaseDmg = 15.0f;
    
    private void Awake() 
    {
        if(SceneManager.GetActiveScene().name == "awakeScene") return;
        getObjects();
        StartCoroutine(timedLoop());
        StartCoroutine(oilSet());
    }

    private void Update() 
    {
        if (SceneManager.GetActiveScene().name == "awakeScene") return;
        horizontal = IM.horizontal;
        vertical = IM.vertical;
        lastValue = engineRPM;
        addDownForce();
        animateWheels();
        steerVehicle();
        calculateEnginePower();
    }

    private void calculateEnginePower(){
        wheelRPM();
        
        if (vertical != 0 ){ rigidbody.drag = 0.005f; }
        if (vertical == 0){ rigidbody.drag = 0.1f; }
        totalPower = 3.6f * enginePower.Evaluate(engineRPM) * (vertical);
        float velocity  = 0.0f;
        
        if (engineRPM >= maxRPM || flag)
        {
            engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - 500, ref velocity, 0.05f);
            flag = (engineRPM >= maxRPM - 450);
            test = (lastValue > engineRPM);
        }
        else 
        { 
            engineRPM = Mathf.SmoothDamp(engineRPM,1000 + (Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity , smoothTime);
            test = false;
        }
        if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000;
        moveVehicle();
        shifter();
    }

    private void wheelRPM(){
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
        wheelsRPM = (R != 0) ? sum / R : 0;
 
        if(wheelsRPM < 0 && !reverse ){
            reverse = true;
            manager.changeGear();
        }
        else if(wheelsRPM > 0 && reverse){
            reverse = false;
            manager.changeGear();
        }
    }

    private bool checkGears(){
        if(KPH >= gearChangeSpeed[gearNum] ) return true;
        else return false;
    }

    private void shifter(){

        if(!isGrounded())return;
            //automatic
        if(engineRPM > maxRPM && gearNum < gears.Length-1 && !reverse && checkGears() ){
            gearNum ++;
            manager.changeGear();
            return;
        }
        if(engineRPM < minRPM && gearNum > 0){
            gearNum --;
            manager.changeGear();
        }

    }
 
    public bool isGrounded()
    {
        //try { if (wheels[0].isGrounded && wheels[1].isGrounded && wheels[2].isGrounded && wheels[3].isGrounded) return true; }
        //catch (Exception e) { return false; }
        if (wheels[0].isGrounded && wheels[1].isGrounded && wheels[2].isGrounded && wheels[3].isGrounded) return true;
        else return false;
    }

    private void moveVehicle()
    {
        brakeVehicle();

        if (drive == driveType.allWheelDrive){
            for (int i = 0; i < wheels.Length; i++){
                wheels[i].motorTorque = totalPower / 4;
                wheels[i].brakeTorque = brakPower;
            }
        }else if(drive == driveType.rearWheelDrive){
            wheels[2].motorTorque = totalPower / 2;
            wheels[3].motorTorque = totalPower / 2;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = brakPower;
            }
        }
        else{
            wheels[0].motorTorque = totalPower / 2;
            wheels[1].motorTorque = totalPower / 2;

            for (int i = 0; i < wheels.Length; i++)
            {
                wheels[i].brakeTorque = brakPower;
            }
        }
        KPH = rigidbody.velocity.magnitude * 3.6f;
    }

    private void brakeVehicle()
    {
        if (vertical < 0){
            brakPower =(KPH >= 10)? 500 : 0;
        }
        else if (vertical == 0 &&(KPH <= 10 || KPH >= -10)){
            brakPower = 10;
        }
        else{
            brakPower = 0;
        }
    }
  
    private void steerVehicle()
    {
        //Ackerman steering formula
        if (horizontal > 0 ) {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
        } else if (horizontal < 0 ) {                                                          
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * horizontal;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * horizontal;
        } else {
            wheels[0].steerAngle =0;
            wheels[1].steerAngle =0;
        }
    }

    private void animateWheels ()
	{
		Vector3 wheelPosition = Vector3.zero;
		Quaternion wheelRotation = Quaternion.identity;

		for (int i = 0; i < 4; i++) {
			wheels [i].GetWorldPose (out wheelPosition, out wheelRotation);
			wheelMesh [i].transform.position = wheelPosition;
			wheelMesh [i].transform.rotation = wheelRotation;
		}
	}
   
    private void getObjects(){
        IM = GetComponent<InputManager>();
        manager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        CarEffects = GetComponent<CarEffects>();
        rigidbody = GetComponent<Rigidbody>();
        wheelColliders = gameObject.transform.Find("wheelColliders").gameObject;
        wheelMeshes = gameObject.transform.Find("wheelMeshes").gameObject;
        wheels[0] = wheelColliders.transform.Find("0").gameObject.GetComponent<WheelCollider>();
        wheels[1] = wheelColliders.transform.Find("1").gameObject.GetComponent<WheelCollider>();
        wheels[2] = wheelColliders.transform.Find("2").gameObject.GetComponent<WheelCollider>();
        wheels[3] = wheelColliders.transform.Find("3").gameObject.GetComponent<WheelCollider>();
        wheelMesh[0] = wheelMeshes.transform.Find("0").gameObject;
        wheelMesh[1] = wheelMeshes.transform.Find("1").gameObject;
        wheelMesh[2] = wheelMeshes.transform.Find("2").gameObject;
        wheelMesh[3] = wheelMeshes.transform.Find("3").gameObject;
        centerOfMass = gameObject.transform.Find("mass").gameObject;
        rigidbody.centerOfMass = centerOfMass.transform.localPosition;   
    }

    private void addDownForce(){ rigidbody.AddForce(-transform.up * (DownForceValue * rigidbody.velocity.magnitude)); }

    private void dmgLoop()
    {
        if (KPH > speedBaseDmg) accDmg += .033f * (KPH/speedBaseDmg);
        else accDmg += .033f;
    }
    
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "awakeScene") return;
        if (!isGrounded()) dmgLoop();
        else { dmg += accDmg; accDmg = 0.0f; }
    }

    private IEnumerator timedLoop(){
		while(true){
			yield return new WaitForSeconds(.7f);
            radius = 6 + KPH / 20;
		}
	}

    private IEnumerator oilSet(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            if (isGrounded()) //flying does not consume oil
            {
                if (lastKPH > KPH) gas += (float)0.015*demo; //decelerate
                else if (KPH / gearChangeSpeed[gearNum] > 0.8f) gas += (float)0.02*demo; //accelerate
                else if (KPH / gearChangeSpeed[gearNum] <= 0.8f || KPH > 20.0f) gas += (float)0.015*demo; //accelerate slightly
            }
            lastKPH = KPH;
        }
    }
}
