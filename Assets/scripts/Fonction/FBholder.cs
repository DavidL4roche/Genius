using Facebook.Unity;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBholder : MonoBehaviour {

    private string urlCheckConnection = "http://seriousgameiut.alwaysdata.net/scripts/CheckConnectionByMail.php";
    private string urlAddIP = "http://seriousgameiut.alwaysdata.net/scripts/AddIP.php";
    private string urlCreateUser = "http://seriousgameiut.alwaysdata.net/scripts/CreateUser.php";
    private WWW download;
    private WWW download2;

    public Joueur JoueurLoge;

    private string monJson;
    private JSONNode monNode;

    public void InitialiseAndConnect()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(SetInit, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
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
            FB.API ("/me?fields=id,first_name,last_name,email", HttpMethod.GET, GetFacebookInfo, new Dictionary<string, string> () { });
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
            // Token : EAAdpipzaYAsBAG4yZBlz21LZB0TygWutihi9RXZAAyniwg70UUTkiPgoaYhTss7ZBqBAKtH1AdqPNsmFjCGEFBk0LqNObkJOrOrIF40LlyjxQz28fSjq0YdH4ghWUgQ5hv0eHpF3WxrT11R5kZCqvlX2BqlzwDvmabFfZAhY44TD7Q7FSg62KpDveSJribrFIZBbhaCJcMc5gZDZD
            if (result.ResultDictionary.ContainsKey("id"))
            {
                string idFb = result.ResultDictionary["id"].ToString();
                Debug.Log("Id : " + idFb);
            }
            if (result.ResultDictionary.ContainsKey("first_name"))
            {
                string prenom = result.ResultDictionary["first_name"].ToString();
                Debug.Log("Prénom : " + prenom);
            }
            if (result.ResultDictionary.ContainsKey("last_name"))
            {
                string nom = result.ResultDictionary["last_name"].ToString();
                Debug.Log("Nom : " + nom);
            }
            if (result.ResultDictionary.ContainsKey("email"))
            {
                string email = result.ResultDictionary["email"].ToString();
                Debug.Log("Email : " + email);

                // On vérifie si l'utilisateur existe dans la base (mail et pass : id)
                string name = result.ResultDictionary["first_name"].ToString() + result.ResultDictionary["last_name"].ToString();
                string idFb = result.ResultDictionary["id"].ToString();
                StartCoroutine(CheckConnection(name, email, idFb));
            }
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    // Connexion ou redirection vers l'inscription de l'utilisateur
    private IEnumerator CheckConnection(string nom, string mail, string pass)
    {
        string urlComp = urlCheckConnection + "?mail=" + mail + "&pass=" + pass;
        Debug.Log(urlComp);
        download = new WWW(urlComp);
        yield return download;

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            monJson = download.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'un utilisateur est renvoyé)
            string result = monNode["result"].Value;

            // L'utilisateur n'existe pas
            if (result.ToLower() == "false")
            {
                // S'il n'existe pas, on crée son compte et on le connecte
                StartCoroutine(Connect(nom, mail, pass));
            }

            // Sinon on correspond bien à un utilisateur
            else
            {
                // On récupère l'IP de l'appareil
                string ipLocal = DemarrageGenius2.LocalIPAddress();

                // On ajoute l'IP du téléphone au joueur
                string urlComp2 = urlAddIP + "?ip=" + ipLocal + "&playerId=" + monNode["utilisateur"][0]["id"];
                download = new WWW(urlComp2);
                yield return download;

                // On change le booléen isConnected de l'adresse IP du joueur en vrai (1)
                string urlIP = "http://seriousgameiut.alwaysdata.net/scripts/ConnectOnIP.php";
                urlIP += "?connect=true&playerId=" + ipLocal;
                download2 = new WWW(urlIP);
                yield return download2;

                // On récupère les données du Joueur pour l'attribuer à notre objet
                int.TryParse(monNode["utilisateur"][0]["id"].Value, out Joueur.IDJoueur);
                Joueur.NomJoueur = monNode["utilisateur"][0]["pseudo"].Value;
                Joueur.dateDerniereCo = Convert.ToDateTime(monNode["utilisateur"][0]["lastConnection"].Value);
                ChargerLieu loading = new ChargerLieu();
                Instantiate(JoueurLoge);

                // On vérifie si c'est la première connection de l'utilisateur
                if (monNode["utilisateur"][0]["isFirstConnection"] == 1)
                {
                    // On change le booléen isFirstConnection du joueur en faux (0)
                    string urlStat = "http://seriousgameiut.alwaysdata.net/scripts/ChangePlayerStats.php";
                    urlStat += "?stat=isFirstConnection&value=0&id=" + monNode["utilisateur"][0]["id"].Value;
                    download = new WWW(urlStat);
                    yield return download;

                    loading.Charger("Tutoriel");
                }
                else
                {
                    loading.Charger("Daedelus");
                }
            }
        }
    }

    // Connection à Genius
    public IEnumerator Connect(string pseudo, string mail, string pass)
    {
        string urlComp2 = urlCreateUser;
        urlComp2 += "?pseudo=" + pseudo + "&mail=" + mail + "&pass=" + pass;
        Debug.Log(urlComp2);
        download = new WWW(urlComp2);
        yield return download;

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            monJson = download.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'un utilisateur est renvoyé)
            string result = monNode["result"].Value;

            if (result.ToLower() == "false")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }

            // L'inscription s'est bien déroulée
            else
            {
                Debug.Log("Inscription bien déroulée");
                // On rerentre dans CheckConnection pour se connecter et lancer le jeu
                StartCoroutine(CheckConnection(name, mail, pass));
            }
        }
    }

    // Déconnexion
    public void LogOutButton()
    {
        FB.LogOut();
        Destroy(GameObject.Find("UnityFacebookSDKPlugin"));
        Debug.Log("User logged out");
    }
}
