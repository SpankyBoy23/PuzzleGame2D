using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerBlocksPlayer2 : MonoBehaviour
{
    public bool isFallen = false;
    public TeterminosPlayer2 tP;
    int roundedX, roundedY;

    void Update()
    {
        if (isFallen)
        {
             roundedX = Mathf.RoundToInt(transform.position.x);
             roundedY = Mathf.RoundToInt(transform.position.y);

            // X 0 , y +1
            if (roundedY < 11)
            {
                Function(0, 1);
            }


            // X 0 , y -1
            if (roundedY > 0)
            {
                Function(0, -1);
            }


            // X -1 , y 0
            if (roundedX > 0)
            {
                Function(-1, 0);  
            }



            // X +1 , y 0
            if (roundedX < 5)
            {
                Function(1, 0);
            }
        
        }
    }
    void Function(int x,int y)
    {
        if (TeterminosPlayer2.grid2[roundedX+x, roundedY + y] != null)
        {
            if (TeterminosPlayer2.grid2[roundedX+x, roundedY + y].tag == this.tag)
            {
              //  Debug.Log((TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminosPlayer2>().blockColor == tP.blockColor) +":" + tP.blockColor);

                if (TeterminosPlayer2.grid2[roundedX+x, roundedY + y].GetComponent<TeterminoBlockPlayer2>() != null)
                   TeterminosPlayer2.grid2[roundedX +x, roundedY + y].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
                TeterminosPlayer2.grid2[roundedX+x, roundedY + y].GetComponent<SpriteRenderer>().color = Color.red;
                GetComponent<SpriteRenderer>().color = Color.red;
                Destroy(this.gameObject, 2f);
                Destroy(TeterminosPlayer2.grid2[roundedX+x, roundedY + y].gameObject,2f);

            }
        }
    }
}
