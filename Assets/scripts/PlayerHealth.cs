using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering.Universal;

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
            Instantiate(PlayerExplosionPrefab, transform.position, Quaternion.identity);
            SoundEffectManager.play("playerexplosion");
            GetComponent<player>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            Light2D[] lights = GetComponentsInChildren<Light2D>();
            foreach (Light2D light in lights)
            {
                light.enabled = false;
            }

            StartCoroutine(DieWithDelay());
        }
    }
    private IEnumerator DieWithDelay()
    {
        //Wait for 1 second before loading game over
        yield return new WaitForSeconds(1f);
        Die();
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
        SceneManager.LoadScene("GameOver");
    }
}