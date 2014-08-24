using UnityEngine;
using System.Collections;

public class BombAI : MonoBehaviour {

    public Shivering shiver;
    public Damagable health;
    public float shiverTime;
    public ParticleSystem explosionSystem;

    float shiverTimeout;

	// Update is called once per frame
	void Update () {
        shiverTimeout -= Time.deltaTime;
        if (shiverTimeout < 0)
        {
            shiver.enabled = false;
            if (health.currentHealth <= 0)
            {
                explosionSystem.transform.position = transform.position;
                explosionSystem.Play();
                gameObject.SetActive(false);
            }
        }

        if (health.WasHitInLastFrame() && health.currentHealth >= 0)
        {
            shiver.enabled = true;
            shiverTimeout = shiverTime;
        }
	}
}
