using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    public string publicUrl;
    public string privateUrl;

    public string addEndPoint;
    public string getEndPoint;

    public List<Entry> entries = new List<Entry>();

    // Start is called before the first frame update
    void Start()
    {
        UploadScore();
        GetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UploadScore() => StartCoroutine(UploadScore_Internal());
    public void GetScore() => StartCoroutine(GetScore_Internal());

    IEnumerator UploadScore_Internal()
    {
        string username = PlayerPrefs.GetString("Username");
        int wins = PlayerPrefs.GetInt("Wins", 0);
        int loses = PlayerPrefs.GetInt("Loses", 0);

        string pData = $"{username}/{wins}/{loses}";

        using (UnityWebRequest www = UnityWebRequest.Get($"{privateUrl}{addEndPoint}{pData}"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success || (www.downloadHandler.text != "OK"))
                Debug.LogError("Error in leaderboard while posting data");

            Debug.Log(www.downloadHandler.text);
        }
    }

    IEnumerator GetScore_Internal() 
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{publicUrl}{getEndPoint}"))
        {
            yield return www.SendWebRequest();

            if(www.result != UnityWebRequest.Result.Success)
                Debug.LogError("Error in leaderboard while getting data");

            string[] downloadedEntries = www.downloadHandler.text.Split('\n');

            for(int i = 0; i < downloadedEntries.Length - 1; i++) 
            {
                string[] entry = downloadedEntries[i].Split('|');
                Entry entry_= new Entry { username = entry[0], wins = int.Parse(entry[1]), loses = int.Parse(entry[2]) };

                entries.Add(entry_);
            }
        }
    }

    [System.Serializable]
    public struct Entry
    {
        public string username;
        public int wins;
        public int loses;
    }
}
