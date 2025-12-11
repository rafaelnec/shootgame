using UnityEngine;

public abstract class PlayableObject : MonoBehaviour, IDamageable
{
    [SerializeField]
    protected Rigidbody2D _rb;

    // Playable object is composed with Health - It is part of Playable Object
    public Health health = new Health();

    // Playable object has a weapon - it is part of the aggregate objects we can give it
    public GameObject weapon;

    private string _name;

    [SerializeField]
    protected float _speed;

    public PlayableObject() { }
    public PlayableObject(float maxHealth)
    {
        health = new Health(maxHealth);
    }
    public PlayableObject(float maxHealth, float speed, string name = "")
    {
        health = new Health(maxHealth);
        speed = _speed;
        name = _name;
    }

    public abstract void Move(Vector2 direction);
    public abstract void Move(Vector2 direction, float speed);
    public abstract void Move(Vector2 direction, Vector2 target);

    public void Shoot()
    {
        Weapon _weapon = weapon.GetComponent<Weapon>();
        _weapon.SetBulletTag("PlayerBullet");
        _weapon.Shoot();
    }

    public virtual void Knockout()
    {
        Debug.Log($"oops");
    }

    public virtual void Knockout(bool dramatic)
    {
        if (dramatic)
        {
            Debug.Log($"{name} has been knocked out.");
            Debug.Log($"Their career will never recover.");
            Debug.Log($"This event has been recorded, televised, and shared for the whole world to see.");
            Debug.Log($"Unfortunate.");
            Debug.Log($"Perhaps next time, things will go better.");
            Debug.Log($"This, is the beginning, of { name }s underdog story.");
        }
        else
        {
            Knockout();
        }
    }

    public void GetDamage(float damage)
    {
        health.DeductHealth(damage);
        if(health.GetHealth() <= 0)
        {
            Knockout();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            float damage = other.gameObject.GetComponent<Bullet>().GetBulletDamage();  
            Debug.Log("Playable Object Hit by Enemy Bullet");
        }

    }
}
