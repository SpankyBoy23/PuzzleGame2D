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
}
