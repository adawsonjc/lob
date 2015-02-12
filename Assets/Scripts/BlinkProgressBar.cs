﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkProgressBar : MonoBehaviour
{

	private Image image;

	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		image.fillAmount = (BlinkController.blinkInterval - BlinkController.ellapsedTime) / BlinkController.blinkInterval;
	}
}
