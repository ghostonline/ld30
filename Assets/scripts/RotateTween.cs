using UnityEngine;
using System.Collections;

public class RotateTween : MonoBehaviour {

    public Vector3 rotation;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(rotation * Time.deltaTime);
	}
}
