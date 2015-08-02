using UnityEngine;
using System.Collections;

public class BlinkController : MonoBehaviour
{
	//TODO
	//OnPause of the game, the ellapsed Time will need to be taken out and passed back into the coroutine,
	//the coroutine will then use this parameter to activate a shorter blink than usual on resume.

	private float startTime;
	private float rememberEllapsedTime;
	public GameObject blinkScreen;
	private float ellapsedTime;
	public float blinkInterval = 5f;
	public float blinkDuration = 0.2f;

	private IEnumerator blinkCountdownIEnumerator;

	void Start ()
	{
		blinkCountdownIEnumerator = blinkCountdown ();
		StartCoroutine (blinkCountdownIEnumerator);
	}

	void Update ()
	{
		//this will go from 0 to the blinkInterval, to be used with Progress bar.
		ellapsedTime = Time.time - startTime;
	}

	public void playerActivatesBlink ()
	{
		rememberEllapsedTime = ellapsedTime;
		StopCoroutine (blinkCountdownIEnumerator);
		StartCoroutine (singleBlink ());
	}

	IEnumerator singleBlink ()
	{
		blinkScreen.SetActive (true);
		moveAllGhosts ();
		yield return new WaitForSeconds (blinkDuration);
		blinkScreen.SetActive (false);
		StartCoroutine (blinkCountdownIEnumerator);
	}

	IEnumerator blinkCountdown ()
	{
		while (true) {
			//reset the start time
			startTime = Time.time;
			//wait for the blink interval
			yield return new WaitForSeconds (blinkInterval);
						
			//1. Blacken Screen (Will do animation in future)
			blinkScreen.SetActive (true);

			//2. Move Ghosts.
			moveAllGhosts ();

			//3. Wait For Blink Duration;
			yield return new WaitForSeconds (blinkDuration);

			//4. Unblacken Screen
			blinkScreen.SetActive (false);
		}
	}

	public void moveAllGhosts ()
	{
		GameObject[] ghostGOs = GameObject.FindGameObjectsWithTag ("Ghost");
		foreach (GameObject g in ghostGOs) {
			g.GetComponent<Ghost_AI> ().move ();
		}
	}

	public float getEllapsedTime ()
	{
		return ellapsedTime;
	}

	public float getBlinkInterval ()
	{
		return blinkInterval;
	}
	
	public float getBlinkDuration ()
	{
		return blinkDuration;
	}


}