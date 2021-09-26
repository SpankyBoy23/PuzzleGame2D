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
    }

    public void OnClick_LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
