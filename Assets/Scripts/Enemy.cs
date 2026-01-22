using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    private EnemyController enemyController;
    protected Player player; 
    protected Transform playerTransform;
    public float moveSpeed = 3f;
    public float damage = 0;
    public float shootTimeRate = 1.0f;
    public float shootSpeed = 5.0f;
    public int scoreValue { get; set; } = 0;
    public GameObject weapon;
    private Weapon _weapon;

    // private bool _playerHitEnemy = false;

    public UnityEvent enemyHitByPlayer;

    public void Awake()
    {
        enemyHitByPlayer = new UnityEvent();
    }

    protected virtual void Start()
    {
        enemyController = FindFirstObjectByType<EnemyController>();
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
        enemyHitByPlayer.Invoke();
        enemyHitByPlayer.RemoveAllListeners();
    }

    public void EnemyHitByNuke(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

}
