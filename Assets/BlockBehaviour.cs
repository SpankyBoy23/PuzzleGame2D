using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockBehaviour : MonoBehaviour
{
    public bool breakAble = false;

   
    private void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other);
        if(other.tag == this.gameObject.tag)
        if(other.GetComponent<BlockBehaviour>() != null)
        {
            if (other.GetComponent<BlockBehaviour>().breakAble)
            {
                 this.breakAble = true;
                 Destroy(gameObject, 2);
           //      RowDown();
            }
        }
    }
    public void RowDown()
    {
            for (int i = 0; i < BlockMovement.height; i++)
            {
            for (int j = 0; j < BlockMovement.width; j++)
            {
                if (!BlockMovement.grid[j, i - 1].gameObject.GetComponent<BlockBehaviour>().breakAble)

                    if (BlockMovement.grid[j, i] != null)
                {
                    BlockMovement.grid[j, i - 1] = BlockMovement.grid[j, i];
                    BlockMovement.grid[j, i] = null;
                   
                        BlockMovement.grid[j, i - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }


}
