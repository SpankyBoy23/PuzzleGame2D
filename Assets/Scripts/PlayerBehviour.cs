using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehviour : MonoBehaviour
{
    public Animator animator;

    private float distance;
    public float attackRange = 8;
    public float resetDelay = 1;
    public bool attacked = false;
    [SerializeField] Vector3 intialPosition;
   public GameObject target;
    public bool isLucas = false;
    public Vector2 pos;

    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Bot");
        intialPosition = transform.position;
    }
    public void Walk()
    {
        Debug.Log("SS2343");

        if (!LogicManager.intance.finalMove)
        {
            animator.SetBool("Walk", true);
        }
        if (isLucas && LogicManager.intance.finalMove)
            animator.Play("ChargeAttack");
        else if (!isLucas && LogicManager.intance.finalMove)
        {
            animator.SetBool("Walk", true);
        }
            
    }
    private void Update()
    {
        //Function();
        if ((transform.position.x >= 3) && !attacked)
        {
            attacked = true;
            //  animator.SetTrigger("Attack");
            // Time.timeScale = 0;

            animator.SetBool("Walk", false);
            if (!LogicManager.intance.finalMove)
            {
                
                animator.Play("Attack#1");
            }
            else
            {

                animator.Play("ChargeAttack");

            }

            this.Wait(resetDelay, () => { transform.position = intialPosition; attacked = false; });

            // animator.ResetTrigger("Attack");


        }
    }
    private void Function()
    {
        pos = transform.position - target.transform.position;
   //     Debug.Log("<color=red>distance: </color> "+Mathf.Pow(pos.x+pos.y,2));
      //  Debug.Log(Vector2.Distance(transform.position,target.transform.position));
        if ((transform.position.x >= attackRange))
        {
            attacked = true;
            //  animator.SetTrigger("Attack");
            // Time.timeScale = 0;

            animator.SetBool("Walk", false);
            if (!LogicManager.intance.finalMove)
            {

                animator.Play("Attack#1");
            }
            else
            {
                animator.Play("ChargeAttack");
            }
            
            this.Wait(resetDelay,()=>{ transform.position = intialPosition;attacked = false; });
            
            // animator.ResetTrigger("Attack");


        }
    }

   
}
