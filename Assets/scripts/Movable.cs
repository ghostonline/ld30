using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

	public float jumpVelocity = 1f;
	public float horizontalVelocity = 1f;
    public int maxJumpFrames = 10;

	public float axisDeadzone = 0.01f;

    public GameObject[] jumpLocks;

    int jumpTimeout;

	void FixedUpdate () {
        var velocity = rigidbody2D.velocity;

        // Horizontal movement simply based on axis
		var horizontal = Input.GetAxis ("Horizontal");
		if (!IsInDeadZone(horizontal))
        {
            velocity.x = horizontalVelocity * horizontal;
        }
        else
        {
            velocity.x = 0;
        }

        // Vertical movement
        var vertical = Input.GetAxis ("Jump");
        var onGround = IsOnGround ();

        if (IsInDeadZone(vertical))
        {
            if (onGround)
            {
                jumpTimeout = maxJumpFrames;
            }
            else
            {
                jumpTimeout = 0;
            }
        }

        if (vertical > axisDeadzone && jumpTimeout > 0)
        {
            velocity.y = jumpVelocity;
            --jumpTimeout;
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

    bool IsInDeadZone(float value)
    {
        return Mathf.Abs (value) < axisDeadzone;
    }
}
