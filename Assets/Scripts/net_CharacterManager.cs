using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net_CharacterManager : MonoBehaviour
{
    public net_Character first;
    public net_Character second;

    public List<GameObject> characters;

    public static net_CharacterManager Singleton;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
