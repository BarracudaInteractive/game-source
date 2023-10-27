using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class SectionManager : MonoBehaviour
{
    private short high = 12;
    private TrackWaypoints waypoints;
    private Transform currentWaypoint;
    private List<Transform> nodes = new List<Transform> ();
    private List<float> steps = new List<float> { 8.62f, 8.53f, -10.0f, 36.87f, 70.28f, 4.90f, 53.67f, 138.80f, -4.76f, 30.0f};
    
    private int timeout = 0;
    private int rtime = 30;
    private GameObject _gameObject;
    
    [Header("Section Canvas")]
    public GameObject sectionSetCanvas;
    
    [Header("Pacenotes buttons")] //straight, shallow curve, tight curve, very tight curve, hairpin
    public GameObject st; 
    public GameObject sc;
    public GameObject tc;
    public GameObject vc;
    public GameObject hp;

    [HideInInspector] public Button b0;
    [HideInInspector] public Button b1;
    [HideInInspector] public Button b2;
    [HideInInspector] public Button b3;
    [HideInInspector] public Button b4;
    private short p = 0;
    
    [Header("Temper slider")] // 0.2 - 1.0
    public GameObject ts;
    
    [HideInInspector] public Slider s0;
    private float t = 0.2f;
    
    [Header("Submit button")] //straight, shallow curve, tight curve, very tight curve, hairpin
    public GameObject sb;

    [HideInInspector] public Button b5;
    
    [HideInInspector] public short id = 0;
    [HideInInspector] public int idx = 0;
    [HideInInspector] public bool rev = false;
    [HideInInspector] public Vector3 pos;
    [HideInInspector] public Vector3 rot;
    
    [Header("Text field")] //straight, shallow curve, tight curve, very tight curve, hairpin
    public TMP_Text txt;
    
    
    public void setPValue(short val) { p = val; }
    
    public void setTValue(float val) { t = val; }

    public void swapValues() { for (int i = 0; i < steps.Count; i++) steps[i] = -steps[i]; }
    
    public void moveNodes()
    {
        Vector3 mov;
        if (rev) swapValues();
        switch (p)
        {
            case 0:
                for (int i = 0; i < 3; i++)
                {
                    mov = new Vector3(pos.x, high, pos.z);
                    nodes[idx + i].position = mov;
                    nodes[idx + i].Rotate(0.0f, rot.y, pos.z + 0.0f, Space.Self);
                    nodes[idx + i].Translate(0.0f, 0.0f, (33.3f * (i + 1)), Space.Self);
                }
                break;
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    mov = new Vector3(pos.x, high, pos.z);
                    nodes[idx + i].position = mov;
                    nodes[idx + i].Rotate(0.0f, steps[0] + rot.y, 0.0f, Space.Self); //pos.z + 0.0f
                    nodes[idx + i].Translate(0.0f, 0.0f, (33.3f * (i + 1)), Space.Self);
                }
                break;
            case 2:
                mov = new Vector3(pos.x, high, pos.z);
                nodes[idx].position = mov;
                nodes[idx].Rotate(0.0f, steps[1] + rot.y, 0.0f, Space.Self);
                nodes[idx].Translate(steps[2], 0.0f, (60.66f), Space.Self);
                nodes[idx + 1].position = nodes[idx].position;
                nodes[idx + 1].Rotate(0.0f, steps[3] + rot.y, 0.0f, Space.Self);
                nodes[idx + 1].Translate(0.0f, 0.0f, (50.0f), Space.Self);
                nodes[idx + 2].position = nodes[idx + 1].position;
                nodes[idx + 2].Rotate(0.0f, steps[4] + rot.y, 0.0f, Space.Self);
                nodes[idx + 2].Translate(0.0f, 0.0f, (50.0f), Space.Self);
                break;
            case 3:
                mov = new Vector3(pos.x, high, pos.z);
                nodes[idx].position = mov;
                nodes[idx].Rotate(0.0f, steps[5] + rot.y, 0.0f, Space.Self);
                nodes[idx].Translate(steps[2], 0.0f, (70.26f), Space.Self);
                nodes[idx + 1].position = nodes[idx].position;
                nodes[idx + 1].Rotate(0.0f, steps[6] + rot.y, 0.0f, Space.Self);
                nodes[idx + 1].Translate(0.0f, 0.0f, (59.0f), Space.Self);
                nodes[idx + 2].position = nodes[idx + 1].position;
                nodes[idx + 2].Rotate(0.0f, steps[7] + rot.y, 0.0f, Space.Self);
                nodes[idx + 2].Translate(0.0f, 0.0f, (33.09f), Space.Self);
                break;
            case 4:
                mov = new Vector3(pos.x, high, pos.z);
                nodes[idx].position = mov;
                nodes[idx].Rotate(0.0f, steps[8] + rot.y, 0.0f, Space.Self);
                nodes[idx].Translate(steps[2], 0.0f, (60.21f), Space.Self);
                nodes[idx + 1].position = mov;
                nodes[idx + 1].Rotate(0.0f, rot.y, 0.0f, Space.Self);
                nodes[idx + 1].Translate(steps[9], 0.0f, (75.50f), Space.Self);
                nodes[idx + 2].position = mov;
                nodes[idx + 2].Rotate(0.0f, -steps[8] + rot.y, 0.0f, Space.Self);
                nodes[idx + 2].Translate(steps[9] * 2 - steps[2], 0.0f, (60.21f), Space.Self);
                break;
        }
        if (rev) swapValues();
    }
    
    public void submit() { startSectionSet(false); moveNodes(); _gameObject.GetComponent<Attrib>().SetSf(t); }
    
    public void initButtons()
    {
        b0 = st.GetComponent<Button>();
        b0.onClick.AddListener(() => setPValue(0));
        b1 = sc.GetComponent<Button>();
        b1.onClick.AddListener(() => setPValue(1));
        b2 = tc.GetComponent<Button>();
        b2.onClick.AddListener(() => setPValue(2));
        b3 = vc.GetComponent<Button>();
        b3.onClick.AddListener(() => setPValue(3));
        b4 = hp.GetComponent<Button>();
        b4.onClick.AddListener(() => setPValue(4));
    }

    public void initSlider()
    {
        s0 = ts.GetComponent<Slider>();
        s0.onValueChanged.AddListener(delegate { setTValue(s0.value); });
    }
    
    public void startSectionSet(bool b){ sectionSetCanvas.SetActive(b);  }
    
    public void textSet() { txt.text = "Section: " + id; }
    
    private void Awake()
    {
        initButtons();
        initSlider();
        b5 = sb.GetComponent<Button>();
        b5.onClick.AddListener(() => submit());
    }

    private void Start()
    {
        nodes = GameObject.FindGameObjectWithTag("path").GetComponent<TrackWaypoints>().nodes;
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
                if (hit.collider.tag == "section" && timeout > 0)
                {
                    timeout = -rtime;
                    id = hit.collider.GetComponent<Attrib>().GetId();
                    rev = hit.collider.GetComponent<Attrib>().IsReverse();
                    idx = id * 3;
                    startSectionSet(true);
                    textSet();
                    pos = hit.collider.transform.position;
                    rot = hit.collider.transform.rotation.eulerAngles;
                    _gameObject = hit.collider.gameObject;
                }
            }
        }
    }
}
