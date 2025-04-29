using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering.Universal;

// Handles player health system, damage taking, and death effects
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public HealthUI healthUI;
    public GameObject PlayerExplosionPrefab;

    private void Start()
    {

        currentHealth = maxHealth;
        UpdateHealthUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if collision is with a projectile
        projectile projectile = collision.GetComponent<projectile>();
        if (projectile)
        {
            TakeDamage(projectile.damage);
            Destroy(projectile.gameObject);
        }
    }
    //apply the damage to the player
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0) {
            //play the player explosion if the players health is 0.
            Instantiate(PlayerExplosionPrefab, transform.position, Quaternion.identity);
            SoundEffectManager.play("playerexplosion");
            //disable all components related to the player
            GetComponent<player>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            //disale the lights
            Light2D[] lights = GetComponentsInChildren<Light2D>();
            foreach (Light2D light in lights)
            {
                light.enabled = false;
            }
            //begin death sequence with delay
            StartCoroutine(DieWithDelay());
        }
    }
    // Coroutine that waits before ending game
    private IEnumerator DieWithDelay()
    {
        //Wait for 1 second before loading game over
        yield return new WaitForSeconds(1f);
        Die();
    }
    // Updates the health display UI
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
        SceneManager.LoadScene("GameOver");
    }
}