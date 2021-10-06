using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

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
}
