using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController intance;
     GameObject target;

    public float damageDelay;
    private void Awake()
    {
        intance = this;
        target = GameObject.FindGameObjectWithTag("Player");
    }
    public void setFalse()
    {
        this.Wait(1f, () => { PlayerAnimation.hit = false; });
        Debug.Log(PlayerAnimation.hit);
    }
    public void TakeDamage()
    {
        this.Wait(damageDelay, () => { target.GetComponentInChildren<Animator>().SetTrigger("Hit"); });
    }
}
