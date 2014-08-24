using UnityEngine;
using System.Collections;

public class MultiEmitter : MonoBehaviour {

    public ParticleSystem[] systems;

    public void Play()
    {
        foreach (var system in systems)
        {
            system.Play();
        }
    }
}
