using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBotBehaviour : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {

        //  SpawnOrb();
    }

    public void Attack()
    {
       // Debug.Log("ItCalled");
        if (!LogicManager.intance.finalMove)
            anim.Play("Attack#1");
        else
            anim.Play("ChargeAttack");
    }
}
