using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class scri : MonoBehaviour
{
    public GameObject logo;
    public GameObject idioma;
    public GameObject english;
    
    private Button _bEnglish;
    // Start is called before the first frame update

    private void _NextScene()
    {
        logo.SetActive(true);
        idioma.SetActive(false);
    }
    
    private void LoadGame() { SceneManager.LoadScene("Prefs"); }
    
    void Awake()
    {
        logo.SetActive(false);
        _bEnglish = english.GetComponent<Button>();
        _bEnglish.onClick.AddListener(() => _NextScene());
    }
}
