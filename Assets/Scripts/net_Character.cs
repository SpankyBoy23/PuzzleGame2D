using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using FirstGearGames.Mirrors.Assets.FlexNetworkAnimators;

public class net_Character : NetworkBehaviour
{
    public Animator animator;
    public FlexNetworkAnimator networkAnimator;

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
        if (GameManager.singleton.decide == true) return;

        if(isWalking == true) 
        {      
            float distance = Vector3.Distance(transform.position, target.position);

            if(distance <= 2) 
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

        if (target.GetComponentInChildren<Animator>()) 
        {
            target.GetComponentInChildren<Animator>().Play("Hurt");
        }

      
    }

    public void Walk() 
    {
        isWalking = true;
        animator.SetBool("Walk", true);
    }

    public void Spawn(int index , Transform t) 
    {
        List<GameObject> chars = net_CharacterManager.Singleton.characters;
        GameObject go = Instantiate(chars[index], transform.position, transform.rotation);
        RuntimeCharacter rc = go.GetComponent<RuntimeCharacter>();
        rc.target = transform;

        animator = rc.animator;
        networkAnimator.SetAnimator(rc.animator);
       
    }
}

public enum AnimationType 
{
    Idle,
    Attack,
    Win,
    Lose
}