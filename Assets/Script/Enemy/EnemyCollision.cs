using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    [SerializeField]
    private EnemyController enemy;

    private void BulletConversion(Collision2D collision)
    {
        Bullet bulletCheck = collision.gameObject.GetComponent<Bullet>();

        CollisionCheck(bulletCheck);
    }
    private void BulletConversion(Collider2D collision)
    {
        Bullet bulletCheck = collision.gameObject.GetComponent<Bullet>();

        CollisionCheck(bulletCheck);
    }

    private void CollisionCheck(Bullet bullet)
    {
        if (bullet != null && bullet.team != enemy.team)
        {
            Destroy(bullet.gameObject);
            enemy.health -= 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletConversion(collision);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        BulletConversion(collision);
    }
}
