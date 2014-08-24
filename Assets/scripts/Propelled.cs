using UnityEngine;
using System.Collections;

public class Propelled : MonoBehaviour {

    public float speed = 1f;
    public Transform direction;

	// Update is called once per frame
	void FixedUpdate () {
        var angle = direction.position - transform.position;
        rigidbody2D.velocity = angle.normalized * speed;
	}
}
