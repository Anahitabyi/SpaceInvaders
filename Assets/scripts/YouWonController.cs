using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class YouWonController : MonoBehaviour
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