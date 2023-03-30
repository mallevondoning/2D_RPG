using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; set; }

    public int Level { get; set; } = 0;
    public int Exp = 0;
    public int ExpCheck { get; set; } = 0;
    public int TotalExp { get; set; } = 0;

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

    public float Hp = 0;
    public float MaxHp = 200f;

    private Rigidbody2D body;
    private Locomotion locomotion;

    private Vector3 normalArt;
    private Vector3 currentArt;
    private float normalArtX;

    private bool justShot = false;

    private void Awake()
    {
        Instance = this;

        body = GetComponent<Rigidbody2D>();
        locomotion = new Locomotion();

        normalArt = currentArt = art.localScale;
        normalArtX = art.localScale.x;

        Hp = MaxHp;

        ExpCheck = ExpCalc();
    }

    private void Update()
    {
        body.velocity = locomotion.Jump(gameObject, body, timeToMaxJump, jumpForce);

        if (body.velocity.x > 0.01 && (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0))
            art.localScale = currentArt = new Vector3(normalArt.x, normalArt.y, normalArt.z);
        else if (body.velocity.x < -0.01 && (Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Horizontal") > 0))
            art.localScale = currentArt = new Vector3(-normalArt.x, normalArt.y, normalArt.z);
        else
            art.localScale = currentArt;

        if (Exp >= ExpCheck)
        {
            Level++;
            Exp -= ExpCheck;

            ExpCheck = ExpCalc();
        }
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

                Vector2 dir = Vector2.right * art.localScale.x / normalArtX;

                Bullet spawnedBullet = Instantiate(bullet);
                spawnedBullet.InitBullet(muzzle.transform.position, team, dir, 0f, false);
            }
        }
        if (Input.GetAxis("Shoot") == 0)
            justShot = false;
    }

    public bool IsDead()
    {
        return MaxHp <= 0;
    }

    private int ExpCalc()
    {
        float a = 1.036f;
        float b = 3.4f;
        float c = 100f;
        float tempExp = Level * Mathf.Pow(a, Level) + Level * b + c;

        return Mathf.CeilToInt(tempExp);
    }

    public void AddExp(int exp)
    {
        Exp += exp;
        TotalExp += exp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bulletCheck = collision.gameObject.GetComponent<Bullet>();

        if (bulletCheck != null && bulletCheck.team != team)
        {
            Destroy(bulletCheck.gameObject);
            MaxHp -= 10f;
        }
    }
}
