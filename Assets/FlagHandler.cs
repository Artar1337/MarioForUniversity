using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagHandler : MonoBehaviour
{
    private BoxCollider2D box;
    private bool entered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (entered || collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;
        entered = true;

        PlayerStats s = collision.GetComponent<PlayerStats>();

        //попал на флаг
        if (box.IsTouching(collision))
        {
            s.score += 1500;
            transform.Find("Flag").GetComponent<Animator>().SetTrigger("FlagDown");
        }
        //+ попал на палочку
        s.score += 500;
        //считаем время
        s.timeCounting = false;
        s.score = (int)s.time * 50;
        s.time = 0;
        s.SetUI();
        collision.GetComponent<Animator>().SetTrigger("EndReached");
    }

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }
}
