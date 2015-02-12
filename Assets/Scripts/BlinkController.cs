using UnityEngine;
using System.Collections;

public class BlinkController : MonoBehaviour
{

	private float startTime;
	public GameObject blinkScreen;
	public static float ellapsedTime;
	public static float blinkInterval = 5f;
	public static float blinkDuration = 0.5f;
		
	void Start ()
	{
		StartCoroutine (blinkCountdown (blinkInterval));
	}

	void Update ()
	{
		//this will go from 0 to the blinkInterval, to be used with Progress bar.
		ellapsedTime = Time.time - startTime;
	}
	
	IEnumerator blinkCountdown (float blinkInterval)
	{
		while (true) {
			//reset the start time
			startTime = Time.time;
			//wait for the blink interval
			yield return new WaitForSeconds (blinkInterval);
						
			//-------------BLINK-----------------------//
			//1. Blacken Screen (Will do animation in future)
			blinkScreen.SetActive (true);

			//2. Move Ghosts.
			GameObject[] ghostGOs = GameObject.FindGameObjectsWithTag ("Ghost");
			foreach (GameObject g in ghostGOs) {
				g.GetComponent<Ghost_AI> ().move ();
			}

			//3. Wait For Blink Duration;
			yield return new WaitForSeconds (blinkDuration);

			//4. Unblacken Screen
			blinkScreen.SetActive (false);
		}
	}

	//OnPause of the game, the ellapsed Time will need to be taken out and passed back into the coroutine,
	//the coroutine will then use this parameter to activate a shorter blink than usual on resume.

}