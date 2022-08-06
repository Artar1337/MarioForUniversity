using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAI : MonoBehaviour
{
    private BoxCollider2D box;
    private Animator animator;
    private Vector3 initialPosition;
    private Vector3 endPosition;
    public float timeInAttack, timeInGrow, waitTime, currentTime, offset;
    //0 - поднимается вверх, 1 - атакует, 2 - спускается вниз, 3 - ждет
    private int stage = 0;
    private float step;

    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentTime = timeInGrow;
        step = offset / timeInGrow;
        initialPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
    }

    private void TriggerSetActive()
    {
        box.enabled = !box.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        if(stage == 0)
        {
            if (transform.position.y + step * Time.deltaTime < endPosition.y)
                transform.position = new Vector3(transform.position.x,
                    transform.position.y + step * Time.deltaTime, transform.position.z);
            if (currentTime < 0)
            {
                step = offset / timeInAttack;
                transform.position = endPosition;
                animator.SetTrigger("Attack");
                currentTime = timeInAttack;
                stage++;
            }
        }
        else if (stage == 1)
        {
            if (currentTime < 0)
            {
                step = offset / timeInGrow;
                currentTime = timeInGrow;
                stage++;
            }
        }
        else if(stage == 2)
        {
            if (transform.position.y - step * Time.deltaTime > initialPosition.y)
                transform.position = new Vector3(transform.position.x,
                    transform.position.y - step * Time.deltaTime, transform.position.z);
            if (currentTime < 0)
            {
                stage = 0;
                transform.position = initialPosition;
                currentTime = waitTime;
            }
        }
        else
        {
            if (currentTime < 0)
            {
                stage = 0;
                currentTime = timeInGrow;
            }
        }
    }
}
