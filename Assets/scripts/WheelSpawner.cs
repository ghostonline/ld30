using UnityEngine;
using System.Collections;

public class WheelSpawner : MonoBehaviour {

    public Pool pool;
    public PlatformSpawner platforms;
    public Transform spawnLevel;
    public Vector3 offset;

    public bool test;

	void Update () {
	    if (test)
        {
            test = false;
            Spawn();
        }
	}
	
	public void Spawn () {
        var pos = spawnLevel.position;
        var platform = platforms.GetFirstPlatformHigherThanPosition(pos);
        if (platform == null) { Debug.LogWarning("Could not find platform"); return; }
        var spawnPos = platform.transform.position + offset;
        
        var wheel = pool.NextItem();
        wheel.transform.position = spawnPos;
        wheel.SetActive(true);
        var motor = wheel.GetComponent<WheelJoint2D>().motor;
        if (spawnPos.x < pos.x)
        {
            motor.motorSpeed *= -1;
        }
	}
}
