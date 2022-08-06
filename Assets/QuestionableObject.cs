using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionableObject : MonoBehaviour
{
    public Sprite spriteUsed = null;
    //тип объекта: 0 - раздаёт монеты, 1 - раздает грибы, 2 - раздает врагов
    [Range(0,2)]
    public int objectType = 0;
    public GameObject coin;
    public GameObject mushroom;
    public GameObject enemy;
    private bool objectNotUsed = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objectNotUsed && spriteUsed != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = spriteUsed;
            objectNotUsed = false;
            GameObject objToInstantiate = null;
            switch (objectType)
            {
                case 0:
                    collision.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.coin);
                    objToInstantiate = coin;
                    break;
                case 1:
                    collision.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.mushroom_appear);
                    objToInstantiate = mushroom;
                    break;
                case 2:
                    collision.gameObject.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.enemy);
                    objToInstantiate = enemy;
                    break;
                default:
                    break;
            }
            Instantiate(objToInstantiate, new Vector3(transform.position.x, transform.position.y + 0.64f), Quaternion.identity);
        }
    }
}
