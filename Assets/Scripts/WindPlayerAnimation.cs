using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPlayerAnimation : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject prefab;


    private void Start()
    {
       // SpawnOrb();
    }

    public void SpawnOrb()
    {
        
        GameObject a = Instantiate(prefab, spawnPos.position, Quaternion.identity);
      //  Debug.Log("Working"+a.name);
    }
}
