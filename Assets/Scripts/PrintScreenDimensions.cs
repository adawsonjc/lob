using UnityEngine;
using System.Collections;

public class PrintScreenDimensions : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log ("Screen Width: " + Screen.width);
		Debug.Log ("Screen Height: " + Screen.height);
	}
}
