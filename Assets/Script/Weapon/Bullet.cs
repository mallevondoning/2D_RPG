using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private Collider2D collision;

    [Header("------------UX------------")]
    [SerializeField]
    private float bulletAliveTime;
    [SerializeField]
    private float speed;

    private float currentAliveTime = 0;

    public BulletTeam team { get; set; }
    private Vector3 direction;
    private float gravityMultiplier;
    private bool collideWithWall;

    public void InitBullet(Vector2 start, BulletTeam team, Vector2 direction, float gravityMultiplier, bool collideWithWall)
    {
        transform.position = start;
        this.team = team;
        this.direction = direction;
        this.gravityMultiplier = gravityMultiplier;
        this.collideWithWall = collideWithWall;
    }

    private void Update()
    {
        currentAliveTime += Time.deltaTime;

        if (currentAliveTime >= bulletAliveTime)
        {
            Destroy(gameObject);
        }

        collision.isTrigger = !collideWithWall;

        body.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
