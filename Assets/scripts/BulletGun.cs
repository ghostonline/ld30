using UnityEngine;
using System.Collections;

public class BulletGun : MonoBehaviour {

    public Transform spawnPoint;
    public Transform fireDirection;

    public GameObject bulletTemplate;
    public Transform poolParent;
    public int poolSize = 10;

	// Use this for initialization
	void Start () {
        if (poolSize <= 0) { poolSize = 10; }
        var poolParentTransform = poolParent.transform;
        var digits = Mathf.CeilToInt(Mathf.Log10(poolSize));
        bulletTemplate.SetActive(false);
        string bulletTemplateName = string.Format("{0}{{0:D0{1}}}", bulletTemplate.name, digits);
        for (int ii = 0; ii < poolSize; ++ii)
        {
            var child = (GameObject)GameObject.Instantiate(bulletTemplate);
            child.name = string.Format(bulletTemplateName, ii);
            child.transform.parent = poolParentTransform;
            //child.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
