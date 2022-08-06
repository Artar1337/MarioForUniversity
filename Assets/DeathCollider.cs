using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private GameObject Mario;
    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.Find("Mario").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Mario.GetComponent<Animator>().SetBool("Dead", true);
        }
    }

}
