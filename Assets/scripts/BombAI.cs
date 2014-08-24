using UnityEngine;
using System.Collections;

public class BombAI : MonoBehaviour {

    public Shivering shiver;
    public Damagable health;
    public float shiverTime;

    float shiverTimeout;

	// Update is called once per frame
	void Update () {
        shiverTimeout -= Time.deltaTime;
        if (shiverTimeout < 0)
        {
            shiver.enabled = false;
        }

        if (health.WasHitInLastFrame())
        {
            shiver.enabled = true;
            shiverTimeout = shiverTime;
        }
	}
}
