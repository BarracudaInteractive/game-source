using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sections : MonoBehaviour
{
    // Start is called before the first frame update
    public Color linecolor;
    [Range(0, 1)] public float SphereRadius;
    public List<Transform> secs = new List<Transform>();


    private void OnDrawGizmosSelected(){

        Gizmos.color = linecolor;
        
        foreach(Transform section in this.transform){
            if (section.name.Contains("Section"))
            {
                secs.Add(section);
                Debug.Log("Section added");
            }
        }
        Transform[] path = GetComponentsInChildren<Transform>();

        secs = new List<Transform>();
        for (int i = 1; i < path.Length; i++) 
            if (path[i].name.Contains("Section")) secs.Add(path[i]);

        for (int i = 0; i < secs.Count; i++) {
            Vector3 currentSection = secs[i].position;
            Vector3 previousSection = Vector3.zero;

            if (i != 0) previousSection = secs[i - 1].position;
            else if (i == 0) previousSection = secs[secs.Count - 1].position;

            Gizmos.DrawLine(previousSection,currentSection);
            Gizmos.DrawSphere(currentSection, SphereRadius);
        }
    }

}
