using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    private TMP_Text UIText;
    private string textToWrite;
    private int CharacterIndex;
    private float timePerCharacter;
    private float timer;

    public void AddWriter(TMP_Text UIText, string textToWrite, float timePerCharacter)
    {
        this.UIText = UIText;
        this.textToWrite = textToWrite;
        this.timePerCharacter = timePerCharacter;
        CharacterIndex = 0;
    }
    public void Update()
    {
        if (UIText != null)
        {

            timer -= Time.deltaTime;

            while (timer <= 0)
            {
                // Display next character
                timer += timePerCharacter;
                CharacterIndex++;
                UIText.text = textToWrite.Substring(0, CharacterIndex);

                if (CharacterIndex >= textToWrite.Length)
                {
                    // Entire String has been displayed.
                    UIText = null;
                    return;
                }

            }

        }
    }
}
