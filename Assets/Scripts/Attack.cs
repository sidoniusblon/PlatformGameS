using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RunnerEnemy"))
        {
            EnemyDead(collision.collider);
        }
        else if (collision.gameObject.CompareTag("ConstEnemy"))
        {
            GameObject Enemy = collision.gameObject;
            Enemy.GetComponent<BoxCollider2D>().isTrigger = true;
            Enemy.GetComponent<PlatformMoving>().move = false;
            Enemy.gameObject.AddComponent<Rigidbody2D>();
            EnemyDead(collision.collider);
            AudioTest.Instance.enemyDeadMusic();
            GameManager.Instance.score += 10;
            GameManager.Instance.UpdateScore();
        }
    }
    void EnemyDead(Collider2D collider)
    {
        AudioTest.Instance.enemyDeadMusic();
        collider.gameObject.GetComponent<SpriteRenderer>().color = new Color32(108, 45, 45, 255);
        collider.gameObject.GetComponent<PlatformMoving>().move = false;
        Destroy(collider.gameObject.GetComponent<BoxCollider2D>());
        Destroy(collider.gameObject,2);
    }
}
