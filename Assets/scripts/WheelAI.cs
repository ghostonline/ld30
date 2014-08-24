using UnityEngine;
using System.Collections;

public class WheelAI : MonoBehaviour {

    public CollideTrigger leftTrigger;
    public CollideTrigger rightTrigger;
    public WheelJoint2D motor;
    public float speed;

	// Update is called once per frame
	void Update () {
        if (leftTrigger.hasCollision || rightTrigger.hasCollision)
        {
            var target = 0f;
            if (leftTrigger.hasCollision) { target -= speed; }
            if (rightTrigger.hasCollision) { target += speed; }
            var engine = motor.motor;
            engine.motorSpeed = target;
            motor.motor = engine;
            motor.useMotor = !Mathf.Approximately(target, 0f);
        }
    }
}
