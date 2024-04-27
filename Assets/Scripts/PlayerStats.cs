using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int damagePerInterval = 2;
    [SerializeField] private float intervalDuration = 0.3f;
    [SerializeField] private AudioSource audioClip;
    
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
        // if  Health = 0 , shows gameOver Screen
        if (currentHealth <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        if (transform.position.y < - 10.0f)
        {
            FindObjectOfType<GameManager>().GameOver();
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
                audioClip.Play();                
                UpdateHealth(4);
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
