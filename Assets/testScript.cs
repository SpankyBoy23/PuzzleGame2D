using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testScript : MonoBehaviour
{
    public GameObject Text;
    private void Start()
    {
        foreach (Transform transform in TeterminosPlayer2.grid2)
        {
           GameObject a=  Instantiate(Text, transform.position, Quaternion.identity);
            
        }
       
    }
}
