using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactersManager : MonoBehaviour
{
    public static CharactersManager charactersManager;
    [SerializeField] Button[] charactersBtn;
    [SerializeField] Sprite lockSpirte;
    [SerializeField] Sprite[] characterSpirtes;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CharacterIndex"))
        {
            PlayerPrefs.SetInt("CharacterIndex", 1);
        }
        if (!PlayerPrefs.HasKey("CharacterUnlocked"))
        {
            PlayerPrefs.SetInt("CharacterUnlocked", 1);
        }
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
                charactersBtn[i].GetComponent<Image>().sprite = characterSpirtes[i];
            }
        }
    }
    public void CharacterSelection(int index)
    {
        PlayerPrefs.SetInt("CharacterIndex", index);
    }
}
