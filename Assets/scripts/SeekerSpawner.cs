using UnityEngine;
using System.Collections;

public class SeekerSpawner : MonoBehaviour {

    public Pool seekerPool;
    public Transform spawnOriginLeft;
    public Transform spawnTargetLeft;
    public Transform spawnOriginRight;
    public Transform spawnTargetRight;

    public bool test;

	// Update is called once per frame
	void Update () {
	    if (test)
        {
            test = false;
            Spawn(transform.position);
        }
	}

    public void Spawn(Vector3 height)
    {
        var origin = spawnOriginLeft.position;
        var target = spawnTargetLeft.position;
        if (Random.value > 0.5f)
        {
            origin = spawnOriginRight.position;
            target = spawnTargetRight.position;
        }

        var seeker = seekerPool.NextItem();
        if (seeker.activeSelf)
        {
            Debug.LogWarning("Too many seekers on screen, cannot spawn more");
            return;
        }

        height.x = origin.x; height.z = origin.z;
        seeker.transform.position = height;
        seeker.SetActive(true);
        var targetPos = height;
        targetPos.x = target.x; targetPos.z = target.z;
        seeker.GetComponent<SeekerAI>().SetTarget(targetPos);
    }
}
