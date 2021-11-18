using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setting : MonoBehaviour
{
    public static setting intance;

    public bool sound = true;
    public bool music = true;

    private void Awake()
    {
        intance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSound()
    {
        sound = !sound;
    }
    public void SetMusic()
    {
        music = !music;
    }
}
