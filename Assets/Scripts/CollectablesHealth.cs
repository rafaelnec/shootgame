using System.Collections.Generic;
using UnityEngine;

public class CollectablesHealth : Collectables
{
    [SerializeField] private HealthTypes healthTypes;    

    private SpriteRenderer _sprite;

    private int _healthAmount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        int randomIndex = Random.Range(0, healthTypes.HealthObjects.Count);
        HealthData healthItem = healthTypes.HealthObjects[randomIndex];

        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = healthItem.sprite;
        _healthAmount = (int)healthItem.healAmount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player.Heal(_healthAmount);
            Destroy(gameObject);
        }
    }

}
