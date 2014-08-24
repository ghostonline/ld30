using UnityEngine;
using System.Collections;

public class GrinderAI : MonoBehaviour {

    public Shivering shiver;
    public Transform retractedAnchor;
    public Transform playerTriggerHeight;
    public Transform player;
    public float activateDuration;
    public float raiseSpeed;

    bool trigger;
    Vector3 activePosition;
    Vector3 retractedPosition;
    float activateTimer;
    bool raising;
	
    // Use this for initialization
	void Start () {
        activePosition = transform.position;
        retractedPosition = retractedAnchor.position;
        transform.position = retractedPosition;
        activateTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (!trigger && playerTriggerHeight.position.y < player.position.y)
        {
            trigger = true;
        }
        else if (trigger && activateTimer < activateDuration)
        {
            activateTimer += Time.deltaTime;
            var progressValue = Mathf.Clamp01(activateTimer / activateDuration);
            transform.position = Vector3.Lerp(retractedPosition, activePosition, progressValue);
            shiver.enabled = activateTimer < activateDuration;
            raising = !(activateTimer < activateDuration);
        }

        if (raising)
        {
            rigidbody2D.velocity = Vector2.up * raiseSpeed;
        }
	}
}
