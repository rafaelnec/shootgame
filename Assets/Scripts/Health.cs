using UnityEngine;

public class Health
{
    private float maxHealth;
    private float currentHealth;
    private float healthRegenRate;

    public Health() { }
    public Health(float _maxHealth)
    {
        this.maxHealth = _maxHealth;
    }

    public Health(float _currentHealth, float _healthRegenRate, float _maxHealth = 100)
    {
        this.maxHealth = _maxHealth;
        this.currentHealth = _currentHealth;
        this.healthRegenRate = _healthRegenRate;
    }

    public void AddHealth(float value)
    {
        currentHealth += value;
    }

    public void DeductHealth(float value)
    {
        currentHealth -= value;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}