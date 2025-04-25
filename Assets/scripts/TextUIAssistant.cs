using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextUIAssistant : MonoBehaviour
{
    [SerializeField] private TMP_Text gameoverMessage;
    [SerializeField] private TextWriter textWriter;
    //private void Awake()
    //{
    //    gameoverMessage.text = "hello!";
    //}
    private void Start()
    {

        textWriter.AddWriter(gameoverMessage, "GAME OVER!", .1f);
    }

}
