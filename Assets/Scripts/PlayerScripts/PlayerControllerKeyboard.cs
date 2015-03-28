using UnityEngine;
using System.Collections;

public class PlayerControllerKeyboard : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;
	
	void Update ()
	{
		movement ();
		rotation ();
	}

	public void dies ()
	{
		Debug.Log ("Player Dies");
	}

	void rotation ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3 (0, 0, -turnSpeed));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3 (0, 0, turnSpeed));
		}
	}
	
	void movement ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * moveSpeed;

	}

	//add mouse controls

	//OnMouseMove
	//turn the player in that direction




}
