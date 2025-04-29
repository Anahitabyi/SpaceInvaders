using UnityEngine;
using TMPro;
public class ScoreUI : MonoBehaviour
{
    public int score;
    public TMP_Text ScoreText;

    //displays the ui for score
    public void UpdateScore(int points)
    {
        if (ScoreText != null)
        {
            score += points;
            ScoreText.text = "Score: " + score;
        }
        else
        {
            Debug.Log("THe text refrence is missing");
        }
    }
}