using System.Security.Cryptography;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _bulletSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector3 _direction;

    private float _bulletDamage;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.linearVelocity = GetDierection() * _bulletSpeed;
    }

    void Update()
    {
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public Vector3 GetDierection()
    {
        if (_direction == Vector3.zero)
        {
            return transform.up;
        }
        return _direction;
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            _direction = direction;
        } 
        
    }

    public float GetBulletDamage()
    {
        return _bulletDamage;
    }

    public void SetBulletDamage(float damage)
    {
        _bulletDamage = damage;
    }
   
    public void SetBulletSpeed(float speed)
    {
        _bulletSpeed = speed;
    }

}
