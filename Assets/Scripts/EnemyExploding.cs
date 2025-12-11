using UnityEngine;

public class EnemyExploding : Enemy
{
   
    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize(); // Normalize for consistent speed

            // Move the enemy
            transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

            // (Optional) Rotate the enemy to face the player (for 2D)
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
