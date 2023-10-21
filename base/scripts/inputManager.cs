using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputManager : MonoBehaviour {

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public bool handbrake;
    [HideInInspector] public bool boosting;
    //private bool first = false;
    //public Button boton;
    
    
    // AI components

    private trackWaypoints waypoints;
    private Transform currentWaypoint;
    private List<Transform> nodes = new List<Transform> ();
    private int distanceOffset = 5;
    [Range(0,1)]public float steerForce = 0.2f;
    [Header("AI acceleration value")]
    [Range(0,1)]public float acceleration = 0.5f;
    public int currentNode;
    

    private void Start() {
        //boton = GameObject.FindGameObjectWithTag("FreezeButton").GetComponent<Button>();
        //boton.onClick.AddListener(() => routing());
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<trackWaypoints>();
        currentWaypoint = gameObject.transform;
        nodes = waypoints.nodes;
    }

    private void FixedUpdate()
    {
        //if (Time.timeScale != 0 && first)
        //{
            if (gameObject.tag == "AI") AIDrive();
            else if (gameObject.tag == "Player")
            {
                calculateDistanceOfWaypoints();
                keyboard();
            }
        //}
    }

    private void keyboard () {
        vertical = Input.GetAxis ("Vertical");
        horizontal = Input.GetAxis ("Horizontal");
        handbrake = (Input.GetAxis ("Jump") != 0) ? true : false;
        if (Input.GetKey (KeyCode.LeftShift)) boosting = true;
        else boosting = false;
    }

    private void AIDrive () {
        calculateDistanceOfWaypoints ();
        AISteer ();
        vertical = acceleration;

    }

    private void calculateDistanceOfWaypoints () {
        Vector3 position = gameObject.transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < nodes.Count; i++) {
            Vector3 difference = nodes[i].transform.position - position;
            float currentDistance = difference.magnitude;
            if (currentDistance < distance) {
                if ((i + distanceOffset) >= nodes.Count) {
                    currentWaypoint = nodes[1];
                    distance = currentDistance;
                } else {
                    currentWaypoint = nodes[i + distanceOffset];
                    distance = currentDistance;
                }
                currentNode = i;
            }
            
        }
        
    }

    private void AISteer () {

        Vector3 relative = transform.InverseTransformPoint (currentWaypoint.transform.position);
        relative /= relative.magnitude;

        horizontal = (relative.x / relative.magnitude) * steerForce;

    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "section")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("AHHHHHHHHHH");
        }
    }
}