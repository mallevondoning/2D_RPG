using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        NoneID = 0,
        Melee = 1,
        Raged = 2,
    }
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
    [SerializeField]
    private GameObject hitbox;
    [SerializeField]
    private Transform art;

    [Header("------------Enemy behavior------------")]
    public EnemyType enemyType;
    public EnemyEnvironment environmentType;
    public EnemyBehavior behaviorType;

    [Header("------------UX------------")]
    public float health;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int expHeld;

    private IEnemyBehavior behavior;
    private Rigidbody2D body;

    private Vector3 normalArt;
    private Vector3 currentArt;

    private void OnValidate()
    {
        switch (behaviorType)
        {
            case EnemyBehavior.Idle:
                behavior = new IdleBehavior();
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
    }

    private void FixedUpdate()
    {
        behavior.Locomotion(hitbox.transform, body, speed);
    }

    private bool IsDead()
    {
        return health <= 0;
    }
}
