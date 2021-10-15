using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using LightReflectiveMirror;

public class Menu : MonoBehaviour
{
    public LightReflectiveMirrorTransport lightReflectiveMirrorTransport;
    bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        lightReflectiveMirrorTransport.serverListUpdated.AddListener(() => OnRefreshServersList());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowCharacterSelectionScene() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
        PlayerPrefs.SetInt("SceneId", 1);
    }

    public void RequestServersList()
    {
        lightReflectiveMirrorTransport.RequestServerList();
    }

    public void OnRefreshServersList()
    {
        if (firstTime == false)
        {
            firstTime = true;
            return;
        }

        StartCoroutine(InternalConnectToRoom());
    }

    public IEnumerator InternalConnectToRoom()
    {
        yield return new WaitForSeconds(0.4f);

        bool connected = false;

        foreach (var server in lightReflectiveMirrorTransport.relayServerList)
        {
            string[] data = server.serverData.Split('|');

            if (server.currentPlayers == server.maxPlayers) continue;
            if (data[1] != "PuzzleGame") continue;

            /*    statusText.text = "CONNECTED TO ROOM...";
    */
            //   Debug.Log($"{server.currentPlayers}/{server.maxPlayers}");

            NetworkManager.singleton.networkAddress = server.serverId;
            NetworkManager.singleton.StartClient();

            connected = true;
        }

        if (connected == false)
        {
            yield return new WaitForSeconds(0.8f);
            NetworkManager.singleton.StartHost();
            yield break;
        }
    }
}