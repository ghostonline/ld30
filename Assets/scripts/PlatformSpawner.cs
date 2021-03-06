﻿using UnityEngine;
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
    public Transform groundFloor;
    public float groundFloorWidth;

    GameObject[] platforms;
    bool[] horizontal;
    float nextSpawnHeight;
    int poolIdx;
    int columnIdx;

	// Use this for initialization
	void Start () {
        scrollSpeed = Mathf.Abs(scrollSpeed);
        if (poolSize <= 0) { poolSize = 10; }
        platforms = new GameObject[poolSize];
        horizontal = new bool[poolSize];
        var poolParentTransform = platformTemplate.transform.parent;
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

        SpawnPlatform(groundFloor.position, groundFloorWidth, false);
        nextSpawnHeight = GetNextSpawnHeight(groundFloor.position.y);
	}

    void SpawnPlatform(Vector3 pos, float width, bool flip)
    {
        var platformIdx = FindFreePlatform();
        var platform = platforms[platformIdx];
        platform.SetActive(true);
        platform.transform.position = pos;
        var scale = Vector3.zero;
        horizontal[platformIdx] = !flip;
        if (!flip) { scale = new Vector3(width, platformHeight, 0f);}
        else { scale = new Vector3(platformHeight, width, 0f); } 
        platform.transform.localScale = scale;
    }

    public GameObject GetFirstPlatformHigherThanPosition(Vector3 pos)
    {
        var nearestIdx = -1;
        var nearestDistance = float.PositiveInfinity;
        for (int ii = 0; ii < poolSize; ++ii)
        {
            var platform = platforms[ii];
            if (!platform.activeSelf || !horizontal[ii]) { continue; }
            var distance = platform.transform.position.y - pos.y;
            if (distance > 0 && distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestIdx = ii;
            }
        }

        if (nearestIdx > -1)
        {
            return platforms[nearestIdx];
        }

        return null;
    }

    float GetNextSpawnHeight(float lastHeight)
    {
        return lastHeight + Random.Range(minPlatformInterval, maxPlatformInterval);
    }
	
	// Update is called once per frame
	void Update () {
        var spawnerHeight = transform.position.y;
        while (spawnerHeight > nextSpawnHeight)
        {
            var width = Random.Range(minPlatformWidth, maxPlatformWidth);
            columnIdx = GetRandomAdjacentColumn(columnIdx);
            var pos = spawnColumns[columnIdx].position;
            pos.y = nextSpawnHeight;
            var flip = ShouldFlip();
            SpawnPlatform(pos, width, flip);
            nextSpawnHeight = GetNextSpawnHeight(nextSpawnHeight);
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

    int FindFreePlatform()
    {
        // Start running from the next in line
        poolIdx = (poolIdx + 1) % poolSize;
        for (int ii = 0; ii < poolSize; ++ii)
        {
            int idx = (poolIdx + ii) % poolSize;
            if (!platforms[idx].activeSelf)
            {
                poolIdx = idx;
                break;
            }
        }

        // Just return the oldest one
        return poolIdx;
    }
        

}
