using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehviour : MonoBehaviour
{
    public Animator animator;

    private float distance;
    public float attackRange = 8;
    public bool attacked = false;
    [SerializeField] Vector3 intialPosition;
   public GameObject target;


    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Bot");
        intialPosition = transform.position;
    }
    public void Walk()
    {

        animator.SetBool("Walk", true);
    }
    private void Update()
    {
        Function();
    }
    private void Function()
    {
        
        if((transform.position - target.transform.position).magnitude < attackRange && !attacked)
        {
            attacked = true;
            
            animator.SetBool("Walk", false);
            animator.SetTrigger("Attack");
            this.Wait(1f,()=>{ transform.position = intialPosition;attacked = false; });
           // animator.ResetTrigger("Attack");
  
            
        }
    }
  

}
