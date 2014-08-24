using UnityEngine;
using System.Collections;

public class Shivering : MonoBehaviour {

    public float magnitude = 1f;
    public float speed = 1f;

    Vector3 basePosition;
    Vector3 targetPosition;

	void Start () {
        basePosition = transform.localPosition;
        targetPosition = selectRandomDeviationTarget();
        enabled = false;
	}

    void OnDisable()
    {
        transform.localPosition = basePosition;
    }

    Vector3 selectRandomDeviationTarget()
    {
        var direction = new Vector3(Random.value - 0.5f,Random.value - 0.5f,Random.value - 0.5f);
        direction.Normalize();
        return basePosition + direction * magnitude;
    }
	
	void Update () {
        var curPosition = transform.localPosition;
        var distance = Vector3.Distance(targetPosition, curPosition);
        var travelDistance = Mathf.Min(speed * Time.deltaTime, magnitude * 2);
        if (distance < travelDistance)
        {
            curPosition = targetPosition;
            travelDistance -= distance;
            targetPosition = selectRandomDeviationTarget();
        }

        var direction = targetPosition - curPosition;
        direction.Normalize();
        transform.localPosition = curPosition + direction * travelDistance;
    }
}
