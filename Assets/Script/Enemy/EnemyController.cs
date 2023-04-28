using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    public enum EnemyEnvironment
    {
        NoneID = 0,
        Grounded = 1,
        Flying = 2,
    }
    public enum EnemyBehavior
    {
        NoneID = 0,
        Idle = 1,
        Walking = 2,
        Follow = 3,
    }

    public BulletTeam team = BulletTeam.enemy;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private Transform art;

    [Header("------------Enemy behavior------------")]
    public EnemyEnvironment environmentType;
    public EnemyBehavior behaviorType;

    [Header("If behavior is idle")]
    [SerializeField] private bool canTurn;
    [SerializeField] private float turnTimer;

    [Header("------------UX------------")]
    public float health;
    [SerializeField] private float speed;
    [SerializeField] private int expHeld;

    private IEnemyBehavior behavior;
    private Gun gun;

    private Vector3 normalArt;
    private Vector3 currentArt;

    private void OnValidate()
    {
        switch (environmentType)
        {
            case EnemyEnvironment.Grounded:
                body.gravityScale = 1;
                break;
            case EnemyEnvironment.Flying:
                body.gravityScale = 0;
                break;
            default:
                Debug.LogWarning(transform.name + " environment type is " + environmentType + ". Then adding gravity");
                body.gravityScale = 1;
                break;
        }

        switch (behaviorType)
        {
            case EnemyBehavior.Idle:
                behavior = new IdleBehavior();
                var tempBehavior = behavior as IdleBehavior;
                tempBehavior.SetTurning(canTurn, turnTimer);
                break;
            case EnemyBehavior.Walking:
                behavior = new WalkingBehavior();
                break;
            case EnemyBehavior.Follow:
                //behavior = new FollowBehavior();
                Debug.LogWarning(behaviorType + " is disable for now");
                break;
            default:
                Debug.LogWarning(transform.name + " behavior is " + behaviorType + ", and this behvior does not exsist on " + transform.name);
                behavior = null;
                break;
        }

        behavior.Start(transform);
    }

    private void Awake()
    {
        Assert.IsNotNull(behavior, "Behavior is never set");

        body = hitbox.GetComponent<Rigidbody2D>();

        normalArt = currentArt = art.localScale;

        TryGetComponent<Gun>(out gun);
    }

    private void Update()
    {
        if (IsDead())
        {
            PlayerController.Instance.AddExp(expHeld);
            Destroy(gameObject);
        }

        if (body.velocity.x > 0.01)
            art.localScale = currentArt = new Vector3(normalArt.x, normalArt.y, normalArt.z);
        else if (body.velocity.x < -0.01)
            art.localScale = currentArt = new Vector3(-normalArt.x, normalArt.y, normalArt.z);
        else
            art.localScale = currentArt;

        Shoot();
    }

    private void FixedUpdate()
    {
        behavior.Locomotion(hitbox.transform, body, speed);
    }

    private void Shoot()
    {
        if (gun == null) return;
        gun.Shoot();
    }

    private bool IsDead()
    {
        return health <= 0;
    }
}
