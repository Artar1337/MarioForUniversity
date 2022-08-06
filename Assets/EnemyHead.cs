using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{
    private PlayerStats Mario;
    public int scoreValue = 125;

    private void Start()
    {
        Mario = GameObject.Find("Mario").gameObject.GetComponent<PlayerStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //����������� �������� ������ � ��������� ���������
            gameObject.GetComponentInParent<EnemyAI>().isDead = true;
            gameObject.GetComponentInParent<Animator>().SetBool("Dead", true);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //+ ��������� ���� ��� �����
            Mario.score += scoreValue;
            //������� ���������� ��� �����
            Mario.gameObject.GetComponent<Rigidbody2D>().
                AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
        }
    }

}
