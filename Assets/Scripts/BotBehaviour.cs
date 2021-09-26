using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    public Animator animator;

    private float distance;
    public float attackRange = 8;
    bool attacked = false;
    Vector3 intialPosition;
     GameObject target;


    private void Start()
    {
        //Walk();
        target = GameObject.FindGameObjectWithTag("Player");
        intialPosition = transform.position;
    }
    public void Walk()
    {
        
        animator.SetBool("Walk", true);
    }
    private void Update()
    {
      //  Debug.Log(transform.position.x - target.transform.position.x);
        Function();
    }
    private void Function()
    {

        if ((transform.position - target.transform.position).magnitude < attackRange && !attacked)
        {
            attacked = true;
            animator.SetTrigger("Attack");

            this.Wait(0.1f, () => { animator.SetBool("Walk", false); });
            this.Wait(1f, () => {  transform.position = intialPosition; attacked = false; });
            

        }
    }
}
