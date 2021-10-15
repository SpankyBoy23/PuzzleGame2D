using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Pause("CharacterSelection");
            Pause("1");
            Pause("2");
            Pause("3");
            Play("MainMenu");
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            Pause("CharacterSelection");
            Pause("1");
            Pause("2");
            Pause("3");
            Play("MainMenu");
        }else if(scene.buildIndex == 1)
        {
            Play("CharacterSelection");
            Pause("MainMenu");
            Pause("1");
            Pause("2");
            Pause("3");
        }else if(scene.buildIndex >= 2)
        {
            Pause("CharacterSelection");
            Pause("MainMenu");
            Pause("1");
            Pause("2");
            Pause("3");
            FindObjectOfType<EnvironmentManager>().SetUpEnviorment();
        }
    }
    public void SceneLaod(int a)
    {
        if (a == 0)
        {
            Pause("CharacterSelection");
            Pause("1");
            Pause("2");
            Pause("3");
            Play("MainMenu");
        }
        else if (a == 1)
        {
            Play("CharacterSelection");
            Pause("MainMenu");
            Pause("1");
            Pause("2");
            Pause("3");
        }
        else if (a >= 2)
        {
            Pause("CharacterSelection");
            Pause("MainMenu");
            Pause("1");
            Pause("2");
            Pause("3");
            FindObjectOfType<EnvironmentManager>().SetUpEnviorment();
        }
    }
    public void Play(string name)
    {

        Debug.Log("Playing: " + name+ " "+ PlayerPrefs.GetInt("Music"));
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        
            if (setting.intance.music)
                s.source.Play();
            if (s == null)
            {
                Debug.LogWarning("Sound:" + name + "not Found!");
                return;
            }
        
    
    }
    public void Pause(string name)
    {
        Debug.Log("Paused");
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        s.source.Pause();
        
    }
    public void PauseAll()
    {
        Pause("CharacterSelection");
        Pause("MainMenu");
        Pause("1");
        Pause("2");
        Pause("3");
    }
}
