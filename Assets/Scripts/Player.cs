using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Player : PlayableObject
{

    private static Player _instance;

    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private GameLevelSettings gameLevelSettings;    

    public TextMeshProUGUI healthText;
    
    public GameObject nukeBar;
    public int nukeCount = 0;
    public GameObject gunpPowerUpBar;   
    public int gunpPowerUpCount = 0;
    public GameObject gunPowerUpCountDown;
    public float powerGunUpDuration = 5f;
    public bool _shootPowerGunUpEnd = false;
    public bool _shootPowerGunActivate = false;   

    public UnityEvent<int> PlayerScored;
    public UnityEvent PlayerKnockedOut;
    public UnityEvent<int> NukeCollected;
    public UnityEvent<int> NukeUsed;

    private GameController _gameController;
    private EndScoreManager _endScoreManager;
    private SceneChanger _sceneChanger;

    void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {
        _gameController = FindFirstObjectByType<GameController>();
        _endScoreManager = FindFirstObjectByType<EndScoreManager>();
        _sceneChanger = FindFirstObjectByType<SceneChanger>();
        
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
        //update score within score manager
        _endScoreManager.UpdateScores(_gameController.GetScore());
        //change to game over scene
        _sceneChanger.LoadScene("GameOverScene");

        //previous code
        //gameOverScreen.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Heal(float healAmount)
    {
        healAmount = health.GetHealth() + healAmount > 100 ? 100 - health.GetHealth() : healAmount;
        health.AddHealth(healAmount);
        SetHealthText();
    }

    public void TakeDamage(float damage)
    {
        health.DeductHealth(damage);
        SetHealthText();
        if (health.GetHealth() <= 0) Knockout();
    }

    public void Score(int score)
    {
        PlayerScored.Invoke(score);
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
            NukeUsed.Invoke(nukeCount);
            nukeCount--;
        }
    }

    public void ShootPowerGunUpReset()
    {
        _shootPowerGunUpEnd = false;
    }

    public override void ShootPowerGunUp()
    {
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
        }

        if(other.gameObject.tag == "CollectableNuke")
        {
            if (nukeCount < 3)
            {
                nukeCount++;
                NukeCollected.Invoke(nukeCount);
                // Image uiImage = nukeBar.transform.Find("Nuke0" + nukeCount).GetComponent<Image>();
                // UpdateImageAlpha("Nuke", true);
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

    public void UpdatePlayerSprite(int gameLevel)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer && gameLevel < gameLevelSettings.GameData.Count)
        {
            spriteRenderer.sprite = gameLevelSettings.GameData[gameLevel-1].PlayerSprite;
        }
    }

}
