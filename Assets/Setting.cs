using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public static Setting instance;

    public bool sound = true;
    public bool music = true;
  

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
     
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
    }

    public void SetToggleSound()
    {
        sound = !sound;
       

    }
    public void setToggleMusic()
    {
        music = !music;
        
        if (!music)
            MusicManager.instance.PauseAll();
        else
            MusicManager.instance.SceneLaod(SceneManager.GetActiveScene().buildIndex);

    }
}
