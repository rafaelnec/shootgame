using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EnemyBigBossController : MonoBehaviour
{
    private static EnemyBigBossController _instance;

    [SerializeField] private List<Enemy> enemyPrefabs; // Drag different types here
    [SerializeField] private EnemySettings enemySettings;

    [SerializeField] private int bigBossRound = 10; 

    private int _randomSpawn = 0;
    public UnityEvent<int> bigBossHitted;
    public UnityEvent GameWin;

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

    public void SpawnBigBoss()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        _randomSpawn = Random.Range(1, 10);

        for (int i = 0; i < _randomSpawn; i++)
        {
            float randomX = Random.Range(bottomLeft.x, topRight.x);
            float randomY = Random.Range(bottomLeft.y, topRight.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);
            
            Enemy nextEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            Enemy enemy = Instantiate(nextEnemy, spawnPosition, Quaternion.identity);
            enemy.enemyData = enemySettings.EnemyObjects[0];
            enemy.EnemyEliminated.AddListener(OnEnemyEliminated);
        }
    }

    public void OnEnemyEliminated()
    {
        _randomSpawn--;
        bigBossHitted.Invoke(10);
        if (_randomSpawn <= 0) {
            bigBossRound--;
            if (bigBossRound <= 0) GameWin.Invoke();
            else SpawnBigBoss();
        } 
    }
}
