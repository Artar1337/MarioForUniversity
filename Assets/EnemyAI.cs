using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemySpeed = -2f;
    public float timeCooldown = 0f;
    public bool canWalk = false;
    public bool isDead = false;
    private Animator Player;
    private Collider2D collider2d;

    private void Start()
    {
        collider2d = GetComponent<Collider2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("BackWall"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("BackWall"));
        Player = GameObject.Find("Mario").GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //столкновение с игроком - игрок теряет жизнь
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !isDead)
        {
            PlayerStats s = Player.GetComponent<PlayerStats>();
            GetComponent<Animator>().SetBool("Dead", true);
            isDead = true;
            if (s.isBig)
            {
                s.SetBig();
                collision.transform.localScale = new Vector3(4, 4, 1);
                return;
            }
            Player.SetBool("Dead", true);
            return;   
        }

        //столкновение с дружественным противником - разворачивается
        if ((collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) &&
            timeCooldown < 0.1f)
        {
            enemySpeed = -enemySpeed;
            timeCooldown = 0.3f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("SpecialWall")) &&
            timeCooldown < 0.1f)
        {
            enemySpeed = -enemySpeed;
            timeCooldown = 0.3f;
        }
    }

    //первый раз видим врага - он начинает идти
    private void OnWillRenderObject()
    {
        canWalk = true;
    }

    private void DisableEnemy()
    {
        if (collider2d.enabled)
        {
            collider2d.enabled = false;
            transform.Find("Hit Collider").gameObject.SetActive(false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!canWalk)
            return;
        timeCooldown -= Time.deltaTime;
        if (timeCooldown < 0)
            timeCooldown = 0;
        transform.position = transform.position + new Vector3
                (enemySpeed * Time.deltaTime, 0, 0);
        if (transform.position.y < -100)
        {
            Destroy(this.gameObject);
        }
    }
}
