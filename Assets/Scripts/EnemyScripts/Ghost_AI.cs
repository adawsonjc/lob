using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

public class Ghost_AI : MonoBehaviour
{
	
	public GameObject target;
	public int moveDistance;

	private Seeker seeker;

	public void Start ()
	{
		//Get a reference to the Seeker component we added earlier
		seeker = GetComponent<Seeker> ();
	}

	public void move ()
	{
		//Start a new path to the targetPosition, return the result to the OnPathComplete function
		seeker.StartPath (transform.position, target.transform.position, OnPathComplete);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			target.GetComponent<PlayerControllerXboxRemote> ().dies ();
			this.gameObject.SetActive (false);
			//Destroy(this.gameObject);
		}
	}

	//if Ghost collides with door, move the ghost to the centre of the door. Not too difficult to implement

	public void OnPathComplete (Path p)
	{
		float availableDistance = moveDistance;
		Vector3[] nodes = p.vectorPath.ToArray ();
		int numberOfNodes = nodes.Length;
		//going on the assumption that the first node is the float position of the ghost, start at the second node.
		int nodePos = 1;

		//while we still have distance to move, and while there are still steps left in the path...
		while (availableDistance > 0 && nodePos < numberOfNodes) {
			float xDifference = Mathf.Abs ((float)transform.position.x - nodes [nodePos].x);
			float yDifference = Mathf.Abs ((float)transform.position.y - nodes [nodePos].y);
			float distanceBetween = Mathf.Sqrt (Mathf.Pow (xDifference, 2) + Mathf.Pow (yDifference, 2));
			availableDistance -= distanceBetween;

			//move the ghost to the next node
			transform.position = nodes [nodePos];

			nodePos++;

		}

		//Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
	}


}