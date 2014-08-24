using UnityEngine;
using System.Collections;

public class BulletGun : MonoBehaviour {

    public Transform spawnPoint;
    public Transform fireDirection;
    public float fireRate;

    public GameObject bulletTemplate;
    public Transform poolParent;
    public int poolSize = 10;

    float fireCooldown;
    GameObject[] bullets;
    int poolIdx;

	void Start () {
        if (poolSize <= 0) { poolSize = 10; }
        bullets = new GameObject[poolSize];
        var poolParentTransform = poolParent.transform;
        var digits = Mathf.CeilToInt(Mathf.Log10(poolSize));
        bulletTemplate.SetActive(false);
        string bulletTemplateName = string.Format("{0}{{0:D0{1}}}", bulletTemplate.name, digits);
        for (int ii = 0; ii < poolSize; ++ii)
        {
            var child = (GameObject)GameObject.Instantiate(bulletTemplate);
            child.name = string.Format(bulletTemplateName, ii);
            child.transform.parent = poolParentTransform;
            bullets[ii] = child;
        }
	}
	
	// Update is called once per frame
	void Update () {
        fireCooldown -= Time.deltaTime;
	    if (Input.GetAxis("Fire1") > 0 && fireCooldown < 0)
        {
            var bullet = FindFreeBullet();
            bullet.transform.position = spawnPoint.position;
            bullet.transform.rotation = CalculateAimAngle();
            bullet.SetActive(true);
            fireCooldown = fireRate;
        }
	}

    GameObject FindFreeBullet()
    {
        // Start running from the next in line
        poolIdx = (poolIdx + 1) % poolSize;
        for (int ii = 0; ii < poolSize; ++ii)
        {
            int idx = (poolIdx + ii) % poolSize;
            if (!bullets[idx].activeSelf)
            {
                poolIdx = idx;
                return bullets[idx];
            }
        }

        // Just return the oldest one
        return bullets[poolIdx];
    }

    Quaternion CalculateAimAngle()
    {
        var lookDir = Quaternion.LookRotation(fireDirection.position - spawnPoint.position);
        var realign = Quaternion.Euler(0, 270, 0);
        return lookDir * realign;
    }
}
