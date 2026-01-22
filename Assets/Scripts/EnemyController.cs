
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{   

    private static EnemyController _instance;

    [Header("Enemies")]
    [SerializeField]
    private int enemyCurrentLevel = 8;
    [SerializeField]
    private List<Enemy> enemyPrefabs;
    [SerializeField]
    private List<Sprite> enemySprites;
    [SerializeField]
    private int enemyHitCount = 0;
    public UnityEvent NextLevelEnemySpawn;

    void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {       
        SpawEnemy();
    }

    public void OnEnemyTriggerEnter()
    {
        enemyHitCount++;
        if (enemyHitCount == enemyCurrentLevel)
        {
            if(enemyCurrentLevel == 9) NextLevelEnemySpawn.Invoke();
            enemyCurrentLevel = enemyCurrentLevel < 9 ? enemyCurrentLevel + 1 : 1;
            enemyHitCount = 0;
            SpawEnemy();
        }
        
    }

    public void SpawEnemy()
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
            Enemy enemy = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
            SpriteRenderer spriteRenderer = enemy.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = enemySprites[enemyCurrentLevel];
            enemy.scoreValue = enemyCurrentLevel;
            enemy.EnemyEliminated.AddListener(OnEnemyTriggerEnter);
        }
    }

}
