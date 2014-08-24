using UnityEngine;
using System.Collections;

public class BulletAI : MonoBehaviour {

    public int damage = 1;

    void OnTriggerEnter2D(Collider2D coll)
    {
        var damagable = coll.GetComponent<Damagable>();
        if (damagable != null)
        {
            damagable.ApplyDamage(damage);
        }
        gameObject.SetActive(false);
    }
}
