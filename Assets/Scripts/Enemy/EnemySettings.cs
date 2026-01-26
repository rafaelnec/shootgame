using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    public Sprite enemySprite;
    public GameObject effectPrefab;
    public float enemySpeedRate = 0.25f;
    public float enemyDamageRate = 0.25f;
    public float enemyShootSpeedRate = 0.25f;
    public Color enemyColor;
}

[CreateAssetMenu(fileName = "EnemySettings", menuName = "Scriptable Objects/EnemySettings")]
public class EnemySettings : ScriptableObject
{
    public List<EnemyData> EnemyObjects = new List<EnemyData>();
    
}
