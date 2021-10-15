using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeCharacter : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    public net_Character.CharacterType cType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;
        transform.position = target.position;
        
    }
}
