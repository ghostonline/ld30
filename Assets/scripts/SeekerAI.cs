using UnityEngine;
using System.Collections;

public class SeekerAI : MonoBehaviour {

    const string PlayerTag = "Player";

    public RotateTween rotationAnimator;
    public Rigidbody2D mover;

    public float speed;
    public float telegraphRotationScale;
    public AnimationCurve lockProgress;
    public float lockDuration;
    public float breakFactor = 0.9f;

    Vector3 idleRotation;
    float lockTimer;
    bool moving;
    Vector3 targetPosition;

	// Use this for initialization
	void Start () {
        idleRotation = rotationAnimator.rotation;
        lockTimer = lockDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (!moving)
        {
            lockTimer -= Time.deltaTime;
            if (lockTimer < 0)
            {
                lockTimer += lockDuration;
                var player = GameObject.FindGameObjectWithTag(PlayerTag);
                targetPosition = player.transform.position;
                var direction = targetPosition - transform.position;
                mover.velocity = direction.normalized * speed;
                moving = true;
            }
            
            var lockProgressValue = lockProgress.Evaluate(1 - lockTimer / lockDuration);
            var scale = (telegraphRotationScale - 1) * lockProgressValue + 1;
            rotationAnimator.rotation = scale * idleRotation;
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            var direction = targetPosition - transform.position;
            if (Vector3.Dot(direction, mover.velocity) < 0)
            {
                // Passed the target
                mover.velocity = mover.velocity * breakFactor;
                if (mover.velocity.magnitude < 0.01f)
                {
                    mover.velocity = Vector2.zero;
                    moving = false;
                }
            }
        }
    }
}
