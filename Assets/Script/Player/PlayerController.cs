using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Transform art;


    [Header("------------UX------------")]
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float timeToMaxJump = 0.5f;

    private Rigidbody2D body;
    private Locomotion locomotion;

    private Vector3 normalArt;
    private Vector3 currentArt;

    private float health;

    private bool justShot = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        locomotion = new Locomotion();

        normalArt = currentArt = art.localScale;
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
                Debug.Log("Shot");

                justShot = true;
            }
        }
        if (Input.GetAxis("Shoot") == 0)
            justShot = false;
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(1,1,0));
    }
    */
}
