using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTeterminosPower : MonoBehaviour
{
    public bool isFallen = false;
    public bool breakNow = false;
    public BotTeterminos tP;
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
    void Function(int x, int y)
    {
        if (BotTeterminos.grid[roundedX + x, roundedY + y] != null)
        {
            //  test = TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<TeterminosPlayer2>().gameObject;
            if (BotTeterminos.grid[roundedX + x, roundedY + y].tag == this.gameObject.tag)
            {

                if (BotTeterminos.grid[roundedX + x, roundedY + y].GetComponent<BotTeterminosBlock>() != null)
                    BotTeterminos.grid[roundedX + x, roundedY + y].GetComponent<BotTeterminosBlock>().breakNow = true;
                //  TeterminosPlayer2.grid2[roundedX + x, roundedY + y].GetComponent<SpriteRenderer>().color = Color.red;
                if (!LogicManager.intance.objectListForBot.Contains(BotTeterminos.grid[roundedX, roundedY].gameObject))
                {
                    LogicManager.intance.objectListForBot.Add(BotTeterminos.grid[roundedX, roundedY].gameObject);
                }

                // GetComponent<SpriteRenderer>().color = Color.red;
                //  Destroy(this.gameObject, 2f);
                //  Destroy(TeterminosPlayer2.grid2[roundedX + x, roundedY + y].gameObject, 02);

            }
        }
    }
}
