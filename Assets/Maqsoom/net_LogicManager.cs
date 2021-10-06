using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class net_LogicManager : MonoBehaviour
{

    public static net_LogicManager intance;

    public List<GameObject> objectList;
    public int noOfBlocksLastDestoryed;
    bool justBroke = false;
    [SerializeField] float coolDown = 1f;
   
/*    public PlayerBehviour playerBehviour;
    public BotBehaviour botBehaviour;*/

    private void Awake()
    {
        intance = this;
    }

    private void Start()
    {


    }
    private void Update()
    {
      /*  if (!playerBehviour)
        {
            playerBehviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehviour>();
        }
        if (!botBehaviour)
        {
            botBehaviour = GameObject.FindGameObjectWithTag("Bot").GetComponent<BotBehaviour>();
        }*/
    }
    void LateUpdate()
    {
        //Debug.Log(objectList.Count);

        if (Mirror.NetworkServer.active == false) return;

        DestroyPlayerBlocks();
    }
    private void DestroyPlayerBlocks()
    {
        if (objectList.Count > 1)
        {
            noOfBlocksLastDestoryed = objectList.Count;
            foreach (GameObject obj in objectList)
            {
                if(obj!=null)
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                this.Wait(0.2f, () => { Mirror.NetworkServer.Destroy(obj); });
               
                //playerBehviour.Walk();
            }
            this.Wait(0.4f, () => { ResetList(); });

        }
        else if (objectList.Count == 1)
        {
            objectList.Clear();
        }
        if (justBroke)
        {
            coolDown -= Time.deltaTime;
        }
        if (coolDown < 0)
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
