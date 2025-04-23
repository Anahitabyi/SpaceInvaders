
using UnityEngine;
public class BunkerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public BunkerHealthUI bunkerHealthUI;

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
        currentHealth -= damage * 10;
        currentHealth = Mathf.Max(0, currentHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            // Remove the bunker
            Debug.Log("The bunker is destroyed.");
            Destroy(gameObject);
            Destroy(bunkerHealthUI.gameObject);
        }
    }
    private void UpdateHealthUI()
    {
        if (bunkerHealthUI != null)
        {
            bunkerHealthUI.UpdateHealth(currentHealth, maxHealth);
            Debug.Log("the bunker health is changed.");
        }
    }
}