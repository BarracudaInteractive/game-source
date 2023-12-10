using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class vkEnabler : MonoBehaviour
{
	[DllImport("__Internal")]
	private static extern bool IsMobile();
    
	public bool IsMobileWebGL()
	{
#if UNITY_WEBGL && !UNITY_EDITOR
            return IsMobile();
#endif
		return false;
	}
	
	public void ShowVirtualKeyboard()
	{
		if (!IsMobileWebGL()) return;
		try
		{
			TNVirtualKeyboard.instance.ShowVirtualKeyboard();
			TNVirtualKeyboard.instance.targetText = gameObject.GetComponent<TMP_InputField>();
		}
		catch {}
	}
}
