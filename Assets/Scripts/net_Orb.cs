using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net_Orb : MonoBehaviour
{
    public Transform target;

    bool effect = false;
    public GameObject distoryEffect;
    public bool mainScene;
    [SerializeField] float breakDis = 0.5f;
    public float speed = 4;
    public bool test; //e

    bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main")
        {
            GetComponent<Orb>().enabled = false;
        }

        StartCoroutine(Internal_Start());
      
    }

    IEnumerator Internal_Start() 
    {
        yield return new WaitForSeconds(0.2f);

        start = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false) return;

        if (target == null)
        {
            Destroy(gameObject);
        }
        if (mainScene == false) return;

        Vector3 targetPos = target.transform.position;
        targetPos.y = 7f;
        targetPos.z = 0;
        transform.position = Vector3.Lerp(transform.position,targetPos, speed * Time.deltaTime);
    //    Debug.Log(Vector2.Distance(transform.position, target.position));
        if (Vector2.Distance(transform.position, target.position) <= breakDis)
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
