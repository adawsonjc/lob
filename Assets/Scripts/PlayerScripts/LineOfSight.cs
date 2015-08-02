using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour
{
	public Texture2D cursorTexture;
	public Transform player;
	private int layerMask;
	private LineRenderer lineRenderer;
	private bool lineOfSightActive = true;
	private RaycastHit2D lineOfSightTarget;
	private bool hitSomething = false;
								
	void Start ()
	{
		Cursor.SetCursor (cursorTexture, new Vector2 (16, 16), CursorMode.Auto);

		//Set the ray to ignore the anything in the "Player" layer.
		layerMask = 1 << LayerMask.NameToLayer ("Player");
		layerMask = ~layerMask;

		lineRenderer = GetComponent<LineRenderer> ();
		lineRenderer.SetWidth (.05f, .05f);
	}

	void Update ()
	{

		//For turning line of sight off and on
		if (Input.GetButtonDown ("Fire2")) {
			lineRenderer.SetPosition (0, Vector3.zero);
			lineRenderer.SetPosition (1, Vector3.zero);
			lineOfSightActive = !lineOfSightActive;
		}

		Vector2 direction = transform.position - player.position;

		if (lineOfSightActive) {
			RaycastHit2D hit = Physics2D.Raycast (player.position, direction, 5f, layerMask);
			if (hit) {
				hitSomething = true;
				lineOfSightTarget = hit;
				lineRenderer.SetPosition (0, new Vector3 (player.position.x, player.position.y, -1));
				lineRenderer.SetPosition (1, new Vector3 (hit.point.x, hit.point.y, -1));
			} else {
				hitSomething = false;
				lineRenderer.SetPosition (0, new Vector3 (player.position.x, player.position.y, -1));
				lineRenderer.SetPosition (1, transform.position);
			}

		}
	}

	public RaycastHit2D getLineOfSightTarget ()
	{
		return lineOfSightTarget;
	}

	public bool getLineOfSightActive ()
	{
		return lineOfSightActive;
	}
		
	public bool hasHitSomething ()
	{
		return hitSomething;
	}

}

