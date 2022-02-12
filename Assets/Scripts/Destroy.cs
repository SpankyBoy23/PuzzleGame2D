using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float delay = 1f;

    private void Start()
    {
        Destroy(gameObject, delay);
    /*    if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main")
        {
            delay = 0.8f;
        }
          */
    }

    private void OnDestroy()
    {

       /* if (effectDone == true) return;
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Main")
        {
            if (distoryEffect == null) return;

            var a = Instantiate(distoryEffect, transform.position, Quaternion.Inverse(transform.rotation));

            target.GetComponent<net_Character>().currentCharacter.GetComponent<RuntimeCharacter>().animator.Play("Hurt");
        }
*/
    }
}
