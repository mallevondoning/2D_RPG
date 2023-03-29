using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletAliveTime;
    [SerializeField]
    private float speed;

    private Rigidbody2D body;
    private Collider2D collision; 

    private float currentAliveTime = 0;

    public BulletTeam team { get; set; }
    private Vector3 direction;
    private float gravityMultiplier;
    private bool collideWithWall;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

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

        collision.isTrigger = collideWithWall;

        body.gravityScale = gravityMultiplier;
        body.velocity = direction * speed;
    }
}
