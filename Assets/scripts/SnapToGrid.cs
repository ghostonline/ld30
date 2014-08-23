using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class SnapToGrid : MonoBehaviour {
	const int DEFAULT_GRID = 1;

	public int gridX = DEFAULT_GRID;
	public int gridY = DEFAULT_GRID;

	void Start () {
		if (!Application.isEditor)
		{
			enabled = false;
		} 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gridX <= 0) { gridX = DEFAULT_GRID; }
		if (gridY <= 0) { gridY = DEFAULT_GRID; }

        var oldPosition = transform.position;
		var position = transform.position;
		position.x = Mathf.RoundToInt(position.x / gridX) * gridX;
		position.y = Mathf.RoundToInt(position.y / gridY) * gridY;
        var distance = Vector3.Distance (oldPosition, position);
        if (!Mathf.Approximately (distance, 0f))
        {
            transform.position = position;
        }
	}
}
