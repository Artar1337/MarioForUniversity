using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float characterSpeed = 3f;
    public float jumpHeight = 4.5f;
    //стоим ли на земле сейчас (иначе Марио может застрять в потолке!)
    public bool isGrounded = false;

    private Joystick joystick;
    private GameObject joystickContainer;
    private Rigidbody2D rigidbodyReference;
    private Animator animatorReference;
    private PlayerStats stats;
    private Collider2D collider2d;
    //можно ли использовать джойстик
    private bool canWalk = true;
    private bool playingFinalAnimation = false;

    private void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rigidbodyReference = GetComponent<Rigidbody2D>();
        animatorReference = GetComponent<Animator>();
        joystickContainer = GameObject.Find("Main Canvas").transform.Find("Fixed Joystick").gameObject;
        joystick = joystickContainer.GetComponent<Joystick>();
        stats = GetComponent<PlayerStats>();
        rigidbodyReference.bodyType = RigidbodyType2D.Dynamic;
        stats.timeCounting = true;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.main_theme);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")
            && !isGrounded)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")
            && isGrounded)
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //столконвение с триггером растения - потеря жизни
        if(collision.gameObject.layer == LayerMask.NameToLayer("Plant"))
        {
            if (stats.isBig)
            {
                stats.SetBig();
                transform.localScale = new Vector3(4, 4, 1);
                return;
            }
            animatorReference.SetBool("Dead", true);
        }
    }

    private void DisablePlayer()
    {
        if (collider2d.enabled)
        {
            collider2d.enabled = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            Camera.main.GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.death);
            stats.timeCounting = false;
        }
    }

    private void ShowDeathScreen()
    {
        PlayerStats.lives--;
        Transform t;
        if (PlayerStats.lives <= 0)
        {
            //gameover screen
            StartCoroutine(CountdownThenLoad(1, true));
        }
        else
        {
            //show lives and continue
            t = GameObject.Find("Main Canvas").transform.Find("Lives");
            t.gameObject.SetActive(true);
            t.Find("Text").GetComponent<TMPro.TMP_Text>().text = PlayerStats.lives.ToString();
            StartCoroutine(CountdownThenLoad(3));
        }
    }

    IEnumerator CountdownThenLoad(int seconds, bool gameover = false)
    {
        int counter = seconds;
        while (counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
        }

        if (gameover)
        {
            GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.gameover);
            GameObject.Find("Main Canvas").transform.Find("GameOver").gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameObject.Find("Main Canvas").transform.Find("Lives").gameObject.SetActive(false);
        }
    }

    private void FinalAnimation()
    {
        transform.position = new Vector3(165, transform.position.y, transform.position.z);
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.flag);
        canWalk = false;
    }

    void Update()
    {
        //вылетели за пределы карты - останавливаем тело
        if (transform.position.y < -100)
        {
            rigidbodyReference.bodyType = RigidbodyType2D.Static;
            transform.position = transform.position + new Vector3(0, 10, 0);
            return;
        }

        if (animatorReference.GetBool("Dead"))
            return;

        //Марио идёт в замок
        if (playingFinalAnimation)
        {
            transform.position = transform.position + new Vector3
                (characterSpeed * Time.deltaTime, 0, 0);
            return;
        }

        //Марио спускается по флагштоку
        if (!canWalk)
        {
            //если марио в нижней точке флагштока
            if (isGrounded)
            {
                animatorReference.SetFloat("Speed", 1);
                animatorReference.SetBool("Jumping", false);
                playingFinalAnimation = true;
                canWalk = true;
            }
            return;
        }

        var move = joystick.Horizontal;
        if (!joystickContainer.activeInHierarchy)
            move = 0;
        animatorReference.SetFloat("Speed", Mathf.Abs(move));

        if (Mathf.Abs(move) > 0.2f)
        {
            transform.position = transform.position + new Vector3
                (characterSpeed * Time.deltaTime * Mathf.Abs(move) / move, 0, 0);

            float characterScale = stats.scale;

            //разворачиваем в нужную сторону
            float scale;
            if (move > 0.01f)
            {
                scale = characterScale;
            }
            else
            {
                scale = -characterScale;
            }
            transform.localScale = new Vector3(scale, characterScale, characterScale);
        }

        var jump = joystick.Vertical;

        var velocityY = Mathf.Abs(rigidbodyReference.velocity.y);

        animatorReference.SetBool("Jumping", velocityY > 0.01f);

        if (velocityY < 0.01f && jump > 0.4f && isGrounded)
        {
            GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.jump);
            rigidbodyReference.AddForce
                   (new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
    }
}
