
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void OnStartClick()
    {
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayBackgroundMusic(true);
            StartCoroutine(LoadMenuAfterFade());
        }
        else
        {
            SceneManager.LoadScene("mainmenu");
        }
    }

    private IEnumerator LoadMenuAfterFade()
    {
        yield return MusicManager.Instance.FadeInMusic();
        SceneManager.LoadScene("mainmenu");
    }
}