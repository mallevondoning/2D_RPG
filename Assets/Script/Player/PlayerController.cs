using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform art;
    [SerializeField]
    private Transform muzzle;
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private BulletTeam team = BulletTeam.player;

    [Header("------------UX------------")]
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float timeToMaxJump = 0.5f;
    [SerializeField]
    private float health = 100f;

    private Rigidbody2D body;
    private Locomotion locomotion;

    private Vector3 normalArt;
    private Vector3 currentArt;
    private float normalArtX;

    private bool justShot = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        locomotion = new Locomotion();

        normalArt = currentArt = art.localScale;
        normalArtX = art.localScale.x;
    }

    private void Update()
    {
        body.velocity = locomotion.Jump(gameObject, body, timeToMaxJump, jumpForce);

        if (body.velocity.x > 0.01)
            art.localScale = currentArt = new Vector3(normalArt.x, normalArt.y, normalArt.z);
        else if (body.velocity.x < -0.01)
            art.localScale = currentArt = new Vector3(-normalArt.x, normalArt.y, normalArt.z);
        else
            art.localScale = currentArt;
    }

    private void FixedUpdate()
    {
        body.velocity = locomotion.Move(body, speed);

        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetAxis("Shoot") != 0)
        {
            if (!justShot)
            {
                justShot = true;

                Vector3 dir = Vector3.right * art.localScale.x / normalArtX;
                Debug.Log(art.localScale.x / normalArtX);

                Bullet spawnedBullet = Instantiate(bullet);
                spawnedBullet.InitBullet(muzzle.transform.position, team, dir, 0f, false);
            }
        }
        if (Input.GetAxis("Shoot") == 0)
            justShot = false;
    }

    public bool IsDead()
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
