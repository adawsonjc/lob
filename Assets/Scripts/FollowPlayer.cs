using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

	public Transform target;
	
	void Update ()
	{
		transform.position = new Vector3 (target.position.x, target.position.y, -10);
	}
}
