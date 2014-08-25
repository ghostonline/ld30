using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {

    public Pool bombPool;
    public PlatformSpawner platforms;
    public Transform spawnLevel;
    public Vector3 bombOffset;

    public bool test;

	// Update is called once per frame
	void Update () {
	    if (test)
        {
            test = false;
            Spawn();
        }
	}

    public void Spawn()
    {
        var pos = spawnLevel.position;
        var platform = platforms.GetFirstPlatformHigherThanPosition(pos);
        var bombPos = platform.transform.position + bombOffset;

        var bomb = bombPool.NextItem();
        bomb.transform.position = bombPos;
        bomb.SetActive(true);
    }
}
