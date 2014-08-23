using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour {

	public float jumpVelocity = 1f;
	public float horizontalVelocity = 1f;
    public float horizontalAccelleration = 0.2f;
    public float wallKickVelocity = 1f;
    public int maxJumpFrames = 10;
    public int lastMinuteJumpTimeout = 2;

	public float axisDeadzone = 0.001f;

    public CollideTrigger leftWallJumpLock;
    public CollideTrigger rightWallJumpLock;

    public GameObject[] jumpLocks;

    int jumpTimeout;
    int lastMinuteJump;
    bool wallHug
    {
        get { return wallHugLeft || wallHugRight; }
    }
    bool wallHugLeft;
    bool wallHugRight;
    bool wallJumpPrimed;

	void FixedUpdate () {
        var velocity = rigidbody2D.velocity;
        var onGround = IsOnGround ();

        // Horizontal movement simply based on axis
		var horizontal = Input.GetAxis ("Horizontal");
        var targetVelocity = horizontalVelocity * Mathf.Abs(horizontal);
        var targetVelocityDirection = Mathf.Sign(horizontal);
        if (onGround)
        {
            velocity.x = targetVelocity * targetVelocityDirection;
        }
        else if (velocity.x * targetVelocityDirection < targetVelocity)
        {
            velocity.x += targetVelocity * horizontalAccelleration * targetVelocityDirection;

        }

        wallHugLeft = leftWallJumpLock.hasCollision;
        wallHugRight = rightWallJumpLock.hasCollision;
        if (wallHugLeft && velocity.x < 0)
        {
            velocity.x = 0;
        }
        if (wallHugRight && velocity.x > 0)
        {
            velocity.x = 0;
        }

        // Vertical movement
        var vertical = Input.GetAxis ("Jump");
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
                wallJumpPrimed = false;
            }
            else
            {
                jumpTimeout = 0;
            }

            if (wallHug && !onGround)
            {
                wallJumpPrimed = true;
            }
        }

        if (vertical > axisDeadzone)
        {
            if (wallJumpPrimed && wallHug)
            {
                jumpTimeout = maxJumpFrames;
                if (wallHugLeft) { velocity.x = wallKickVelocity; }
                if (wallHugRight) { velocity.x = -wallKickVelocity; }
                wallJumpPrimed = false;
            }

            if (jumpTimeout > 0)
            {
                velocity.y = jumpVelocity;
                --jumpTimeout;
            }
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
