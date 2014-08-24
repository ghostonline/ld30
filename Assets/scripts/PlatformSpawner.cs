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
    public float platformHeight = 100f;
    public float flipChance = 0.3f;
    public Transform[] spawnColumns;

    GameObject[] platforms;
    float spawnTimer;
    int poolIdx;
    int columnIdx;

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
            columnIdx = GetRandomAdjacentColumn(columnIdx);
            platform.transform.position = spawnColumns[columnIdx].position;
            if (!ShouldFlip())
            {
                platform.transform.localScale = new Vector3(width, platformHeight, 0f);
            }
            else
            {
                platform.transform.localScale = new Vector3(platformHeight, width, 0f);
            } 
        }
	}

    int GetRandomAdjacentColumn(int currentIdx)
    {
        int startIdx = currentIdx;
        int options = 0;
        if (currentIdx > 0)
        {
            --startIdx;
            ++options;
        }
        if (currentIdx < spawnColumns.Length - 1)
        {
            ++options;
        }

        var targetIdx = Random.Range(startIdx, startIdx + options);
        if (targetIdx == currentIdx)
        {
            ++targetIdx;
        }

        return targetIdx;
    }

    bool ShouldFlip()
    {
        return Random.value < flipChance;
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
