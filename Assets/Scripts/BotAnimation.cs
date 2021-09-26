using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimation : StateMachineBehaviour
{
    public float speed = 0.25f;
    public Transform target;
    public Transform tranform;
    public static bool hit = false;
    //  public Vector2 initialPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        tranform = GameObject.FindGameObjectWithTag("Bot").GetComponentInParent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        /* Vector2 targetPos = new Vector2(target.position.x, rb.position.y);
         if (!hit)
         {
             Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
             rb.MovePosition(newPos);
         }

         else if (hit && rb.position.x > 0)
         {
             Debug.Log("Working");
             Vector2 perviousPosition = Vector2.MoveTowards(rb.position, new Vector2(0, animator.GetComponent<Transform>().position.y), -speed * Time.fixedDeltaTime);
             rb.MovePosition(perviousPosition);
         }


         if (rb.position.x < 7)
         {
             animator.SetBool("Walk", false);
         }



         if (Vector2.Distance(target.position, rb.position) <= attackRange && !hit)
         {
             //  Debug.Log(Vector2.Distance(target.position, rb.position));
             AnimationController.intance.TakeDamage();
             animator.SetTrigger("Attack");
             hit = true;
         }*/
        tranform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*if (rb.position.x < 0)
        {
            animator.SetBool("Walk", false);
        }*/
    }
}
