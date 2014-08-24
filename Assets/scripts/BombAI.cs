using UnityEngine;
using System.Collections;

public class BombAI : MonoBehaviour {

    public Damagable health;
    public MultiEmitter explosionSystem;

    float shiverTimeout;

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
