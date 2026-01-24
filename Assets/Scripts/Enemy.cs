using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected Player player; 
    protected Transform playerTransform;
    public EnemyData enemyData;
    public GameObject weapon;
    private Weapon _weapon;
    public float moveSpeed = 3f;
    public float damage = 0;
    public float shootTimeRate = 1.0f;
    public float shootSpeed = 5.0f;
    public int scoreValue { get; set; } = 0;

    public UnityEvent EnemyEliminated;

    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        playerTransform = player.transform;
        if(weapon)
            _weapon = weapon.GetComponent<Weapon>();
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while(_weapon)
        {
            yield return new WaitForSeconds(shootTimeRate);
            _weapon.SetBulletTag("EnemyBullet");
            _weapon.setBulletColour(enemyData.enemyColor);
            _weapon.Shoot(playerTransform, shootSpeed);
        } 
    }

    public void IncreaseWeaponDamage(float damageIncrease)
    {
        if (weapon != null) {
            Weapon weaponComponent = weapon.GetComponentInChildren<Weapon>();
            if (weaponComponent != null) weaponComponent.damage += damageIncrease;
        }
        
    }

    // This function is called when another collider enters the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(player) player.TakeDamage(damage);
            Destroy(gameObject, 0.5f);
        } 
        if(other.gameObject.CompareTag("PlayerBullet"))
        {   
            if(player) player.Score(scoreValue);
            Transform explosion = gameObject.transform.Find("ExplosionEffect");
            if (explosion != null) explosion.gameObject.SetActive(true);
            else Destroy(gameObject);
            Destroy(other.gameObject);
        } 
    }
    

    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;

        EnemyEliminated?.Invoke();
        EnemyEliminated?.RemoveAllListeners();

        if (enemyData.effectPrefab != null)
        {
            GameObject effect = Instantiate(enemyData.effectPrefab, transform.position, Quaternion.identity);
            effect.SetActive(true);
        }
    }

}
