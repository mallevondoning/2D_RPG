using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletAliveTime;
    [SerializeField]
    private float speed;

    private float currentAliveTime = 0;

    public BulletTeam team { get; set; }
    private Vector3 direction;
    private float gravityMultiplier;
    private bool collideWithWall;

    public void InitBullet(Vector3 start, BulletTeam team, Vector3 direction, float gravityMultiplier, bool collideWithWall)
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

        transform.position += direction * speed * Time.deltaTime;
    }
}
