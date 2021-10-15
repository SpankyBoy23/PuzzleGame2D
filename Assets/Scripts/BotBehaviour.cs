using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    public Animator animator;

    private float distance;
    public float attackRange = -2;
    bool attacked = false;
    Vector3 intialPosition;
     GameObject target;
    public bool isLucas;

    private void Start()
    {
        //Walk();
        target = GameObject.FindGameObjectWithTag("Player");
        intialPosition = transform.position;
    }
    public void Walk()
    {
        Debug.Log("Its worked");
        if (!isLucas && !LogicManager.intance.finalMove)
        {
            Debug.Log("First Works");
            animator.SetBool("Walk", true);
        }else if(isLucas && !LogicManager.intance.finalMove)
        {
            Debug.Log("2 Works");
            animator.SetBool("Walk", true);
        }
         else if (isLucas && LogicManager.intance.finalMove)
        {
            Debug.Log("3 Works");
            animator.Play("ChargeAttack");
        }
            

            
    }
    private void Update()
    {
      //  Debug.Log(transform.position.x - target.transform.position.x);
        Function();
    }
    private void Function()
    {

        if (transform.position.x < attackRange && !attacked)
        {
            attacked = true;
            if (!LogicManager.intance.finalMove)
            {
                animator.Play("Attack#1");
            }
            else
            {
                animator.Play("ChargeAttack");
            }

            this.Wait(0.1f, () => { animator.SetBool("Walk", false); });
            this.Wait(1f, () => {  transform.position = intialPosition; attacked = false; });

        }
    }
}
