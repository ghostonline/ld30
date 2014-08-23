using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

	public float jumpVelocity = 1f;
	public float horizontalVelocity = 1f;
    public float maxWallSlideVelocity = -1f;
    public int maxJumpFrames = 10;
    public int lastMinuteJumpTimeout = 2;

	public float axisDeadzone = 0.01f;

    public CollideTrigger leftWallJumpLock;
    public CollideTrigger rightWallJumpLock;

    public GameObject[] jumpLocks;

    int jumpTimeout;
    int lastMinuteJump;
    bool wallHug;

    void Awake()
    {
        if (maxWallSlideVelocity > 0)
        {
            Debug.LogWarning("Wall slide velocity is not angled downwards");
        }
    }

	void FixedUpdate () {
        var velocity = rigidbody2D.velocity;

        // Horizontal movement simply based on axis
		var horizontal = Input.GetAxis ("Horizontal");
		if (!IsInDeadZone(horizontal))
        {
            velocity.x = horizontalVelocity * horizontal;
            wallHug = (leftWallJumpLock.hasCollision && horizontal < 0) || (rightWallJumpLock.hasCollision && horizontal > 0);
        }
        else
        {
            velocity.x = 0;
        }

        if (wallHug && (leftWallJumpLock.hasCollision || rightWallJumpLock.hasCollision))
        {
            velocity.y = Mathf.Max(velocity.y, maxWallSlideVelocity);
        }
        else
        {
            wallHug = false;
        }

        // Vertical movement
        var vertical = Input.GetAxis ("Jump");
        var onGround = IsOnGround ();
        if (onGround)
        {
            lastMinuteJump = lastMinuteJumpTimeout;
        }
        else
        {
            --lastMinuteJump;
        }

        if (IsInDeadZone(vertical))
        {
            if (onGround || lastMinuteJump > 0)
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
