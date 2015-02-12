using UnityEngine;
using System.Collections;

public class PlayerControllerKeyboard : MonoBehaviour {

	public float speed;
	
	void Update () {
		movement ();
		rotation ();
	}

	public void dies(){
		Debug.Log ("Player Dies");
	}

	void rotation ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (new Vector3 (0, 0, -2));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (new Vector3 (0, 0, 2));
		}
	}
	
	void movement ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * speed;

	}

}
