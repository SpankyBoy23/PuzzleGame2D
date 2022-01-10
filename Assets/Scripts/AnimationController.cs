using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController intance;
    public GameObject target;
    [SerializeField] Animator animatorTarget;
    public float damageDelay;
    private void Awake()
    {
        intance = this;
      
       // target = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        if (transform.parent.tag == "Bot")
        {
            target = GameObject.FindGameObjectWithTag("Player");
         //   Debug.Log("Finding Player");
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Bot");
          //  Debug.Log("Bot");
            // speed = speed;
        }
        animatorTarget = target.GetComponentInChildren<Animator>();
    }

    public void HitEnemy()
    {
        if (!LogicManager.intance.finalMove) animatorTarget.SetTrigger("Hit");
        else animatorTarget.SetTrigger("Die");
    }
}
