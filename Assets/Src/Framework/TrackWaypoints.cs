using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrackWaypoints : MonoBehaviour{

    public Color LineColor;
    [Range(0, 1)] public float fSphereRadius;
    public List<Transform> NodesList = new List<Transform>();

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = LineColor;
        Transform[] path = GetComponentsInChildren<Transform>();

        NodesList = new List<Transform>();
        
        for (int i = 1; i < path.Length; i++) 
        {
            NodesList.Add(path[i]);
        }

        for (int i = 0; i < NodesList.Count; i++) 
        {
            Vector3 currentWaypoint = NodesList[i].position;
            Vector3 previousWaypoint = Vector3.zero;

            if (i != 0) previousWaypoint = NodesList[i - 1].position;
            else if (i == 0) previousWaypoint = NodesList[NodesList.Count - 1].position;

            Gizmos.DrawLine(previousWaypoint,currentWaypoint);
            Gizmos.DrawSphere(currentWaypoint, fSphereRadius);
        }
    }

}