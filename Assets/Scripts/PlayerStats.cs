using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public HealthBar healthBar;
    //private bool damage = false;
    private float damageSeconds = 5;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        damageSeconds -= Time.deltaTime;
        if (damageSeconds <= 0 && currentHealth > 0)
        {
            TakeDamage(1);
            damageSeconds = 5;
        }        
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Potion")
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
