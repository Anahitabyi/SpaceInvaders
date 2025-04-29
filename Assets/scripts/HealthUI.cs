using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Handles visual representation of player health using heart icons
public class HealthUI : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite fullHearts;
    public Sprite emptyHearts;

    public Image[] hearts;

    public void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            //If current index is less than health value, show full heart
            if (i < health)
            {
                
                hearts[i].sprite = fullHearts;
            }
            //otherwise show empty heart
            else
            {
                hearts[i].sprite = emptyHearts;
            }
            //if (i < maxHealth)
            //{
            //    hearts[i].enabled = true;
            //}
            //else
            //{
            //    hearts[i].enabled = false;
            //}

        }
    }
}
