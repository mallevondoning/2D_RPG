using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private BulletTeam team = BulletTeam.enemy;
    [SerializeField]
    private float health = 50f;

    private void Update()
    {
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    private bool IsDead()
    {
        return health <= 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bulletCheck = collision.gameObject.GetComponent<Bullet>();

        if (bulletCheck != null && bulletCheck.team != team)
        {
            Destroy(bulletCheck.gameObject);
            health -= 10f;
        }
    }
}
