using System.Runtime.CompilerServices;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static AudioSource audioSource; 
    private static SoundEffectManager instance;
    private static SoundEffectLibrary soundEffectLibrary;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void play(string Soundname)
    {
        AudioClip audioClip = SoundEffectLibrary.GetAudioClip(Soundname);
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}