using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{

    public static UIManager intance;

    public GameObject endGamePanel;
    public Image endGameImage;
    public Sprite[] endGameSprites;
    
    private void Awake()
    {
        intance = this;
    }

    public void Endgame(int a)
    {
        endGamePanel.SetActive(true);
        endGameImage.sprite = endGameSprites[a];
        if (a == 0)
        {
            if (PlayerPrefs.GetInt("LevelNumber") < 9 && PlayerPrefs.GetInt("Unlocked")!= 1)
            {
                Debug.Log("Woring");
                PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
            }
            else if(PlayerPrefs.GetInt("LevelNumber") == 9 && PlayerPrefs.GetInt("Unlocked") != 1)
            {
                PlayerPrefs.SetInt("CharacterUnlocked", PlayerPrefs.GetInt("CharacterUnlocked") + 1);
                PlayerPrefs.SetInt("LevelNumber", 2);
            }
            if (PlayerPrefs.GetInt("LevelNumber") < 13 && PlayerPrefs.GetInt("Unlocked") == 1)
            {
                PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
            }
            else if (PlayerPrefs.GetInt("LevelNumber") == 13 && PlayerPrefs.GetInt("Unlocked") == 1)
            {
               // PlayerPrefs.SetInt("CharacterUnlocked", PlayerPrefs.GetInt("CharacterUnlocked") + 1);
                PlayerPrefs.SetInt("LevelNumber", 2);
            }
          
        }

    }

    public void OnClick_LoadScene(int index)
    {
        if(SceneManager.GetActiveScene().buildIndex > 1 && index == 1)
        {
            FindObjectOfType<AdManager>().ShowInterstitial();
           
        }
        else
        {
            Load(index);
        }
    }
    public void Load(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void SoundToggle()
    {
        setting.intance.SetSound();
        
    }
    public void MusicToggle()
    {
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            PlayerPrefs.SetInt("Music", 1);
           // FindObjectOfType<MusicManager>().PauseAll();
        }
        else if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
           // FindObjectOfType<MusicManager>().SceneLaod(SceneManager.GetActiveScene().buildIndex);
        }
    }
    public void showAd()
    {
        
    }
}
