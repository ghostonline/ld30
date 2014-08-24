using UnityEngine;
using System.Collections;

public class FollowObjectHeight : MonoBehaviour {

    public Transform obj;

    float baseOffsetY;
    float minY;

	// Use this for initialization
	void Start () {
        baseOffsetY = obj.position.y - transform.position.y;
        minY = obj.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        var objPos = obj.transform.position;
        if (objPos.y > minY)
        {
            var pos = transform.position;
            var curOffsetY = objPos.y - pos.y;
            var diff = curOffsetY - baseOffsetY;
            pos.y += diff;
            transform.position = pos;
        }
	}
}
