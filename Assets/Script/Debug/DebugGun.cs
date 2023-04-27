using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGun : MonoBehaviour
{
    [SerializeField] private Transform body;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(body.position, transform.position);
    }
}
