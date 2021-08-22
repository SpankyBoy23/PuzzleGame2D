using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnerPlayer2 : MonoBehaviour
{
    public GameObject[] Blocks;
    public static BlockSpawnerPlayer2 blockSpawner;

    private void Start()
    {
        blockSpawner = this;
        NewBlock();
    }
    public void NewBlock()
    {
        Debug.Log("Spawning");
        var a = Instantiate(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
        a.transform.parent = null;
    }
}
