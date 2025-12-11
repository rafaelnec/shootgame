using System.Collections;
using UnityEngine;

public class EnemyMachineGun : Enemy
{

    public float followDistance = 3.0f; // The distance to maintain from the target  

    void Update()
    {
        Vector2 directionToMe = transform.position - playerTransform.position;
        Vector2 desiredOffset = directionToMe.normalized * followDistance;
        Vector2 desiredPosition = (Vector2)playerTransform.position + desiredOffset;
        transform.position = Vector2.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

    }
}
