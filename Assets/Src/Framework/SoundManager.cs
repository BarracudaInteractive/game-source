using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public List<Camera> Cameras;

    public AudioClip _PressButtonSE;
    public AudioClip _GoodSelectionSE;
    public AudioClip _BadSelectionSE;
    public AudioClip _EndSE;
    
    private AudioSource _aSource;

    public void SetVolume(float f) { _aSource.volume = f; }
    
    public void GoodSelectionSE() { _aSource.PlayOneShot(_GoodSelectionSE); }
    
    public void BadSelectionSE() { _aSource.PlayOneShot(_BadSelectionSE); }
    
    public void PressButtonSE() { _aSource.PlayOneShot(_PressButtonSE); }
    
    public void EndSE() { _aSource.PlayOneShot(_EndSE); }
    
    private void Awake() { _aSource = GetComponent<AudioSource>(); }
    
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Prefs")
            return;
        
        foreach (Camera camera in Cameras)
            if (camera.isActiveAndEnabled)
                this.transform.localPosition = camera.transform.position;
    }
}
