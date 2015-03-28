using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public class Crawler_AI : MonoBehaviour
{
	public Transform target;
	private Seeker seeker;
	private Vector3[] path;
	private LineOfSight lineOfSight;
	private bool isInSight;
	private float t = 0;
	
	public static int count = 0;
	private int id;

	public void Start ()
	{
		id = count;
		count++;

		path = new Vector3[0];
		seeker = GetComponent<Seeker> ();
		lineOfSight = target.FindChild ("LOS").GetComponent<LineOfSight> ();
		StartCoroutine (seekPath ());
	}
	
	public void Update ()
	{
		if (lineOfSight.hasHitSomething ()) {
			RaycastHit2D hit = lineOfSight.getLineOfSightTarget ();
			if (hit.transform.gameObject.tag == "Creeper" && hit.transform.gameObject.GetComponent<Crawler_AI> ().getId () == id) {
			} else {
				transform.position = Spline.MoveOnPath (path, transform.position, ref t, 4, 100, EasingType.Cubic, false, false);
			}
		} else {
			transform.position = Spline.MoveOnPath (path, transform.position, ref t, 4, 100, EasingType.Cubic, false, false);
		}
	
	}

	IEnumerator seekPath ()
	{
		while (true) {
			seeker.StartPath (transform.position, target.transform.position, OnPathComplete);
			yield return new WaitForSeconds (0.3f);
		}	
	}

	public void OnPathComplete (Path p)
	{
		List<Vector3> vects = p.vectorPath;
		path = vects.ToArray ();
		t = 0;
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "Player") {
			target.GetComponent<PlayerControllerXboxRemote> ().dies ();
		}
	}

	public int getId ()
	{
		return id;
	}
	
}
