using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion
{
    //internal veribles
    private float timeJumpHeld;
    private bool grounded;
    private bool stoppedJumping;

    public Vector2 Move(Rigidbody2D body, float speed)
    {
        float velX = Input.GetAxis("Horizontal") * speed;

        float velY = body.velocity.y;

        return new Vector2(velX, velY);
    }

    //modify for future stuff
    public Vector2 Jump(GameObject origin, Rigidbody2D body, float maxJump, float jumpForce)
    {
        int mask = LayerMask.GetMask("Ground");
        Vector2 center = Vector2.one * origin.GetComponent<BoxCollider2D>().size.x / 2;
        grounded = Physics2D.BoxCast(origin.transform.position, center, 360f, Vector2.down, 0.3f, mask);

        if (Input.GetAxis("Jump") > 0)
        {
            if (!stoppedJumping && timeJumpHeld < maxJump)
                timeJumpHeld += Time.fixedDeltaTime;

            if (grounded)
                stoppedJumping = false;
        }
        else
        {
            timeJumpHeld = 0;
            stoppedJumping = true;
        }

        float normJump = timeJumpHeld / maxJump;
        normJump = Mathf.Clamp01(normJump);

        float finalJumpForce = normJump * jumpForce;

        if (Input.GetAxis("Jump") > 0 && !stoppedJumping && timeJumpHeld < maxJump)
            return new Vector2(body.velocity.x, finalJumpForce);
        else    
            return new Vector2(body.velocity.x, body.velocity.y);
    }
}
