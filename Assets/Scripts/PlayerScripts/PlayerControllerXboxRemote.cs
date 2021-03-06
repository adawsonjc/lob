﻿using UnityEngine;
using System.Collections;

public class PlayerControllerXboxRemote : MonoBehaviour
{
	public float moveSpeed;
	public float turnSpeed;

	private Vector2 facing;
	private bool rotating = false;

	void Update ()
	{
		movement ();
		rotation ();
	}

	void rotation ()
	{
		float rotateHorizontal = Input.GetAxis ("RightJoystickHorizontal");
		float rotateVertical = Input.GetAxis ("RightJoystickVertical");

		if (Mathf.Abs (rotateHorizontal) > 0.2 || Mathf.Abs (rotateVertical) > 0.2) {
			rotating = true;
			facing = new Vector2 (rotateHorizontal, rotateVertical);
			float targetAngle = Mathf.Atan2 (facing.y, facing.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (targetAngle, Vector3.forward);
		} else {
			rotating = false;
		}
	}

	void movement ()
	{
		float moveHorizontal = Input.GetAxis ("LeftJoystickHorizontal");
		float moveVertical = Input.GetAxis ("LeftJoystickVertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		rigidbody2D.velocity = movement * moveSpeed;
	}
	
	public void dies ()
	{
		Debug.Log ("Player Dies");
	}
	
}
