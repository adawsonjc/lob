using UnityEngine;
using System.Collections;
using Pathfinding;

public class Crawler_AI : MonoBehaviour
{
	public Transform target;
	public float speed;
	public float nextWaypointDistance;

	private int id;
	private int currentWaypoint;
	private LineOfSight lineOfSight;
	private bool isInSight;
	private Seeker seeker;
	private Path path;

	public static int count = 0;

	public void Start ()
	{
		id = count;
		count++;
		seeker = GetComponent<Seeker> ();
		lineOfSight = target.FindChild ("LOS").GetComponent<LineOfSight> ();
		StartCoroutine (seekPath ());
	}

	IEnumerator seekPath ()
	{
		while (true) {
			seeker.StartPath (transform.position, target.position, OnPathComplete);
			yield return new WaitForSeconds (0.3f);
		}	
	}

	public void OnPathComplete (Path p)
	{
		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}
	
	public void FixedUpdate ()
	{
		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			Debug.Log ("End Of Path Reached");
			rigidbody2D.velocity = Vector2.zero;
			return;
		}
		
		//Direction to the next waypoint
		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;
		rigidbody2D.velocity = dir;

		float targetAngle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis (targetAngle, Vector3.forward);
		
		//Check if we are close enough to the next waypoint. If we are, proceed to follow the next waypoint
		if (Vector2.Distance (transform.position, path.vectorPath [currentWaypoint]) < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			target.GetComponent<PlayerControllerKeyboard> ().dies ();
		}
	}

	public int getId ()
	{
		return id;
	}
}