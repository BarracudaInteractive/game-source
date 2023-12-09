using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TNVirtualKeyboard : MonoBehaviour
{
	public static TNVirtualKeyboard instance;
	public string words = "";
	public GameObject vkCanvas;
	public TMP_InputField targetText;

	void Start()
    {
        instance = this;
		HideVirtualKeyboard();
    }
	
	public void KeyPress(string k)
	{
		words += k;
		targetText.text = words;	
	}
	
	public void Del()
	{
		try
		{ 
			words = words.Remove(words.Length - 1, 1);
			targetText.text = words;	
		}
		catch {}
	}
	
	public void ShowVirtualKeyboard()
	{
		words = "";
		vkCanvas.SetActive(true);
	}
	
	public void HideVirtualKeyboard() { vkCanvas.SetActive(false); }
}
