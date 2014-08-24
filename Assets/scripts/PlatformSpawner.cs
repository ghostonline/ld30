using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour {

    public GameObject platformTemplate;
    public int poolSize = 50;
    public float scrollSpeed = 5f;
    public float minPlatformInterval = 1f;
    public float maxPlatformInterval = 5f;
    public float minPlatformWidth = 100f;
    public float maxPlatformWidth = 1500f;

    GameObject[] platforms;
    float spawnTimer;
    int poolIdx;
    BoxCollider2D spawnSize;

	// Use this for initialization
	void Start () {
        scrollSpeed = Mathf.Abs(scrollSpeed);
        if (poolSize <= 0) { poolSize = 10; }
        platforms = new GameObject[poolSize];
        var poolParentTransform = transform;
        var digits = Mathf.CeilToInt(Mathf.Log10(poolSize));
        platformTemplate.SetActive(false);
        string poolItemName = string.Format("{0}{{0:D0{1}}}", platformTemplate.name, digits);
        for (int ii = 0; ii < poolSize; ++ii)
        {
            var child = (GameObject)GameObject.Instantiate(platformTemplate);
            child.name = string.Format(poolItemName, ii);
            child.transform.parent = poolParentTransform;
            platforms[ii] = child;
        }

        spawnSize = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer += Random.Range(minPlatformInterval, maxPlatformInterval);

            var platform = FindFreePlatform();
            var width = Random.Range(minPlatformWidth, maxPlatformWidth);
            platform.SetActive(true);
            platform.rigidbody2D.velocity = new Vector2(0, -scrollSpeed);
            var position = transform.TransformPoint(spawnSize.size * Random.value + spawnSize.center - spawnSize.size * 0.5f);
            platform.transform.position = position;
            platform.transform.localScale = new Vector3(width, minPlatformWidth, 0f);
        }
	}

    GameObject FindFreePlatform()
    {
        // Start running from the next in line
        poolIdx = (poolIdx + 1) % poolSize;
        for (int ii = 0; ii < poolSize; ++ii)
        {
            int idx = (poolIdx + ii) % poolSize;
            if (!platforms[idx].activeSelf)
            {
                poolIdx = idx;
                return platforms[idx];
            }
        }

        // Just return the oldest one
        return platforms[poolIdx];
    }
        

}
