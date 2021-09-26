using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTeterminosBlock : MonoBehaviour
{
    public bool isFallen = false;
    public bool breakNow = false;
    int roundedX, roundedY;
    public BotTeterminos tp;


    void Update()
    {
        if (isFallen && breakNow)
        {
            roundedX = Mathf.RoundToInt(transform.position.x);
            roundedY = Mathf.RoundToInt(transform.position.y);
            //    Debug.Log(roundedX+" ,"+roundedY);

            // X 0 , y +1
            if (roundedY < 11)
            {
                Function(0, 1, 1);

            }


            // X 0 , y -1
            if (roundedY > 0)
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
    void Function(int x, int y, int call)
    {
        //Debug.Log(call);
        if (BotTeterminos.grid[roundedX + x, roundedY + y] != null)
        {
            if (BotTeterminos.grid[roundedX + x, roundedY + y].tag == this.gameObject.tag)
            {

                if (BotTeterminos.grid[roundedX + x, roundedY + y].GetComponent<BotTeterminosBlock>() != null)
                    BotTeterminos.grid[roundedX + x, roundedY + y].GetComponent<BotTeterminosBlock>().breakNow = true;

                if (!LogicManager.intance.objectListForBot.Contains(BotTeterminos.grid[roundedX, roundedY].gameObject))
                {
                    LogicManager.intance.objectListForBot.Add(BotTeterminos.grid[roundedX, roundedY].gameObject);
                }
            }
        }
    }
}
