using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

    ParticleSystem thisParticleSystem;
    // Use this for initialization
    void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the particle is running keep playing
        if (thisParticleSystem.isPlaying)
            return;
        //after done playing it get destroyed
        Destroy(gameObject);
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
