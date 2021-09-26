using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{

    public static LogicManager intance;

    public List<GameObject> objectList;
    public List<GameObject> objectListForBot;
    public int noOfBlocksLastDestoryed;
    public int noOfBlocksLastDestoryedBot;
    bool justBroke = false;
    bool justBrokeBot = false;
    [SerializeField] float coolDown = 1f;
    [SerializeField] float coolDownBot = 1f;
    public PlayerBehviour playerBehviour;
    public BotBehaviour botBehaviour;

    private void Awake()
    {
        intance = this;
    }

    private void Start()
    {
        
       
    }
    private void Update()
    {
        if (!playerBehviour)
        {
            playerBehviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehviour>();
        }
        if (!botBehaviour)
        {
            botBehaviour = GameObject.FindGameObjectWithTag("Bot").GetComponent<BotBehaviour>();
        }
    }
    void LateUpdate()
    {
        //Debug.Log(objectList.Count);
        DestroyPlayerBlocks();
        DestroyBotBlocks();
    }
    private void DestroyPlayerBlocks()
    {
        if (objectList.Count > 1)
        {
            noOfBlocksLastDestoryed = objectList.Count;
            foreach (GameObject obj in objectList)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                this.Wait(0.2f, () => { Destroy(obj); });
                playerBehviour.Walk();
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

    private void DestroyBotBlocks()
    {
        if (objectListForBot.Count > 1)
        {
            noOfBlocksLastDestoryedBot = objectListForBot.Count;
            foreach (GameObject obj in objectListForBot)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                this.Wait(0.2f, () => { Destroy(obj); });
                botBehaviour.Walk();
            }
            this.Wait(0.4f, () => { ResetListBot(); });

        }
        else if (objectListForBot.Count == 1)
        {
            objectListForBot.Clear();
        }
        if (justBrokeBot)
        {
            coolDownBot -= Time.deltaTime;
        }
        if (coolDownBot < 0)
        {
            justBrokeBot = false;
            coolDownBot = 1;
        }
    }
    private void ResetListBot()
    {
        justBrokeBot = true;
        objectListForBot.Clear();
    }
}
