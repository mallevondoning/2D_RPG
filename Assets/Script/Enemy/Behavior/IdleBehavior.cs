using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : IEnemyBehavior
{
    private Vector3 idleArt;

    public void Start(Transform transform)
    {
        Transform artTransform = transform.Find("Hitbox").Find("Art");
        idleArt = artTransform.localScale;
    }

    public void Locomotion(Transform transform, Rigidbody2D body, float speed) { }
}
