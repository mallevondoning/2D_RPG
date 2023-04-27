using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform art;

    [Header("UX")]
    [SerializeField] private float maxTimer;
    private float currentTime = 0;

    private float normalArtX;

    private void Awake()
    {
        normalArtX = art.localScale.x;
    }

    public void Shoot()
    {
        if (currentTime >= maxTimer)
        {
            currentTime = 0;

            Vector2 dir = Vector2.right * art.localScale.x / normalArtX;

            Bullet spawnedBullet = Instantiate(bullet);
            spawnedBullet.InitBullet(firePoint.position, BulletTeam.enemy, dir, 0, false);
        }

        currentTime += Time.deltaTime;
    }
}
