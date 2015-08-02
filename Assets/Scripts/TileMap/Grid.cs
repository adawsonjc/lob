using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
	
	public float width = 32.0f;
	public float height = 32.0f;
	public Color color = Color.white;
	public Transform tilePrefab;
	public TileSet tileset;

	void OnDrawGizmos ()
	{
		Vector3 pos = Camera.current.transform.position;
		Gizmos.color = this.color;

		//Draws lines on the x axis, increments upwards at the height variable (32)
		for (float y=pos.y - 800.0f; y< pos.y + 800.0f; y+=this.height) {
			//The Mathf.floor is simply to ensure the grids lines are spaced evenly
			Gizmos.DrawLine (new Vector3 (-10000000.0f, Mathf.Floor (y / height) * height, 0),
			                new Vector3 (10000000.0f, Mathf.Floor (y / height) * height, 0));
		}

		//Draws lines on the y axis, increments to the right at the width variable (32)
		for (float x=pos.x - 800.0f; x< pos.x + 800.0f; x+=this.width) {
			//The Mathf.floor is simply to ensure the grids lines are spaced evenly
			Gizmos.DrawLine (new Vector3 (Mathf.Floor (x / width) * width, -10000000.0f, 0),
			new Vector3 (Mathf.Floor (x / width) * width, 10000000.0f, 0));
		}

	}

}
