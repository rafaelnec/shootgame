
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyController : MonoBehaviour
{   

    [Header("Enemies")]
    [SerializeField]
    private Enemy _enemy;
    [SerializeField]
    private int enemyCurrentLevel = 1;
    [SerializeField]
    private List<Enemy> enemyPrefabs;
    [SerializeField]
    private List<Sprite> enemySprites;
    [SerializeField]
    private int enemyHitCount = 0;
    private GameController _gameController;


    void Start()
    {
        _gameController = FindFirstObjectByType<GameController>();
        SpawEnemy();
    }

    public void OnEnemyTriggerEnter(bool hitByNuke = false)
    {
        enemyHitCount++;
        if (enemyCurrentLevel < 9 && (hitByNuke || enemyCurrentLevel == enemyHitCount))
        {
            enemyCurrentLevel++;
            SpawEnemy();   
        } else if (enemyCurrentLevel >= 9)
        {
            enemyCurrentLevel = 1;
            // _gameController.NextLevel();
            SpawEnemy();
        }
    }

    void SpawEnemy()
    {
        enemyHitCount = 0;
        CreateEnemy(); 
    }

    public void CreateEnemy()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
        // Get screen bounds in world coordinates
        for (int i = 0; i < enemyCurrentLevel; i++)
        {
            // Generate random position within screen bounds
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
            Enemy prefabToInstantiate = enemyPrefabs[randomIndex];
            SpriteRenderer spriteRenderer = prefabToInstantiate.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemySprites[enemyCurrentLevel];
            Enemy enemy = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
            enemy.scoreValue = enemyCurrentLevel;
        }
    }

}
