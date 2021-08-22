using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class net_BlockMovement : NetworkBehaviour
{
    public static int height = 10;
    public static int width = 6;
    float previousTime;
    private float fallTime = 0.8f;

    public static Transform[,] grid2 = new Transform[width, height];

    public bool net_Start;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasAuthority == false) return;
        if (net_Start == false) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (transform.position.y <= 0)
        {
            //  BlockSpawner.blockSpawner.NewBlock();
            //  this.GetComponent<Teterminos>().enabled = false;
        }
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                Debug.Log("Working");
                transform.position -= new Vector3(0, -1, 0);
                AddToGird();
                this.enabled = false;

                NetworkClient.localPlayer.GetComponent<Player>().NewBlock();
            }
            previousTime = Time.time;
        }
    }

    bool ValidMove()
    {

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);
/*        Debug.Log(roundedY);*/

        if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
        {
            return false;
        }
        if (grid2[roundedX, roundedY] != null)
            return false;


        return true;
    }
    void AddToGird()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);

        grid2[roundedX, roundedY] = transform;
    }

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        net_Start = true;
    }
}
