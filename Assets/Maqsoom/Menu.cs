using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using LightReflectiveMirror;

public class Menu : MonoBehaviour
{
    public static string username;

    public GameObject menuObj;
    public GameObject gameObj;

    public InputField usernameField;
    public Text usernameText;

    public LightReflectiveMirrorTransport lightReflectiveMirrorTransport;

    bool firstTime;

    // Start is called before the first frame update
    void Start()
    {
        usernameField.text = PlayerPrefs.GetString("Username");

        lightReflectiveMirrorTransport.serverListUpdated.AddListener(() => OnRefreshServersList());
    }

    // Update is called once per frame
    void Update()
    {
        if (!string.IsNullOrEmpty(usernameField.text) && usernameField.text != username) 
        {
            username = usernameField.text;
            usernameText.text = username;

            PlayerPrefs.SetString("Username", username);
        }
    }

    public void ShowGameMenu() 
    {
        menuObj.SetActive(false);
        gameObj.SetActive(true);
    }
    public void RequestServersList()
    {
        lightReflectiveMirrorTransport.RequestServerList();
    }

    public void OnRefreshServersList()
    {
        if(firstTime == false)
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
            if (server.currentPlayers == server.maxPlayers) continue;

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