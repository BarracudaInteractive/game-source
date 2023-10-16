using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class freeze : MonoBehaviour
{
    public Button freezeGame;
    public GameObject Platonic_0;
    public Transform parent;    
    public int timeout = 0;
    public int rtime = 60;
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // Start is called before the first frame updat
    void Start()
    {
        Application.targetFrameRate = rtime;
        freezeGame = GameObject.FindGameObjectWithTag("FreezeButton").GetComponent<Button>();
        parent = GameObject.FindGameObjectWithTag("path").GetComponent<Transform>();
        freezeGame.onClick.AddListener(() => ResumeGame());
        PauseGame();
    }

    void Update()
    {
        timeout += 1;
        timeout %= rtime;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 10000);
            if (hit.transform)
            {
                if (hit.collider.tag == "ground" && timeout > 0)
                {
                    //Debug.Log("Spawned wp in: " + hit.point);
                    GameObject Platonic = Instantiate(Platonic_0, hit.point, Quaternion.identity, parent);
                    Debug.Log("Spawned wp: " + hit.point);
                    timeout = -rtime;
                }
            }
        }
        Debug.Log(timeout);
    }
}