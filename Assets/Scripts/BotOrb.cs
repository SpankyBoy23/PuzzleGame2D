using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotOrb : MonoBehaviour
{
    public GameObject target;
    public Transform targetTransform;
    public GameObject distoryEffect;
    public float hitRange;
    public float speed;
    public Animator animatorTarget;
    public float damageDelay;
    bool effect = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        targetTransform = target.transform;
        animatorTarget = target.GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
       
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        // Debug.Log(Vector2.Distance(transform.position, target.transform.position));
        //Debug.Log(Vector2.Distance(transform.position, targetTransform.position));
        if (Vector2.Distance(transform.position, targetTransform.position) < hitRange)
        {
            if (!effect)
            {
                var a = Instantiate(distoryEffect, transform.position, Quaternion.Inverse(transform.rotation));
                
                effect = true;
            }
            if (!LogicManager.intance.finalMove)
                animatorTarget.SetTrigger("Hit");
            else
                animatorTarget.SetBool("Die",true);
            //Destroy(a, 1f);
            Destroy(gameObject);
            //animatorTarget.SetTrigger("Hit");
        }
    }
}
