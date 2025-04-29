using System.Collections.Generic;
using UnityEngine;

// Centralized library for managing and accessing sound effects across the game
public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffect[] soundEffects;
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    private static SoundEffectLibrary instance;

    private void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            InitializeDictionary();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionary()
    {
        //add each sound effect to the library
        soundDictionary = new Dictionary<string, AudioClip>();
        foreach (SoundEffect soundEffect in soundEffects)
        {
            if (!soundDictionary.ContainsKey(soundEffect.name))
            {
                soundDictionary.Add(soundEffect.name, soundEffect.audioClip);
            }
        }
    }


    public static AudioClip GetAudioClip(string name)
    {
        if (instance != null && instance.soundDictionary != null &&
            instance.soundDictionary.TryGetValue(name, out AudioClip clip))
        {
            return clip;
        }
        Debug.LogWarning($"Sound clip {name} not found or library not initialized");
        return null;
    }
}
[System.Serializable]
public struct SoundEffect
{
    public string name;
    public AudioClip audioClip;
}