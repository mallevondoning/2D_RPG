using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugWalkPos : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform item in transform)
        {
            Gizmos.DrawWireSphere(item.position, 0.2f);
        }
    }
}
