using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01 : MonoBehaviour
{
  
    [Header("Game Entities")]
    [SerializeField]
    private List<Enemy> enemyPrefabs;
    [SerializeField]
    private static Scene01 _instance;

    [Header("Game Variables")]
    [SerializeField]
    private float enemySpawnRate = 1.0f;

    private bool isEnemySpawning = false;


    public static Scene01 GetInstance()
    {
        return _instance;
    }

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnemySpawning = true;
        // StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateEnemy();
        }
    }

    void CreateEnemy()
    {

        // Get screen bounds in world coordinates
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // Generate random position within screen bounds
        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);
        
        int randomIndex = UnityEngine.Random.Range(0, enemyPrefabs.Count);
        Enemy prefabToInstantiate = enemyPrefabs[randomIndex];
        Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
        
    }

    IEnumerator EnemySpawner()
    {
        while(isEnemySpawning)
        {
            yield return new WaitForSeconds(1.0f / enemySpawnRate);
            CreateEnemy();
        }
    }

    public void NewGameButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
