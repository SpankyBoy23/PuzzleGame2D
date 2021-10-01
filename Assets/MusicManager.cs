using System;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public Sound[] sounds;
    public static MusicManager instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
    }
    private void Start()
    {
       // Play("Theme");
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not Found!");
            return;
        }
    }
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Pause();
    }
}
