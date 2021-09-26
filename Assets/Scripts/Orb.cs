using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    public Transform target;
    public GameObject distoryEffect;
    public float hitRange;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Bot").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
       // Debug.Log(Vector2.Distance(transform.position, target.position));

        if (Vector2.Distance(transform.position, target.position) < hitRange)
        {
           var a = Instantiate(distoryEffect, transform);
            //Destroy(a, 1f);
            Destroy(gameObject,0.2f);
        }
    }
}
