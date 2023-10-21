using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sections : MonoBehaviour
{
    public Dictionary<string,GameObject> sectionList = new Dictionary<string,GameObject>();

    private void Awake()
    {
        /*foreach(Transform gameObj in this.transform)
            foreach(Transform burrito in gameObj.transform)
                if (burrito.tag == "section")
                {
                sectionList.Add(burrito.GetComponent<coll>()); Debug.Log("Soy la seccion " + burrito.name);
                }*/
    }
}
