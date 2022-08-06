using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinPickupSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().score += 100;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(coinPickupSound);
            Destroy(gameObject);
        }
    }
}
