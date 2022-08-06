using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    public GameObject destroyParticles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(destroyParticles, transform.position,
            transform.rotation);
        collision.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.brick);
        Destroy(gameObject);
    }
}
