using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharactersManager : MonoBehaviour
{
    public static CharactersManager charactersManager;
    [SerializeField] Button[] charactersBtn;
    [SerializeField] Sprite lockSpirte;
    [SerializeField] Sprite[] characterBtnSpirtes;
    [SerializeField] Sprite[] characterSpirtes;
    [SerializeField] Image characterImage;

    ///////////IntroPanel..........//////////////////
    [SerializeField] GameObject panel;
    [SerializeField] Image[] characterIntroImages; 
    [SerializeField] Image[] characterIntroCubbyImage;
    [SerializeField] Sprite[] characterIntroSprites;

    private void Awake()
    {
        if(charactersManager == null)
        {
            charactersManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
       

        if (!PlayerPrefs.HasKey("CharacterIndex"))
        {
            PlayerPrefs.SetInt("CharacterIndex", 1);
        }
        if (!PlayerPrefs.HasKey("LevelNumber"))
        {
            PlayerPrefs.SetInt("LevelNumber", 2);
        }
        if (!PlayerPrefs.HasKey("CharacterUnlocked"))
        {
            PlayerPrefs.SetInt("CharacterUnlocked", 2);
        }
        CharacterManagement();
    }

    public void UnlockAll()
    {
        PlayerPrefs.SetInt("CharacterUnlocked", 6);
        CharacterManagement();
    }
    public void CharacterManagement()
    {
        foreach(Button btn in charactersBtn)
        {
            btn.interactable = false;
            btn.GetComponent<Image>().sprite = lockSpirte;
        }
        for (int i = 0; i < charactersBtn.Length; i++)
        {
            if(i <= PlayerPrefs.GetInt("CharacterUnlocked")-1)
            {
                charactersBtn[i].interactable = true;
                charactersBtn[i].GetComponent<Image>().sprite = characterBtnSpirtes[i];
            }
        }
    }
    public void CharacterSelection(int index)
    {
        PlayerPrefs.SetInt("CharacterIndex", index);
        characterImage.sprite = characterSpirtes[index];
    }
    public void StartBtn()
    {
        panel.SetActive(true);
        characterIntroImages[0].sprite = characterIntroSprites[PlayerPrefs.GetInt("CharacterIndex")];
        characterIntroImages[1].sprite = characterIntroSprites[PlayerPrefs.GetInt("LevelNumber")];
        characterIntroCubbyImage[0].sprite = characterSpirtes[PlayerPrefs.GetInt("CharacterIndex")];
        characterIntroCubbyImage[1].sprite = characterSpirtes[PlayerPrefs.GetInt("LevelNumber")];
        this.Wait(2f, () => { SceneManager.LoadScene("OfflineMode"); });
    }
}
