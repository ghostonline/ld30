using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        gameObject.SetActive(false);
    }
}
