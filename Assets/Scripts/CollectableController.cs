using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollectableController : MonoBehaviour
{
    private static CollectableController _instance;
    
    [SerializeField] private List<Collectables> collectablesPrefabs;
    [SerializeField] private float minCollectablesSpawnDelay = 5.0f;
    [SerializeField] private float maxCollectablesSpawnDelay = 30.0f;

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
        StartCoroutine(routine: CollectablesSpawner());
    }

    void CreateCollectable()
    {

        int randomIndex = UnityEngine.Random.Range(0, collectablesPrefabs.Count);
        
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector2 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        float randomX = Random.Range(bottomLeft.x, topRight.x);
        float randomY = Random.Range(bottomLeft.y, topRight.y);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        
        Collectables prefabToInstantiate = collectablesPrefabs[randomIndex];
        Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
        
    }

    IEnumerator CollectablesSpawner()
    {
        while(true)
        {
            float delay = Random.Range(minCollectablesSpawnDelay, maxCollectablesSpawnDelay);
            yield return new WaitForSeconds(delay);
            CreateCollectable();
        }
    }

    
}
