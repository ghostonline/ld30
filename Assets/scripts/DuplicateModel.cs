using UnityEngine;
using System.Collections;

public class DuplicateModel : MonoBehaviour {

    public GameObject model;
    public Vector3 interval;
    public int count;

	// Use this for initialization
	void Start () {
        for (int ii = 0; ii < count; ++ii)
        {
            var item = (GameObject)GameObject.Instantiate(model);
            item.transform.parent = model.transform.parent;
            item.SetActive(true);
            var pos = model.transform.localPosition;
            pos += interval * ii;
            item.transform.localPosition = pos;
        }
        model.SetActive(false);
        enabled = false;
    }
}
