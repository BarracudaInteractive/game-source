using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public List<Camera> Cameras;

    public AudioClip _PressButtonSE;
    
    private AudioSource _aSource;
    
    public void PressButtonSE() { _aSource.PlayOneShot(_PressButtonSE); }
    
    private void Awake() { _aSource = GetComponent<AudioSource>(); }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
            return;
        
        foreach (Camera camera in Cameras)
            if (camera.isActiveAndEnabled)
                this.transform.localPosition = camera.transform.position;
    }
}
