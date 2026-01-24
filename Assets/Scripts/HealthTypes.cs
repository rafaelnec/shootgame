using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class HealthData
{
    public float healAmount;
    public Sprite sprite;
}


[CreateAssetMenu(fileName = "HealthTypes", menuName = "Scriptable Objects/HealthTypes")]
public class HealthTypes : ScriptableObject
{
    public List<HealthData> HealthObjects = new List<HealthData>();
}

interface IHealthTypes
{
    float HealthAmount { get; set; }
    Sprite HealthSprite { get; set; }
}
