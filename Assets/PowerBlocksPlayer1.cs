using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBlocksPlayer1 : MonoBehaviour
{
    public bool isFallen = false;


    void Update()
    {
        if (isFallen)
        {
            int roundedX = Mathf.RoundToInt(transform.position.x);
            int roundedY = Mathf.RoundToInt(transform.position.y);
            //    Debug.Log(roundedX+" ,"+roundedY);

            // X +1 , y +1
            if (roundedY < 11)
            {

                if (Teterminos.grid[roundedX, roundedY + 1] != null)
                {

                    if (Teterminos.grid[roundedX, roundedY + 1].GetComponent<Teterminos>().blockColor == GetComponent<Teterminos>().blockColor)
                    {
                        if (Teterminos.grid[roundedX, roundedY + 1].GetComponent<TeterminoBlockPlayer1>() != null)
                            Teterminos.grid[roundedX, roundedY + 1].GetComponent<TeterminoBlockPlayer1>().breakNow = true;
                        Teterminos.grid[roundedX, roundedY + 1].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Teterminos.grid[roundedX, roundedY] = null;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(Teterminos.grid[roundedX, roundedY + 1].gameObject, 0.5f);
                        
                    //    Teterminos.grid[roundedX, roundedY + 1] = null;
                    }
                }
                else
                {

                }
            }


            // X -1 , y -1
            if (roundedY > 0)
            {
                if (Teterminos.grid[roundedX, roundedY - 1] != null)
                {

                    if (Teterminos.grid[roundedX, roundedY - 1].GetComponent<Teterminos>().blockColor == GetComponent<Teterminos>().blockColor)
                    {
                        if (Teterminos.grid[roundedX, roundedY - 1].GetComponent<TeterminoBlockPlayer1>() != null)
                            Teterminos.grid[roundedX, roundedY - 1].GetComponent<TeterminoBlockPlayer1>().breakNow = true;
                        Teterminos.grid[roundedX, roundedY - 1].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Teterminos.grid[roundedX, roundedY] = null;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(Teterminos.grid[roundedX, roundedY - 1].gameObject, 0.5f);
                       
                      //  Teterminos.grid[roundedX, roundedY - 1] = null;

                    }
                }
                else
                {
                    transform.position += new Vector3(0, 1, 0);
                }
            }


            // X -1 , y +1
            if (roundedX > 0)
            {
                if (Teterminos.grid[roundedX - 1, roundedY] != null)
                {

                    if (Teterminos.grid[roundedX - 1, roundedY].GetComponent<Teterminos>().blockColor == GetComponent<Teterminos>().blockColor)
                    {
                        if (Teterminos.grid[roundedX - 1, roundedY].GetComponent<TeterminoBlockPlayer1>() != null)
                            Teterminos.grid[roundedX - 1, roundedY].GetComponent<TeterminoBlockPlayer1>().breakNow = true;
                        Teterminos.grid[roundedX - 1, roundedY].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Teterminos.grid[roundedX, roundedY] = null;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(Teterminos.grid[roundedX - 1, roundedY].gameObject, 0.5f);
                     
                      //  Teterminos.grid[roundedX-1, roundedY] = null;

                    }
                }
                else
                {

                }
            }



            // X +1 , y -1
            if (roundedX < 5)
            {
                if (Teterminos.grid[roundedX + 1, roundedY] != null)
                {
                    if (Teterminos.grid[roundedX + 1, roundedY].GetComponent<Teterminos>().blockColor == GetComponent<Teterminos>().blockColor)
                    {
                        if (Teterminos.grid[roundedX + 1, roundedY].GetComponent<TeterminoBlockPlayer1>() != null)
                            Teterminos.grid[roundedX + 1, roundedY].GetComponent<TeterminoBlockPlayer1>().breakNow = true;
                        Teterminos.grid[roundedX + 1, roundedY].GetComponent<SpriteRenderer>().color = Color.red;
                        GetComponent<SpriteRenderer>().color = Color.red;
                        Teterminos.grid[roundedX, roundedY] = null;
                        Destroy(this.gameObject, 0.5f);
                        Destroy(Teterminos.grid[roundedX + 1, roundedY].gameObject, 0.5f);
                       
                     //   Teterminos.grid[roundedX+1, roundedY] = null;

                    }


                }
                else
                {

                }

            }

        }
    }
}
