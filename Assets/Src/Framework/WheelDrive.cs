using UnityEngine;
using System;
using UnityEngine.Serialization;

public class WheelDrive : MonoBehaviour
{
	private enum DriveType
	{
		Front,
		Rear,
		AllWheel
	}
	
    [Tooltip("Maximum steering angle of the wheels")]
	public float fMaxSteeringAngle = 45.0f;
	[Tooltip("Maximum torque applied to the driving wheels")]
	public float fMaxTorque = 300.0f;
	[Tooltip("Maximum brake torque applied to the driving wheels")]
	public float fBrakeTorque = 30000.0f;
	[Tooltip("If you need the visual wheels to be attached automatically, drag the wheel shape here.")]
	public GameObject gWheelShape;

	[Tooltip("The vehicle's speed when the physics engine can use different amount of sub-steps (in m/s).")]
	public float fCriticalSpeed = 5f;
	[Tooltip("Simulation sub-steps when the speed is above critical.")]
	public int iStepsBelow = 5;
	[Tooltip("Simulation sub-steps when the speed is below critical.")]
	public int iStepsAbove = 1;
	
	[Tooltip("The vehicle's drive type: rear-wheels drive, front-wheels drive or all-wheels drive.")]
	[SerializeField] private DriveType Drive;
	
    private WheelCollider[] _Wheels;
    
    // Find all the WheelColliders down in the hierarchy.
	void Start()
	{
		_Wheels = GetComponentsInChildren<WheelCollider>();

		for (int i = 0; i < _Wheels.Length; ++i) 
		{
			var wheel = _Wheels [i];

			// Create wheel shapes only when needed.
			if (gWheelShape != null)
			{
				var ws = Instantiate (gWheelShape);
				ws.transform.parent = wheel.transform;
			}
		}
	}

	// This is a really simple approach to updating wheels.
	// We simulate a rear wheel drive car and assume that the car is perfectly symmetric at local zero.
	// This helps us to figure our which wheels are front ones and which are rear.
	void Update()
	{
		_Wheels[0].ConfigureVehicleSubsteps(fCriticalSpeed, iStepsBelow, iStepsAbove);

		float angle = fMaxSteeringAngle * Input.GetAxis("Horizontal");
		float torque = fMaxTorque * Input.GetAxis("Vertical");

		float handBrake = Input.GetKey(KeyCode.X) ? fBrakeTorque : 0;

		foreach (WheelCollider wheel in _Wheels)
		{
			// A simple car where front wheels steer while rear ones drive.
			if (wheel.transform.localPosition.z > 0)
				wheel.steerAngle = angle;

			if (wheel.transform.localPosition.z < 0)
				wheel.brakeTorque = handBrake;
			
			if (wheel.transform.localPosition.z < 0 && Drive != DriveType.Front)
				wheel.motorTorque = torque;
			
			if (wheel.transform.localPosition.z >= 0 && Drive != DriveType.Rear)
				wheel.motorTorque = torque;

			// Update visual wheels if any.
			if (gWheelShape) 
			{
				Quaternion q;
				Vector3 p;
				wheel.GetWorldPose (out p, out q);

				// Assume that the only child of the wheelcollider is the wheel shape.
				Transform shapeTransform = wheel.transform.GetChild (0);

                if (wheel.name == "a0l" || wheel.name == "a1l" || wheel.name == "a2l")
                {
                    shapeTransform.rotation = q * Quaternion.Euler(0, 180, 0);
                    shapeTransform.position = p;
                }
                else
                {
                    shapeTransform.position = p;
                    shapeTransform.rotation = q;
                }
			}
		}
	}
}
