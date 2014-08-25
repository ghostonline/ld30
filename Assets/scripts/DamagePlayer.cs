using UnityEngine;
using System.Collections;

public class DamagePlayer : MonoBehaviour {

    const string PlayerTag = "Player";
    public int damage = 1;

    void DoDamageIfPlayer(GameObject obj)
    {
        if (obj.tag == PlayerTag)
        {
            var damagable = obj.GetComponent<Damagable>();
            if (damagable)
            {
                damagable.ApplyDamage(damage);
            }
        }
    }

	// Use this for initialization
    void OnCollisionEnter2D (Collision2D coll)
    {
        DoDamageIfPlayer(coll.gameObject);
	}

    void OnTriggerEnter2D (Collider2D coll)
    {
        DoDamageIfPlayer(coll.gameObject);
    }
}
