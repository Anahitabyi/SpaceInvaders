using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        projectile projectile = collision.GetComponent<projectile>();
        if (projectile)
        {
            TakeDamage(projectile.damage);
            Destroy(projectile.gameObject);
        }
    }
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0) {
            Die();
        }
    }
    private void UpdateHealthUI()
    {
        if (healthUI != null)
        {
            healthUI.health = currentHealth;
            healthUI.maxHealth = maxHealth;
        }
    }
    private void Die()
    {
        Debug.Log("player is dead");
    }
}