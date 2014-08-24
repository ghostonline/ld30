using UnityEngine;
using System.Collections;

public class Damagable : MonoBehaviour {

    public Transform healthBarAnchor;
    public HealthBar barTemplate;
    public float barDecreaseSpeed;
    public int maxHealth;
    public int currentHealth;

    HealthBar healthBar;
    bool updateBar;

	// Use this for initialization
	void Start () {
        if (barTemplate == null || healthBarAnchor == null)
        {
            Debug.LogWarning("Cannot create a health bar for damagable gameobject");
            return;
        }

        healthBar = (HealthBar)GameObject.Instantiate(barTemplate);
        healthBar.transform.parent = healthBarAnchor;
        healthBar.transform.localPosition = Vector3.zero;
        updateBar = true;

        if (barDecreaseSpeed <= 0) { barDecreaseSpeed = 0.1f; }
	}
	
	// Update is called once per frame
	void Update () {
        if (!updateBar) { return; }

        var desiredValue = Mathf.Clamp01((float)currentHealth / (float)maxHealth);
        var diff = healthBar.value - desiredValue;
        if (!Mathf.Approximately(diff, 0f))
        {
            if (Mathf.Sign(diff) > 0)
            {
                healthBar.value -= Mathf.Min(barDecreaseSpeed * Time.deltaTime, diff);
            }
            else
            {
                healthBar.value = desiredValue;
            }
        }
        else
        {
            updateBar = false;
        }
	}

    public void ApplyDamage(int points)
    {
        currentHealth -= points;
        updateBar = true;
    }
}
