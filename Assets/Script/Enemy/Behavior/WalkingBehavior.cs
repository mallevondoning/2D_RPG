using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingBehavior : IEnemyBehavior
{
    [SerializeField]
    private List<Vector3> posList = new List<Vector3>();

    private EnemyController enemy;

    private string positionChildName = "WalkPositions";
    private bool foundList = false;

    private int index = 0;
    private Vector3 currentDir = Vector3.zero;

    public void Start(Transform transform)
    {
        Transform positions = transform.Find(positionChildName);

        if (positions != null)
        {
            if (positions.childCount >= 2)
            {
                for (int i = 0; i < positions.childCount; i++)
                {
                    posList.Add(positions.GetChild(i).position);
                }

                foundList = true;
            }
            else
                Debug.LogWarning("Too few children under "+ positions.name+" (min 2)");
        }
        else
            Debug.LogError("Can't find a child named "+positionChildName+" under "+transform.name);
    }
    
    public void Locomotion(Transform transform, Rigidbody2D body, float speed)
    {
        if (foundList)
        {
            transform.parent.gameObject.TryGetComponent(out enemy);

            Vector3 dir = posList[index % posList.Count] - transform.position;
            if (enemy != null && enemy.environmentType == EnemyController.EnemyEnvironment.Grounded)
                dir.y = 0;

            dir = dir.normalized;
            if (currentDir != dir)
            {
                if (currentDir.magnitude > 0)
                    index++;

                currentDir = dir;
            }

            body.velocity = new Vector2(dir.x * speed, body.velocity.y);
        }
    }
}
