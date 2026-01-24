using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public Sprite PlayerSprite;
    public float enemyIncreaseSpeedRate;
    public float enemyIncreaseDamageRate;
    public float enemyIncreaseShootSpeedRate;
}

[CreateAssetMenu(fileName = "GameLevelSettings", menuName = "Scriptable Objects/GameLevelSettings")]
public class GameLevelSettings : ScriptableObject
{
    public List<GameData> GameData = new List<GameData>();
}