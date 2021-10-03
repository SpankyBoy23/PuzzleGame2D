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
    public GameObject playerBehviour;
    public GameObject botBehaviour;
    public bool finalMove;
    public bool canSpawn = true;
    [SerializeField] GameObject[] spawners;
    bool runOnce = true;

    public GameObject DestoryEffect;

    private void Awake()
    {
        intance = this;
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
    }

    private void Start()
    {
        
       
    }
    private void Update()
    {
      
        if (!playerBehviour)
        {
            playerBehviour = GameObject.FindGameObjectWithTag("Player");
        }
        if (!botBehaviour)
        {
            botBehaviour = GameObject.FindGameObjectWithTag("Bot");
        }
    }
   
    void LateUpdate()
    {
        //Debug.Log(objectList.Count);
        DestroyPlayerBlocks();
        DestroyBotBlocks();
    }
    public void BlockSpawners()
    {
        if (spawners[1].GetComponent<BlockSpawnerForBot>())
        {
            spawners[1].GetComponent<BlockSpawnerForBot>().enabled = false;
        }
        else
        {
            spawners[0].GetComponent<BlockSpawnerForBot>().enabled = false;
        }

        if (spawners[0].GetComponent<BlockSpawnerPlayer2>())
        {
            spawners[0].GetComponent<BlockSpawnerPlayer2>().enabled = false;
        }
        else
        {
            spawners[1].GetComponent<BlockSpawnerPlayer2>().enabled = false;
        }
    }
    public void LastMove(bool callFromPlayer)
    {
        finalMove = true;
        if (runOnce)
        {
            if (callFromPlayer)
            {
                if (playerBehviour.GetComponent<PlayerBehviour>())
                    playerBehviour.GetComponent<PlayerBehviour>().Walk();
                else
                {
                    playerBehviour.GetComponent<WandPlayerBehaviour>().Attack();
                }
                this.Wait(3f, () => { UIManager.intance.Endgame(1); });
            }
            else
            {
                if (botBehaviour.GetComponent<BotBehaviour>())
                    botBehaviour.GetComponent<BotBehaviour>().Walk();
                else
                    botBehaviour.GetComponent<BotBehaviour>();

                this.Wait(2f, () => { UIManager.intance.Endgame(0); });
            }
            runOnce = false;
        }
      
        
    }
    private void DestroyPlayerBlocks()
    {
        if (objectList.Count > 1)
        {
            noOfBlocksLastDestoryed = objectList.Count;
            foreach (GameObject obj in objectList)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
                this.Wait(0.2f, () => {  Destroy(obj); });
                
                if(playerBehviour.GetComponent<PlayerBehviour>())
                   playerBehviour.GetComponent<PlayerBehviour>().Walk();
                else
                {
                    playerBehviour.GetComponent<WandPlayerBehaviour>().Attack();
                }
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
                if (botBehaviour.GetComponent<BotBehaviour>())
                    botBehaviour.GetComponent<BotBehaviour>().Walk();
                else
                    botBehaviour.GetComponent<BotBehaviour>();
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
