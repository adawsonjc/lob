using UnityEngine;
using System.Collections;

public class Charger_AI : MonoBehaviour
{
	public Transform target;
	public float speed;
	private LineOfSight lineOfSight;
	
	void Start ()
	{
		lineOfSight = target.FindChild ("LineOfSight").GetComponent<LineOfSight> ();
	}

	void Update ()
	{
		if (lineOfSight.hasHitSomething ()) {
			RaycastHit2D hit = lineOfSight.getLineOfSightTarget ();
			if (hit.transform.gameObject.tag == "Charger") {
				rigidbody2D.velocity = (target.position - transform.position) * speed;
			} else {
				rigidbody2D.velocity = Vector2.zero;
			}
		} else {
			rigidbody2D.velocity = Vector2.zero;
		}
		if (!lineOfSight.getLineOfSightActive ()) {
			rigidbody2D.velocity = Vector2.zero;
		}

	}
}
