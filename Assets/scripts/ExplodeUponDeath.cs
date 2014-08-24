using UnityEngine;
using System.Collections;

public class ExplodeUponDeath : MonoBehaviour {

    public Damagable health;
    public MultiEmitter explosionSystem;

	// Update is called once per frame
	void Update () {
        if (health.WasHitInLastFrame() && health.currentHealth <= 0)
        {
            explosionSystem.transform.position = transform.position;
            explosionSystem.Play();
            gameObject.SetActive(false);
        }
    }
}
