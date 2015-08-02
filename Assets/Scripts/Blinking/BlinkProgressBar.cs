using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlinkProgressBar : MonoBehaviour
{
	private Image image;
	public BlinkController blinkController;
	
	void Start ()
	{
		image = GetComponent<Image> ();
	}

	void Update ()
	{
		//current progress of blink equals the percentage value of the ellapsed time. e.g. length till next blink - time passed so far / length till next blink
		image.fillAmount = (blinkController.getBlinkInterval () - blinkController.getEllapsedTime ()) / blinkController.getBlinkInterval ();
	}
}
