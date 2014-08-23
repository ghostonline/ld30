using UnityEngine;
using System.Collections;

public class CollideTrigger : MonoBehaviour {

    int touches;
    public bool hasCollision
    {
        get { return touches > 0; }
    }
	
    void OnTriggerEnter2D(Collider2D coll)
    {
        ++touches;
	}

    void OnTriggerExit2D(Collider2D coll)
    {
        --touches;
        DebugUtil.Assert(touches >= 0);
    }
}
