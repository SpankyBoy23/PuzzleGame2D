using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public void SetEnemy(int a)
    {
        PlayerPrefs.SetInt("LevelNumber", a);
    }
}
