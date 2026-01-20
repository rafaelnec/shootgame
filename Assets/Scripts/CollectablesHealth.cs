using System.Collections.Generic;
using UnityEngine;

public class CollectablesHealth : Collectables
{
    private List<Dictionary<string,object>> _healthObj = new List<Dictionary<string,object>> {
        new Dictionary<string,object> { {"health", 20}, {"color", Color.pink} },
        new Dictionary<string,object> { {"health", 40}, {"color", Color.yellow} }, 
        new Dictionary<string,object> { {"health", 80}, {"color", Color.blue} }, 
        new Dictionary<string,object> { {"health", 100}, {"color", Color.green} } 
    };

    private SpriteRenderer _sprite;

    private int _healthAmount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int randomIndex = Random.Range(0, _healthObj.Count);
        Dictionary<string,object> healthItem = _healthObj[randomIndex];

        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = (Color)healthItem["color"];

        _healthAmount = (int)healthItem["health"];
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
