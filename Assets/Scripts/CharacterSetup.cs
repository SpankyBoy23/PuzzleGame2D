using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSetup : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> bots;
    public Transform playerPos;
    public Transform botPos;

    private void Start()
    {
       // Debug.Log("Levle" + (PlayerPrefs.GetInt("LevelNumber") - 2));
        Instantiate(characters[PlayerPrefs.GetInt("CharacterIndex")], playerPos.position,playerPos.rotation);
        Instantiate(bots[PlayerPrefs.GetInt("LevelNumber")-2], botPos.position,botPos.rotation);
    }
}
