using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
            return;

        collision.gameObject.GetComponent<PlayerMovement>().characterSpeed = 0f;
        collision.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.success);
        GameObject tmp = GameObject.Find("Main Canvas");
        tmp.transform.Find("Score").gameObject.SetActive(false);
        tmp.transform.Find("Time").gameObject.SetActive(false);
        tmp.transform.Find("Pause").gameObject.SetActive(false);
        tmp = tmp.transform.Find("Final").gameObject;
        tmp.SetActive(true);
        tmp.transform.Find("Text").GetComponent<TMPro.TMP_Text>().text = "You won!\r\nScore: " +
            collision.gameObject.GetComponent<PlayerStats>().score;
    }
}
