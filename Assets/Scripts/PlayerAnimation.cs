using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : StateMachineBehaviour
{
    public float speed = 0.25f;
    public Transform target;
    public Transform tranform;
    public static bool hit = false;

    //  public Vector2 initialPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        target = GameObject.FindGameObjectWithTag("Bot").transform;
        tranform = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<Transform>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*  Vector2 targetPos = new Vector2(target.position.x, rb.position.y);
         // Debug.Log(hit);
          if (!hit)
          {
              Debug.Log("Movinng Forward");
              Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, speed * Time.fixedDeltaTime);
              rb.MovePosition(newPos);
          }

          else if (hit && rb.position.x > 2.5f)
          {
              Debug.Log("Moving forward");
           //   Vector2 perviousPosition = Vector2.MoveTowards(rb.position,new Vector2(0,animator.GetComponent<Transform>().position.y), -speed * Time.fixedDeltaTime);
             // rb.MovePosition(perviousPosition);

            // rb.position = new Vector2(0, animator.GetComponent<Transform>().position.y);
          }

        // Debug.Log("Moving Towards bot");
         if (rb.position.x < 0)
         {
            // animator.SetBool("Walk", false);
         }



          if(Vector2.Distance(target.position, rb.position)<= attackRange)
          {
              //  Debug.Log(Vector2.Distance(target.position, rb.position));
              AnimationController.intance.TakeDamage();
              animator.SetBool("Walk", false);
          }*/
        //Vector2 newPos = Vector2.MoveTowards(tranform.position, target.transform.position, speed * Time.deltaTime);
        tranform.Translate(Vector3.right * speed *Time.fixedDeltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
