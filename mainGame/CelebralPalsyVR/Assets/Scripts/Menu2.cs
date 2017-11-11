using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu2 : MonoBehaviour {

	public Canvas MainCanvas;
	public Canvas OptionCanvas;

	void Awake()
	{
		OptionCanvas.enabled = false;
	}

	public void OptionsOn()
	{
		OptionCanvas.enabled = true;
		MainCanvas.enabled = false;

	}

	public void ReturnOn()
	{
		OptionCanvas.enabled = false;
		MainCanvas.enabled = true;
	}

	public void LoadOn()
	{
		Application.LoadLevel("Motor");
	}

}
