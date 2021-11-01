using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager menuManager;
    public GameObject MultiplayerMode;
    public GameObject usernamePanel;
    [SerializeField] TMP_InputField usernameInuput;
    public bool sound = true;
    public bool music = true;
    public Toggle[] toggle;

    private void Awake()
    {
        PlayerPrefs.SetInt("SceneId", 0);
        menuManager = this;

        if (string.IsNullOrEmpty(PlayerPrefs.GetString("playerName")) == false)
        {
            usernamePanel.SetActive(false);
        }

    }
    private void Update()
    {
  /*      if (!PlayerPrefs.HasKey("UserName") && MultiplayerMode.activeSelf)
        {
            usernamePanel.SetActive(true);
        }
*/


    }

    public void FindMatch() 
    {
        Mirror.NetworkManager.singleton.GetComponent<Menu>().ShowCharacterSelectionScene();
    }

    private void Start()
    {
        
    }
    #region Username

    public void SetUserName()
    { 
        if(usernameInuput.text != "")
        {
         //   PlayerPrefs.SetString("UserName", usernameInuput.text);
            usernamePanel.SetActive(false);
        }
    }

    #endregion
    public void LoadSceneWithIndex(int index)
    {
        PlayerPrefs.SetInt("SceneId", 0);
        SceneManager.LoadScene(index);
    }
    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }
   
    public void SetToggleSound()
    {
        setting.intance.SetSound();
        toggle[0].isOn = setting.intance.sound;
        
    }
    public void setToggleMusic()
    {
        setting.intance.SetMusic();
        toggle[1].isOn = setting.intance.music;
        if (!setting.intance.music)
            MusicManager.instance.PauseAll();
        else
            MusicManager.instance.SceneLaod(SceneManager.GetActiveScene().buildIndex);
                
    }
}
