using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPlayerBehaviour : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        
      //  SpawnOrb();
    }

    public void Attack()
    {
        if (!LogicManager.intance.finalMove)
            anim.SetTrigger("Attack");
        else
            anim.SetTrigger("ChargeAttack");
    }
  

}
