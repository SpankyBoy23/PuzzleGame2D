using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookManager : MonoBehaviour
{
    public Text username;
    public Image img;

    private void Awake()
    {
        FB.Init(SetInit, OnHideUnity);
    }
    void SetInit()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Fb Logged in");
        }
        else
            Debug.Log("FB is not Logged In");

    }
    void OnHideUnity(bool isGameShown)
    {
        if (isGameShown)
        {
            Time.timeScale = 1;

        }
        else
            Time.timeScale = 0;
    }
    public void FbLogin()
    {
        List<string> permessions = new List<string>();
        permessions.Add("gaming_profile");
        FB.LogInWithReadPermissions(permessions, AuthCallResult);
    }
    void AuthCallResult(ILoginResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("FB LoggedIn");
                FB.API("/me?fields=first_name", HttpMethod.GET, callbackData);
                FB.API("me/picture?type=square&height=620&width=620", HttpMethod.GET, GetPicture);
            }
            else
            {
                Debug.Log("Login Failed!");
            }
        }
    }
    void callbackData(IResult res)
    {
        if(res.Error != null)
        {
            Debug.Log("Error Getting Data");
        }
        else
        {
           username.text = res.ResultDictionary["first_name"].ToString();
        }
    }
    private void GetPicture(IGraphResult res)
    {
        if (res.Error == null)
        {
         //   Image img = UIFBProfilePic.GetComponent<Image>();
            img.sprite = Sprite.Create(res.Texture, new Rect(0, 0, 620, 620), new Vector2());
        }

    }
}
