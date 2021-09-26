using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPlayerBehaviour : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject prefab;
   
    private void Start()
    {
        
      //  SpawnOrb();
    }

    public void SpawnOrb()
    {
        Debug.Log("Working");
        GameObject a = Instantiate(prefab, spawnPos.position, Quaternion.identity);
       
    }

}
