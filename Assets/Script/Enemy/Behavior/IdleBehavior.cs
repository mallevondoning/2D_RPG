using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : IEnemyBehavior
{
    private Transform artTransform;
    private Vector3 idleArt;
    private float turnTimer;
    private float currentTurnTimer;
    private bool canTurn;

    public void Start(Transform transform)
    {
        artTransform = transform.Find("Hitbox").Find("Art");
        idleArt = artTransform.localScale;
    }

    public void Locomotion(Transform transform, Rigidbody2D body, float speed)
    {
        if (!canTurn) return;

        currentTurnTimer = TimeUtil.UpdateTimer(currentTurnTimer);

        if (TimeUtil.IsTimerDone(currentTurnTimer, turnTimer))
        {
            Debug.LogWarning("Not implemented");

            currentTurnTimer = 0;
        }
    }

    public void SetTurning(bool canTurn, float turnTimer)
    {
        this.canTurn = canTurn;
        this.turnTimer = turnTimer;
    }
}
