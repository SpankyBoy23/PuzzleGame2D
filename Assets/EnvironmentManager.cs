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

        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main") return;
      
            int a = Random.Range(0, environments.Length);
        if(a < 2)
        {
            MusicManager.instance.Play("1");
        }else if(a == 2)
        {
            MusicManager.instance.Play("2");
        }
        else
        {
            MusicManager.instance.Play("3");
        }
          
            environments[a].SetActive(true);
   
    }
}
