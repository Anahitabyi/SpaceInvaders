using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("mainmenu");
    }
}