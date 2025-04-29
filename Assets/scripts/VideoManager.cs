using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    private VideoPlayer _player;
    //plays the video of the main menu
    private void Start()
    {
        _player = GetComponent<VideoPlayer>();
        _player.isLooping = true;
        _player.Play();
    }
}