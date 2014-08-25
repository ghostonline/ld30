using UnityEngine;
using System.Collections;

public class Progress : MonoBehaviour {

    public GrinderAI grinder;
    public SeekerSpawner seekers;
    public BombSpawner bombs;
    public WheelSpawner wheels;

    public Transform player;

    public int grinderAccelerateHeight = 200;
    public int grinderSpeed = 6;
    bool grinderTrigger;

    public float seekerSpawnInterval;

    int[] seekerHeights = {
        120,
        200,
        300,
    };
    int seekerIdx;
    float seekerSpawnTimeout;
    int seekerSpawnRate;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var playerPosY = player.transform.position.y;
        if (!grinderTrigger && playerPosY > grinderAccelerateHeight)
        {
            grinderTrigger = true;
            grinder.raiseSpeed = grinderSpeed;
        }

        if (seekerIdx < seekerHeights.Length && playerPosY > seekerHeights[seekerIdx])
        {
            ++seekerIdx;
            ++seekerSpawnRate;
        }

        seekerSpawnTimeout -= Time.deltaTime;
        if (seekerIdx > 0 && seekerSpawnTimeout < 0)
        {
            seekerSpawnTimeout = seekerSpawnInterval / seekerSpawnRate;
            seekers.Spawn(player.transform.position);
        }
	}
}
