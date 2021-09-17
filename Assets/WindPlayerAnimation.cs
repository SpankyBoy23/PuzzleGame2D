using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPlayerAnimation : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;
    public float speed;
    public Animator anim;
    GameObject a;
    private void Start()
    {
       // SpawnOrb();
    }
    public void SpawnOrb()
    {
        Debug.Log("Wokringggg");
        a = Instantiate(prefab, spawnPoint.position,Quaternion.identity);
        a.transform.parent = null;
        
    }
    private void Update()
    {
        if(a)
        a.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
    }
    public void ChargeAttack()
    {

    }
}
