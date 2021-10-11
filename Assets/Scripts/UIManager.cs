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
            if (PlayerPrefs.GetInt("LevelNumber") < 9)
            {
                PlayerPrefs.SetInt("LevelNumber", PlayerPrefs.GetInt("LevelNumber") + 1);
            }
            else
            {
                PlayerPrefs.SetInt("LevelNumber", 2);
            }
        }

    }

    public void OnClick_LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void SoundToggle()
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
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
}
