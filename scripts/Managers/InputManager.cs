using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    [HideInInspector] public Controller RR;

    [HideInInspector] public float vertical;
    [HideInInspector] public float horizontal;
    [HideInInspector] public float zet;
    [HideInInspector] public bool handbrake;
    [HideInInspector] public bool boosting;

    private TrackWaypoints waypoints;
    private Transform currentWaypoint;
    private List<Transform> nodes = new List<Transform>();

    [Header("AI values")] [Range(0, 1)] public float acceleration = 0.2f;
    private int distanceOffset = 3;
    [Range(0, 1)] public float steerForce = 0.2f;
    public int currentNode;

    private void Awake()
    {
        RR = GetComponent<Controller>();
    }

    private void Start()
    {
        //boton = GameObject.FindGameObjectWithTag("FreezeButton").GetComponent<Button>();
        //boton.onClick.AddListener(() => routing());
        waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>();
        currentWaypoint = gameObject.transform;
        nodes = waypoints.nodes;
    }

    private void FixedUpdate()
    {
        if (gameObject.tag == "AI") AIDrive();
    }

    private void AIDrive()
    {
        calculateDistanceOfWaypoints();
        AISteer();
        vertical = acceleration;

    }

    private void calculateDistanceOfWaypoints()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 difference = nodes[i].transform.position - position;
            float currentDistance = difference.magnitude;
            if (currentDistance < distance)
            {
                if ((i + distanceOffset) >= nodes.Count)
                {
                    currentWaypoint = nodes[1];
                    distance = currentDistance;
                }
                else
                {
                    currentWaypoint = nodes[i + distanceOffset];
                    distance = currentDistance;
                }

                currentNode = i;
            }
        }
    }

    private void AISteer()
    {
        Vector3 relative = transform.InverseTransformPoint(currentWaypoint.transform.position);
        relative /= relative.magnitude;
        horizontal = (relative.x / relative.magnitude) * steerForce;
        zet = (relative.z / relative.magnitude) * steerForce;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle") RR.dmg += RR.KPH * 0.167f;
    }
}