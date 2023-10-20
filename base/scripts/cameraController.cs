using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    private GameObject car;
    private controller RR;
    private GameObject cameralookAt,cameraPos;
    private float speed = 0;
    private float defaltFOV = 0, desiredFOV = 0;
    [Range (0, 50)] public float smothTime = 8;

    private void Start () {
        car = GameObject.FindGameObjectWithTag ("AI");
        RR = car.GetComponent<controller> ();
        cameralookAt = car.transform.Find ("camera lookAt").gameObject;
        cameraPos = car.transform.Find ("camera constraint").gameObject;

        defaltFOV = Camera.main.fieldOfView;
        desiredFOV = defaltFOV + 15;
    }

    private void FixedUpdate () {
        follow ();
        boostFOV ();

    }
    private void follow () {
        speed = RR.KPH / smothTime;
        gameObject.transform.position = Vector3.Lerp (transform.position, cameraPos.transform.position ,  Time.deltaTime * speed);
        gameObject.transform.LookAt (cameralookAt.gameObject.transform.position);
    }
    private void boostFOV () {

        if (RR.nitrusFlag)
            Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, desiredFOV, Time.deltaTime * 5);
        else
            Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, defaltFOV, Time.deltaTime * 5);

    }

}