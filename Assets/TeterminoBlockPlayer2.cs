using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeterminoBlockPlayer2 : MonoBehaviour
{
    public bool isFallen = false;
    public bool breakNow = false;
    int roundedX, roundedY;
    public TeterminosPlayer2 tp;
    public GameObject test;
    float nextUpdate = 0.00f;

    void Update()
    {
        if (isFallen && breakNow)
        {
            roundedX = Mathf.RoundToInt(transform.position.x);
            roundedY = Mathf.RoundToInt(transform.position.y);
        //    Debug.Log(roundedX+" ,"+roundedY);
     
            // X 0 , y +1
            if ( roundedY < 11)
            {
               Function(0, 1, 1);
             
            }
           

            // X 0 , y -1
           if(roundedY > 0)
           {
                
               Function(0, -1, 2);
            }
            

            // X -1 , y 0
            if (roundedX > 0)
            {        
      
                Function(-1, 0, 3);

            }



            // X +1 , y 0
            if (roundedX < 5)
            {
                 Function(1, 0, 4);

            }

        }
    }
    void Function(int x,int y,int call)
    {
        //Debug.Log(call);
        if (TeterminosPlayer2.grid2[roundedX + x, roundedY + y] != null)
        {
          //  test = TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminosPlayer2>().gameObject;
            if (TeterminosPlayer2.grid2[roundedX + x, roundedY + y].tag == this.gameObject.tag)
            {
                
                if (TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminoBlockPlayer2>() != null)
                    TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
              //  TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<SpriteRenderer>().color = Color.red;
                if (!LogicManager.intance.objectList.Contains(TeterminosPlayer2.grid2[roundedX, roundedY].gameObject))
                {
                    LogicManager.intance.objectList.Add(TeterminosPlayer2.grid2[roundedX, roundedY].gameObject);
                }

               // GetComponent<SpriteRenderer>().color = Color.red;
              //  Destroy(this.gameObject, 2f);
              //  Destroy(TeterminosPlayer2.grid2[roundedX + x, roundedY + y].gameObject, 02);

            }
        }
    }
  
}
