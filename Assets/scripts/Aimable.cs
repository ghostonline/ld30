using UnityEngine;
using System.Collections;

public class Aimable : MonoBehaviour {

	void Update () {
        var worldAim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var myPos = transform.position;
        worldAim.z = myPos.z;
        transform.rotation = Quaternion.LookRotation(worldAim - myPos);
        transform.Rotate(0,270,0);
	}
}
