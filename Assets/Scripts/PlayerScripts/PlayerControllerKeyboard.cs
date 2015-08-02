using UnityEngine;
using System.Collections;

public class PlayerControllerKeyboard : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;
	private Animator animator;
	public BlinkController blinkController;

	void Start ()
	{
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{
		checkIfBlinks ();
		movement ();
		rotation ();
		if (Mathf.Abs (rigidbody2D.velocity.x) > 0.2 || Mathf.Abs (rigidbody2D.velocity.y) > 0.2) {
			animator.SetFloat ("Speed", 1f);
		} else {
			animator.SetFloat ("Speed", 0f);
		}
	}

	public void dies ()
	{
		Debug.Log ("Player Dies");
	}

	void checkIfBlinks ()
	{
		if (Input.GetKeyDown ("space")) {
			blinkController.playerActivatesBlink ();
		}
	}

	void rotation ()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 directionToFace = (mousePos - transform.position).normalized;
		float targetAngle = Mathf.Atan2 (directionToFace.y, directionToFace.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (targetAngle, Vector3.forward);
	}
	
	void movement ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * moveSpeed;
	}
	
}
