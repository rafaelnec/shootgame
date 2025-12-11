using TMPro;
using UnityEngine;

// Our Player class is our "Object" that we'll use for the player character
public class Player : PlayableObject
{
    [SerializeField]
    private Rigidbody2D _playerRB;

    [SerializeField]
    private Camera _playerCamera;

    public TextMeshProUGUI healthText;

    void Start()
    {
        health.AddHealth(100);
        SetHealthText();
    }

    public override void Move(Vector2 direction)
    {
        _playerRB.linearVelocity = direction * _speed * Time.deltaTime;
    }

    public override void Move(Vector2 direction, float speed)
    {
        _playerRB.linearVelocity = direction * speed * Time.deltaTime;
    }

    public override void Move(Vector2 direction, Vector2 target)
    {
        _playerRB.linearVelocity = direction * _speed * Time.deltaTime;

        var playerScreenPos = _playerCamera.WorldToScreenPoint(transform.position);

        target.x -= playerScreenPos.x;
        target.y -= playerScreenPos.y;

        // We will come back to this- don't worry how this works right now!
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle -90);
    }

    public override void Knockout()
    {
        // Bring up the game over screen
        Debug.Log($"Game over!");
    }

    public void TakeDamage(float damage)
    {
        health.DeductHealth(damage);
        SetHealthText();
    }

    private void SetHealthText()
    {
        healthText.text = health.GetHealth().ToString();
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            float damage = other.gameObject.GetComponent<Bullet>().GetBulletDamage();  
            TakeDamage(damage);
            Debug.Log("Playable Object Hit by Enemy Bullet");
        }

    }

}
