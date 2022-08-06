using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public float speed = -3f;
    public float timeCooldown = 0.3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //столкновение с игроком - игрок получает жизнь
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStats s = collision.gameObject.GetComponent<PlayerStats>();
            if (!s.isBig)
                s.SetBig();
            s.score += 1000;
            collision.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.mushroom);
            collision.transform.localScale = new Vector3(6, 6, 1);
            Destroy(gameObject);
            return;
        }

        //столкновение с дружественным противником - разворачивается
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) &&
            timeCooldown < 0.1f)
        {
            speed = -speed;
            timeCooldown = 0.3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("SpecialWall")) &&
            timeCooldown < 0.1f)
        {
            speed = -speed;
            timeCooldown = 0.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeCooldown -= Time.deltaTime;
        if (timeCooldown < 0)
            timeCooldown = 0;
        transform.position = transform.position + new Vector3
                (speed * Time.deltaTime, 0, 0);
        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }
}
