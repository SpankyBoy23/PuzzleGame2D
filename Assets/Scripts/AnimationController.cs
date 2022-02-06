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
        if(target != null)
        animatorTarget = target.GetComponentInChildren<Animator>();
    }

    public void HitEnemy()
    {
        Debug.Log(state+" , "+transform.parent.tag);
        if (LogicManager.intance.finalMove)
        { 
            if (state == DeathState.player && transform.parent.tag == "Player")
            {
                Debug.Log("ss");
                 animatorTarget.SetTrigger("Die");
            }
            else if (state == DeathState.bot && transform.parent.tag == "Bot")
            {
                // if (!LogicManager.intance.finalMove) animatorTarget.SetTrigger("Hit");
                Debug.Log("ss2");
                animatorTarget.SetTrigger("Die");
            }
        }
        else
        {
                animatorTarget.SetTrigger("Hit");
        }

    }
}
