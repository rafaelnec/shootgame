using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Our Player class is our "Object" that we'll use for the player character
public class Player : PlayableObject
{
    [SerializeField]
    private Rigidbody2D _playerRB;

    [SerializeField]
    private Camera _playerCamera;

    public TextMeshProUGUI healthText;

    public GameObject gameOverScreen;

    public GameObject nukeBar;
    public int nukeCount = 0;
    public GameObject gunpPowerUpBar;   
    public int gunpPowerUpCount = 0;
    public GameObject gunPowerUpCountDown;
    public float powerGunUpDuration = 5f;
    public bool _shootPowerGunUpEnd = false;
    public bool _shootPowerGunActivate = false;

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
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void TakeDamage(float damage)
    {
        health.DeductHealth(damage);
        SetHealthText();
        if (health.GetHealth() <= 0) Knockout();
    }

    private void SetHealthText()
    {
        float helth = health.GetHealth();
        helth = helth < 0 ? 0 : helth;
        healthText.text = helth.ToString();
    }

    public override void ShootNuke()
    {
        if (nukeCount > 0)
        {
            base.ShootNuke();
            UpdateImageAlpha("Nuke", false);
            nukeCount--;
        }
    }

    // public int GetGunPowerUpCount()
    // {
    //     return gunpPowerUpCount;
    // }

    // public bool GetGunPowerEnd()
    // {
    //     return _shootPowerGunUpEnd;
    // }

    // public void SetGunPowerEnd(bool gunPowerEnd)
    // {
    //     _shootPowerGunUpEnd = gunPowerEnd;
    // }

    // public override void ShootPowerGunUp()
    // {
    //     if (!_shootPowerGunActivate)
    //     {
    //         _shootPowerGunActivate = true;
    //         gunPowerUpCountDown.SetActive(true);
    //         ClockFillAnimator clockFillAnimator = gunPowerUpCountDown.GetComponentsInChildren<ClockFillAnimator>()[0];
    //         clockFillAnimator.duration = powerGunUpDuration;
    //         clockFillAnimator.PlayAnimateClockFill();
    //         Invoke("ShootPowerGunUpEnd", powerGunUpDuration);
    //     }
    //     base.ShootPowerGunUp();
    // }

    public void ShootPowerGunUpReset()
    {
        _shootPowerGunUpEnd = false;
    }

    public override void ShootPowerGunUp()
    {
        Debug.Log("ShootPowerGunUp called" + gunpPowerUpCount + " " + _shootPowerGunUpEnd);
        if (gunpPowerUpCount > 0 && !_shootPowerGunUpEnd)
        {
            if (!_shootPowerGunActivate)
            {
                _shootPowerGunActivate = true;
                gunPowerUpCountDown.SetActive(true);
                ClockFillAnimator clockFillAnimator = gunPowerUpCountDown.GetComponentsInChildren<ClockFillAnimator>()[0];
                clockFillAnimator.duration = powerGunUpDuration;
                clockFillAnimator.PlayAnimateClockFill();
                Invoke("ShootPowerGunUpEnd", powerGunUpDuration);
            }
            base.ShootPowerGunUp();    
        } 
        
    }

    public void ShootPowerGunUpEnd()
    {
        if (_shootPowerGunActivate)
        {
            gunPowerUpCountDown.SetActive(false);
            UpdateImageAlpha("GunPowerUp", false);
            if (gunpPowerUpCount > 0) gunpPowerUpCount--;
            _shootPowerGunUpEnd = true;
            _shootPowerGunActivate = false;
        }        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyBullet")
        {
            float damage = other.gameObject.GetComponent<Bullet>().GetBulletDamage();  
            TakeDamage(damage);
            Destroy(other.gameObject);
            Debug.Log("Playable Object Hit by Enemy Bullet");
        }

        if(other.gameObject.tag == "CollectableNuke")
        {
            if (nukeCount < 3)
            {
                nukeCount++;
                Image uiImage = nukeBar.transform.Find("Nuke0" + nukeCount).GetComponent<Image>();
                UpdateImageAlpha("Nuke", true);
                Destroy(other.gameObject);
            }
            
        }

        if(other.gameObject.tag == "CollectableGunPowerUp")
        {
            if (gunpPowerUpCount < 3)
            {
                gunpPowerUpCount++;
                UpdateImageAlpha("GunPowerUp", true);
                Destroy(other.gameObject);
            }
            
        }


    }

    private void UpdateImageAlpha(String collectabe, bool activate)
    {
        Image uiImage = null;
        float alpha = activate ? 1f : 0.06f;

        switch (collectabe)
        {
            case "Nuke":
                uiImage = nukeBar.transform.Find("Nuke0" + (nukeCount)).GetComponent<Image>();
                break;
            case "GunPowerUp":
                uiImage = gunpPowerUpBar.transform.Find("GunPowerUp0" + (gunpPowerUpCount)).GetComponent<Image>();
                break;
        }

        if (uiImage) {
            Color tempColor = uiImage.color;
            tempColor.a = alpha;
            uiImage.color = tempColor;
        }
    }

}
