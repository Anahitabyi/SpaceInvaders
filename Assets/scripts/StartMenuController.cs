using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public void OnStartClick()
    {
        
        if (MusicManager.Instance != null)
        {
            StartCoroutine(MusicManager.Instance.FadeOutMusic());
        }

        Invoke(nameof(LoadGameScene), 1.1f);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("spaceInvaders");
    }
}