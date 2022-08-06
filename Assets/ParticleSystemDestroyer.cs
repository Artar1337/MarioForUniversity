using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemDestroyer : MonoBehaviour
{
    private ParticleSystem system;
    // Start is called before the first frame update
    void Start()
    {
        system = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!system.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
