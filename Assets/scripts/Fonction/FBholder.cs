using Facebook.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBholder : MonoBehaviour {

    public void InitialiseAndConnect()
    {
        FB.Init(SetInit, OnHideUnity);
    }

    private void SetInit()
    {
        Debug.Log("Fb Init done");

        if (FB.IsLoggedIn)
        {
            Debug.Log("FB Logged In");
        }
        else
        {
            var perms = new List<string>() { "public_profile", "email" };
            FB.LogInWithReadPermissions(perms, AuthCallback);
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void AuthCallback(ILoginResult result)
    {

        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }

            FB.API ("/me?fields=id,first_name,last_name", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string> () { });
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    // Récupère les infos sur la personne connectée
    void GetFacebookInfo(Facebook.Unity.IGraphResult result)
    {
        if (result.Error == null)
        {
            Debug.Log("Id : " + result.ResultDictionary["id"].ToString());
            Debug.Log("Prénom : " + result.ResultDictionary["first_name"].ToString());
            Debug.Log("Nom : " + result.ResultDictionary["last_name"].ToString());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }
}
