using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private Controller _Controller;
    private TrackWaypoints _Waypoints;
    private Transform _tCurrentWaypoint;
    private List<Transform> _NodesList = new List<Transform>();

    private float _fAcceleration = 0.2f;
    private int _iDistanceOffset = 5;
    private float _fSteerForce = 0.6f;
    private int _iCurrentNode;
    private float _fVertical;
    private float _fHorizontal;
    private float _fZet;

    private const float MIN_DAMAGE = 5.0f;
    private const float BASE_COLLISION_DAMAGE = 50.0f;

    public float SetAcceleration { set => _fAcceleration = value; }

    public float SetSteerForce { set => _fSteerForce = value; }
    
    public int SetDistanceOffset { set => _iDistanceOffset = value; }

    public float GetVertical => _fVertical;

    public float GetHorizontal => _fHorizontal;

    public float GetAcceleration => _fAcceleration;

    private void _CalculateDistanceOfWaypoints()
    {
        Vector3 position = gameObject.transform.position;
        float distance = Mathf.Infinity;

        for (int i = 0; i < _NodesList.Count; i++)
        {
            Vector3 difference = _NodesList[i].transform.position - position;
            float currentDistance = difference.magnitude;

            if (currentDistance < distance)
            {
                if ((i + _iDistanceOffset) >= _NodesList.Count)
                {
                    _tCurrentWaypoint = _NodesList[1];
                    distance = currentDistance;
                }
                else
                {
                    _tCurrentWaypoint = _NodesList[i + _iDistanceOffset];
                    distance = currentDistance;
                }

                _iCurrentNode = i;
            }
        }
    }

    private void _Steer()
    {
        Vector3 relative = transform.InverseTransformPoint(_tCurrentWaypoint.transform.position);
        relative /= relative.magnitude;
        _fHorizontal = (relative.x / relative.magnitude) * _fSteerForce;
        _fZet = (relative.z / relative.magnitude) * _fSteerForce;
    }

    private void Awake()
    {
        _Controller = GetComponent<Controller>();
        if (SceneManager.GetActiveScene().name == "Day1N" || SceneManager.GetActiveScene().name == "Day2A")
        {
            _fAcceleration = 0.2f;
            _iDistanceOffset = 3;
            _fSteerForce = 1f;
        }
    }

    private void Start()
    {
        _Waypoints = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>();
        _tCurrentWaypoint = gameObject.transform;
        _NodesList = _Waypoints.NodesList;
    }

    private void FixedUpdate()
    {
        _CalculateDistanceOfWaypoints();
        _Steer();
        _fVertical = _fAcceleration;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Transform massCenter = gameObject.transform.Find("mass").gameObject.GetComponent<Transform>();
            Transform objCenter = collision.gameObject.GetComponent<Transform>();
            Vector3 direction = objCenter.position - massCenter.position;
            direction.Normalize();
            float collisionAngle = Vector3.Angle(direction, massCenter.forward) + 1.0f;
            float damageReceived = (_Controller.GetKPH / 60) * (BASE_COLLISION_DAMAGE / ((collisionAngle / 22.5f) + 1));
            
            if (damageReceived > MIN_DAMAGE) _Controller.SetDmg = _Controller.GetDmg + damageReceived;
        }
    }
}