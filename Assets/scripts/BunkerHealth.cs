
using UnityEngine;
// Manages health system for defensive bunkers in the game
public class BunkerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    //create a ui object for bunker
    public BunkerHealthUI bunkerHealthUI;

    private void Start()
    {
        currentHealth = maxHealth;//start with maximum health for the bunkers
        UpdateHealthUI();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        projectile projectile = collision.GetComponent<projectile>();

        if (projectile)
        {
            TakeDamage(projectile.damage);// Apply damage to bunker

            Destroy(projectile.gameObject);
        }
    }
    private void TakeDamage(int damage)
    {
        // Reduce health (10x damage multiplier for significant impact)
        currentHealth -= damage * 10;
        //ensure the health doesnt go below zero
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
    //Updates the bunker's health display
    private void UpdateHealthUI()
    {
        if (bunkerHealthUI != null)
        {
            bunkerHealthUI.UpdateHealth(currentHealth, maxHealth);
            Debug.Log("the bunker health is changed.");
        }
    }
}