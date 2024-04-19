using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int damagePerInterval = 1;
    [SerializeField] private float intervalDuration = 0.3f;
    
    private float _interval;
    
    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;

    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        _interval = intervalDuration;
    }
    
    void Update()
    {
        _interval -= Time.deltaTime;
        if (_interval <= 0 && currentHealth > 0)
        {
            TakeDamage(damagePerInterval);
            _interval = intervalDuration;
        }        
    }
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Potion"))
        {
            if (currentHealth < maxHealth && currentHealth > 0)
            {
                UpdateHealth(1);
                Destroy(other.gameObject);
            }
        }
    }
    
    void UpdateHealth(int health)
    {
        currentHealth += health;
        healthBar.SetHealth(currentHealth);
    }
}
