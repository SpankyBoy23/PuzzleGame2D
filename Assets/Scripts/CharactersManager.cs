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

    [Space]
    [Header("RandomPick")]
    public float maxTime = 3;
    public float timeToAnimate = 3;
    bool isRandom;
    int randomNumber;
    public int topReach;
    public int effect;

    private void Awake()
    {
        timeToAnimate = maxTime;
        if (charactersManager == null)
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
        }else
        {
            if(PlayerPrefs.GetInt("LevelNumber") >= PlayerPrefs.GetInt("CharacterUnlocked"))
                 PlayerPrefs.SetInt("CharacterUnlocked", PlayerPrefs.GetInt("LevelNumber"));
        }
        CharacterManagement();
        topReach = PlayerPrefs.GetInt("CharacterUnlocked") -1;
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
        foreach (Button btn in charactersBtn)
        {
            btn.GetComponent<Image>().color = Color.white;
        }
        charactersBtn[index].GetComponent<Image>().color = Color.gray;
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

    public void Random()
    {
        isRandom = true;
    }
    bool test = false;
    private void LateUpdate()
    {
        if (isRandom)
        {
           
            if(timeToAnimate > 0)
            {
                timeToAnimate -= Time.deltaTime;
                if(test)
                {
                    effect--;
                    
                }
                else if (!test)
                {
                    effect++;
                }
                if (effect == 0)
                    test = false;
                else if (effect == topReach)
                    test = true;
                foreach(Button btn in charactersBtn)
                {
                    btn.GetComponent<Image>().color = Color.white;
                }
                charactersBtn[effect].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                Debug.Log("Character: " + effect);
                isRandom = false;
                timeToAnimate = maxTime;
                PlayerPrefs.SetInt("CharacterIndex", effect);
                charactersBtn[effect ].GetComponent<Image>().color = Color.gray;
                characterImage.sprite = characterSpirtes[effect];
            }
        }
    }
}
