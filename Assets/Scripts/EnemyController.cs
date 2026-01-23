
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{   

    private static EnemyController _instance;

    [Header("Enemies")]
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private List<Sprite> enemySprites;
    [SerializeField] private float enemyIncreaseSpeedRate = 0.25f;
    [SerializeField] private float enemyIncreaseDamageRate = 0.25f;
    [SerializeField] private float enemyIncreaseShootSpeedRate = 0.25f;
    
    private float _enemyIncreaseSpeed = 0f;
    private float _enemyIncreaseDamage = 0f;
    private float _enemyIncreaseShootSpeed = 0f;
    
    public UnityEvent EnemyEliminated;


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


    public void OnEnemyEliminated()
    {
        EnemyEliminated.Invoke();
    }

    public void SpawEnemy(int enemyCurrentLevel)
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
            enemy.moveSpeed += _enemyIncreaseSpeed;
            enemy.damage += _enemyIncreaseDamage;
            enemy.shootSpeed += _enemyIncreaseShootSpeed;
            enemy.IncreaseWeaponDamage(_enemyIncreaseDamage);
            enemy.EnemyEliminated.AddListener(OnEnemyEliminated);
        }
    }

    public void SetUpEnemyLevel(int level)
    {
        _enemyIncreaseSpeed = (level - 1) * enemyIncreaseSpeedRate;
        _enemyIncreaseDamage = (level - 1) * enemyIncreaseDamageRate;
        _enemyIncreaseShootSpeed = (level - 1) * enemyIncreaseShootSpeedRate;
    }

}
