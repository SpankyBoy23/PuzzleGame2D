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
    public bool sound;
    public bool music;

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
        this.Wait(1f, () =>
        {
            setToggleMusic();
            SetToggleSound();
        });
      
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
    public void SoundToggle()
    {
        if(PlayerPrefs.GetInt("Sound") == 0)
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
    public void MusicToggle()
    {
        if(PlayerPrefs.GetInt("Music") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
            
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);

        }
    }
    public void SetToggleSound()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            sound.isOn = true;
            this.Wait(0.5f, () =>
            {

            });
        }
        else if (PlayerPrefs.GetInt("Sound") == 1)
        {
            sound.isOn = false;
            this.Wait(0.5f, () =>
            {
                FindObjectOfType<MusicManager>().PauseAll();
            });
        }
    }
    public void setToggleMusic()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            music.isOn = true;
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            music.isOn = false;
        }
    }
}
