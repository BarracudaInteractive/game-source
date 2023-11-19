using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    private int c = 0;
    
    // Start is called before the first frame update
    void OnClick(int i)
    {
        
        ScreenCapture.CaptureScreenshot($"{Application.dataPath}\\Src\\Framework\\" + i + ".png");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnClick(c);
            c++;
        }
    }
}
