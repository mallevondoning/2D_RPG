using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehavior
{
    public void Start(Transform transform);
    public void Locomotion(Transform transform, Rigidbody2D body, float speed);
}
