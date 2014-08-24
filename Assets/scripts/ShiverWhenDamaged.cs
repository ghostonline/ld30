using UnityEngine;
using System.Collections;

public class ShiverWhenDamaged : MonoBehaviour {

    public Shivering shiver;
    public Damagable damage;
    public float shiverTime;

    float shiverTimeout;

    public bool Shivering
    {
        get { return shiver.enabled; }
    }
	
	// Update is called once per frame
	void Update () {
        shiverTimeout -= Time.deltaTime;
        if (shiverTimeout < 0)
        {
            shiver.enabled = false;
        }
        
        if (damage.WasHitInLastFrame())
        {
            shiver.enabled = true;
            shiverTimeout = shiverTime;
        }
	}
}
