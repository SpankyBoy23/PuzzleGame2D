using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerBlocksPlayer2 : MonoBehaviour
{
    public bool isFallen = false;

    void Update()
    {
        if (isFallen)
        {
            int roundedX = Mathf.RoundToInt(transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.position.y);

            // X +1 , y 0
            if (roundedY < 11)
            {
                Debug.Log("Its Here +1y");
                if (TeterminosPlayer2.grid2[roundedX, roundedY + 1] != null)
                {
                   
                    if (TeterminosPlayer2.grid2[roundedX, roundedY + 1].GetComponent<TeterminosPlayer2>().blockColor == GetComponent<TeterminosPlayer2>().blockColor)
                    {

                        if (TeterminosPlayer2.grid2[roundedX, roundedY + 1].GetComponent<TeterminoBlockPlayer2>() != null)
                            TeterminosPlayer2.grid2[roundedX , roundedY + 1].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
                        TeterminosPlayer2.grid2[roundedX, roundedY + 1].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(TeterminosPlayer2.grid2[roundedX, roundedY + 1].gameObject, 0.5f);
                        
                    }
                    

                }
                else
                {
                   
                }
            }


            // X 0 , y -1
            if (roundedY > 0)
            {
                if (TeterminosPlayer2.grid2[roundedX, roundedY - 1] != null)
                {

                    if (TeterminosPlayer2.grid2[roundedX, roundedY - 1].GetComponent<TeterminosPlayer2>().blockColor == GetComponent<TeterminosPlayer2>().blockColor)
                    {
                        //  Debug.Log(GetComponent<TeterminoBlockPlayer2>() + "Its Here -1y");
                        if (TeterminosPlayer2.grid2[roundedX, roundedY - 1].GetComponent<TeterminoBlockPlayer2>() != null)
                            TeterminosPlayer2.grid2[roundedX, roundedY - 1].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
                        TeterminosPlayer2.grid2[roundedX, roundedY - 1].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(TeterminosPlayer2.grid2[roundedX, roundedY - 1].gameObject, 0.5f);
                        
                    }
                    
                }
                else
                {
                   
                }
            }


            // X -1 , y 0
            if (roundedX > 0)
            {
                if (TeterminosPlayer2.grid2[roundedX - 1, roundedY] != null)
                {
                 
                    if (TeterminosPlayer2.grid2[roundedX - 1, roundedY].GetComponent<TeterminosPlayer2>().blockColor == GetComponent<TeterminosPlayer2>().blockColor)
                    {
                        //  Debug.Log(GetComponent<TeterminoBlockPlayer2>() + "Its Here -1x");
                        if (TeterminosPlayer2.grid2[roundedX-1, roundedY].GetComponent<TeterminoBlockPlayer2>() != null)
                            TeterminosPlayer2.grid2[roundedX - 1, roundedY ].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
                        TeterminosPlayer2.grid2[roundedX - 1, roundedY].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(TeterminosPlayer2.grid2[roundedX-1, roundedY].gameObject, 0.5f);
                    }

                }
                else
                {
                    
                }
            }



            // X +1 , y 0
            if (roundedX < 5)
            {
                if (TeterminosPlayer2.grid2[roundedX + 1, roundedY] != null)
                {

                    if (TeterminosPlayer2.grid2[roundedX + 1, roundedY].GetComponent<TeterminosPlayer2>().blockColor == GetComponent<TeterminosPlayer2>().blockColor)
                    {
                        // Debug.Log(GetComponent<TeterminoBlockPlayer2>() + "Its Here +1x");
                        if (TeterminosPlayer2.grid2[roundedX + 1, roundedY].GetComponent<TeterminoBlockPlayer2>() != null)
                            TeterminosPlayer2.grid2[roundedX + 1, roundedY].GetComponent<TeterminoBlockPlayer2>().breakNow = true;
                        TeterminosPlayer2.grid2[roundedX + 1, roundedY].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(TeterminosPlayer2.grid2[roundedX+1, roundedY].gameObject, 0.5f);

                    }


                }
                else
                {
                   
                }

            }

            // X 0 , y +1
            /*   if (TeterminosPlayer2.grid2[roundedX , roundedY + 1] != null)
               {
                   print(TeterminosPlayer2.grid2[roundedX, roundedY + 1].GetComponent<TeterminosPlayer2>().blockColor);
               }

               // X 0 , y -1
               if (TeterminosPlayer2.grid2[roundedX, roundedY - 1] != null)
               {
                   print(TeterminosPlayer2.grid2[roundedX, roundedY - 1].GetComponent<TeterminosPlayer2>().blockColor);
               }

               // X +1 , y 0
               if (TeterminosPlayer2.grid2[roundedX +1, roundedY] != null)
               {
                   print(TeterminosPlayer2.grid2[roundedX +1, roundedY].GetComponent<TeterminosPlayer2>().blockColor);
               }

               // X -1 , y 0
               if (TeterminosPlayer2.grid2[roundedX - 1, roundedY] != null)
               {
                   print(TeterminosPlayer2.grid2[roundedX -1 , roundedY].GetComponent<TeterminosPlayer2>().blockColor);
               }*/
        
        }
    }
}
