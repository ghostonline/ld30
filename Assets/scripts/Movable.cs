using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

	public float jumpVelocity = 1f;
	public float horizontalVelocity = 1f;
    public int maxJumpFrames = 10;

	public float horizontalAxisDeadzone = 0.1f;
	public float verticalAxisDeadzone = 0.1f;

    public GameObject[] jumpLocks;

    int jumpTimeout;

	void FixedUpdate () {
        var velocity = rigidbody2D.velocity;

		var horizontal = Input.GetAxis ("Horizontal");
		if (Mathf.Abs (horizontal) > horizontalAxisDeadzone)
        {
            velocity.x = horizontalVelocity * horizontal;
        }
        else
        {
            velocity.x = 0;
        }

        if (IsOnGround ())
        {
            jumpTimeout = maxJumpFrames;
        }

        var vertical = Input.GetAxis ("Vertical");
        if (vertical > verticalAxisDeadzone && jumpTimeout > 0f)
        {
            velocity.y = jumpVelocity;
            --jumpTimeout;
        }
        else
        {
            jumpTimeout = 0;
        }

        rigidbody2D.velocity = velocity;
	}

    bool IsOnGround()
    {
        var pos2D = Vector2.zero;
        foreach (var jumpLock in jumpLocks)
        {
            var pos = jumpLock.transform.position;
            pos2D.x = pos.x;
            pos2D.y = pos.y;
            var result = Physics2D.Raycast(pos2D, Vector2.zero);
            if (result.collider != null)
            {
                return true;
            }
        }

        return false;
    }
}
