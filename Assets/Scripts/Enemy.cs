using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    protected Player player; 
    protected Transform playerTransform;
    public GameObject weapon;
    private Weapon _weapon;
    public float moveSpeed = 3f;
    public float damage = 0;
    public float shootTimeRate = 1.0f;
    public float shootSpeed = 5.0f;
    public int scoreValue { get; set; } = 0;
    public float separationSpeed = 100.0f;

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
            _weapon.Shoot(playerTransform, shootSpeed);
        } 
    }

    public void IncreaseWeaponDamage(float damageIncrease)
    {
        if (weapon != null) {
            Debug.Log("dfsfds" + weapon.name);
            Weapon weaponComponent = weapon.GetComponentInChildren<Weapon>();
            if (weaponComponent != null) weaponComponent.damage += damageIncrease;
        }
        
    }

    // This function is called when another collider enters the trigger zone
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            Destroy(gameObject, 0.5f);
        } 
        if(other.gameObject.CompareTag("PlayerBullet"))
        {   
            player.Score(scoreValue);
            Destroy(gameObject);
            Destroy(other.gameObject);
        } 
    }
    

    void OnDestroy()
    {
        EnemyEliminated?.Invoke();
        EnemyEliminated?.RemoveAllListeners();
    }

}
