using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] environments;

    private void Awake()
    {
        foreach (GameObject environment in environments)
        {
            environment.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Level") == 8)
        {
           
            environments[5].SetActive(true);
        }
        else
        {
            environments[Random.Range(0, environments.Length - 1)].SetActive(true);
        }
    }
}
