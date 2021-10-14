using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class net_Character : NetworkBehaviour
{
    public Animator animator;
    public NetworkAnimator networkAnimator;

    [SyncVar]
    public uint playerId;

    public Transform target;
    public bool isWalking;
    public float resetDelay;

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false) return;

        if(isWalking == true) 
        {      
            float distance = Vector3.Distance(transform.position, target.position);

            if(distance <= 4) 
            {
                animator.SetBool("Walk", false);
                isWalking = false;
                CmdAnimation(AnimationType.Attack);
                this.Wait(resetDelay, () => {  transform.position = initialPos; });
            }
            else 
            {
                transform.position = Vector3.Lerp(transform.position, target.position, 10 * Time.deltaTime);
            }
        }
    }

    [Command]
    void CmdAnimation(AnimationType type) 
    {
        RpcShowAnimation(type);
    }

    [ClientRpc]
    void RpcShowAnimation(AnimationType type) 
    {
        if (type == AnimationType.Attack) 
        {
            animator.Play("Attack");
        }
        else
        if(type == AnimationType.Win)
        {
            animator.Play("Win");
        }
        else
        if (type == AnimationType.Lose)
        {
            animator.Play("Lose");
        }
    }

    public void Walk() 
    {
        isWalking = true;
        animator.SetBool("Walk", true);
    }
}

public enum AnimationType 
{
    Idle,
    Attack,
    Win,
    Lose
}