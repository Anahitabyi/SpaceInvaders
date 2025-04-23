using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BunkerHealthUI : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public TMP_Text healthText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
        else
        {
            Debug.LogError("HealthText reference is missing in BunkerHealthUI!");
        }
    }
}