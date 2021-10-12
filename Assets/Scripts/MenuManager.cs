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
        menuManager = this;
        
    }
    private void Update()
    {
        if (!PlayerPrefs.HasKey("UserName") && MultiplayerMode.activeSelf)
        {
            usernamePanel.SetActive(true);
        }

     
    }
    private void Start()
    {
      
      
    }
    #region Username

    public void SetUserName()
    { 
        if(usernameInuput.text != "")
        {
            PlayerPrefs.SetString("UserName", usernameInuput.text);
            usernamePanel.SetActive(false);
        }
    }

    #endregion
    public void LoadSceneWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void OpenLink(string Link)
    {
        Application.OpenURL(Link);
    }
   
    public void SetToggleSound()
    {
       sound = !sound;
       toggle[0].isOn = sound;
        
    }
    public void setToggleMusic()
    {
        music = !music;
        toggle[1].isOn = music;
        if (!music)
            MusicManager.instance.PauseAll();
        else
            MusicManager.instance.SceneLaod(SceneManager.GetActiveScene().buildIndex);
                
    }
}
