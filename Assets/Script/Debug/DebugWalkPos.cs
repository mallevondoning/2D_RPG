using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWalkPos : MonoBehaviour
{
    [SerializeField] private Collider2D enemyCollider;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        var circle = enemyCollider as CircleCollider2D;
        foreach (Transform item in transform)
        {
            Gizmos.DrawWireSphere(item.position, circle.radius);
        }
    }
}
