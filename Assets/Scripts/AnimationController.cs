using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathState { player,bot}
public class AnimationController : MonoBehaviour
{
    public static AnimationController intance;
    public static DeathState state;
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
        if (LogicManager.intance.finalMove)
        { 
            if (state == DeathState.player && gameObject.tag == "Bot")
            {
          //  if (!LogicManager.intance.finalMove) animatorTarget.SetTrigger("Hit");
                 animatorTarget.SetTrigger("Die");
            }
            else if (state == DeathState.bot && gameObject.tag == "Player")
            {
           // if (!LogicManager.intance.finalMove) animatorTarget.SetTrigger("Hit");
                 animatorTarget.SetTrigger("Die");
            }
        }
        else
        {
                animatorTarget.SetTrigger("Hit");
        }

    }
}
