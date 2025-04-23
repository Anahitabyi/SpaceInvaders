using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

    public void OnStartClick()
    {
        SceneManager.LoadScene("mainmenu");
    }

}

