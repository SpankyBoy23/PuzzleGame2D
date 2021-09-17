using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public static LogicManager intance;

    public List<GameObject> objectList;
    public int noOfBlocksLastDestoryed;
    bool justBroke = false;
    [SerializeField] float coolDown = 1f;
    public Vector2[] gird2Vector;

    private void Awake()
    {
        intance = this;
    }

    private void Update()
    {
        for (int i = 0; i <gird2Vector.Length; i++)
        {
           // Debug.Log(gird2Vector[i]);
          //  Debug.Log(TeterminosPlayer2.grid2[Mathf.RoundToInt(gird2Vector[i].x), Mathf.RoundToInt(gird2Vector[i].y)]);
            if(!TeterminosPlayer2.grid2[Mathf.RoundToInt(gird2Vector[i].x), Mathf.RoundToInt(gird2Vector[i].y)].gameObject)
            {
                Debug.Log(gird2Vector[i]);
            }
        }
        foreach (Vector2 grid in gird2Vector)
        {
         //   Debug.Log("g");
            int x = Mathf.RoundToInt(grid.x);
            int y = Mathf.RoundToInt(grid.y);
           // Debug.Log(x+","+ y+ ":");
        }
    }

    void LateUpdate()
    {
        //Debug.Log(objectList.Count);
       if(objectList.Count > 1)
       {
            noOfBlocksLastDestoryed = objectList.Count;
            foreach(GameObject obj in objectList)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                this.Wait(1f, () => { Destroy(obj); });
            }
            this.Wait(1.1f, () => { ResetList(); });

       }  
       else if(objectList.Count == 1)
        {
            objectList.Clear();
        }
        if (justBroke)
        {
            coolDown -= Time.deltaTime;
        }
        if(coolDown < 0)
        {
            justBroke = false;
            coolDown = 1;
        }
    }
    private void ResetList()
    {
        justBroke = true;
        objectList.Clear();
    }
}
