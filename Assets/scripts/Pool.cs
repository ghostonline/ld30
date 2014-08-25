using UnityEngine;
using System.Collections;

public class Pool : MonoBehaviour {

    public GameObject template;
    public int poolSize = 5;

    GameObject[] pool;
    int poolIdx;

	// Use this for initialization
	void Start () {
        if (poolSize <= 0) { poolSize = 10; }
        pool = new GameObject[poolSize];
        var poolParentTransform = template.transform.parent;
        var digits = Mathf.CeilToInt(Mathf.Log10(poolSize));
        template.SetActive(false);
        string poolItemName = string.Format("{0}{{0:D0{1}}}", template.name, digits);
        for (int ii = 0; ii < poolSize; ++ii)
        {
            var child = (GameObject)GameObject.Instantiate(template);
            child.name = string.Format(poolItemName, ii);
            child.transform.parent = poolParentTransform;
            pool[ii] = child;
        }

	}
	
    public GameObject NextItem()
    {
        // Start running from the next in line
        poolIdx = (poolIdx + 1) % poolSize;
        for (int ii = 0; ii < poolSize; ++ii)
        {
            int idx = (poolIdx + ii) % poolSize;
            if (!pool[idx].activeSelf)
            {
                poolIdx = idx;
                return pool[idx];
            }
        }
        
        // Just return the oldest one
        return pool[poolIdx];
    }

}
