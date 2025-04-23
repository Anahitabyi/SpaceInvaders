using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            if (i < health)
            {
                
                hearts[i].sprite = fullHearts;
            }
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
