using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net_Orb : MonoBehaviour
{
    public Transform target;
    public float hitRange;
    bool effect = false;
    public GameObject distoryEffect;
    public bool mainScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainScene == false) return;
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position, 4 * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) <= 0.4f)
        {
            if (!effect)
            {
                var a = Instantiate(distoryEffect, transform.position, Quaternion.Inverse(transform.rotation));

                effect = true;
            }
            target.GetComponent<net_Character>().currentCharacter.GetComponent<RuntimeCharacter>().animator.Play("Hurt");
            //Destroy(a, 1f);
            Destroy(gameObject);
            //animatorTarget.SetTrigger("Hit");
        }
    }
}
