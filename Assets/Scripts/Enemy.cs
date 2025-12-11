using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Player player; 
    protected Transform playerTransform;
    public float moveSpeed = 3f;
    public float damage = 0;
    public float shootTimeRate = 1.0f;
    public float shootSpeed = 5.0f;
    public GameObject weapon;
    private Weapon _weapon;

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
        Debug.Log("Enemy Shoot Coroutine Started" + _weapon == null);
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
        if(other.gameObject.tag == "Player")
        {
            player.TakeDamage(damage);
            Destroy(gameObject, 0.5f);
        }

        if(other.gameObject.tag == "PlayerBullet")
        {
            Destroy(gameObject);
        }
    }
}
