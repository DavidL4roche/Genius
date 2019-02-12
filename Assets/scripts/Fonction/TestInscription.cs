using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON
using System.Collections;

public class TestInscription : MonoBehaviour {

    // Paramètres
    private string url = Configuration.url + "scripts/CreateUser.php";
    private WWW download;
    public InputField pseudo;
    public InputField mail;
    public InputField pass;
    private string requeteLoginMail;
    private string requeteInscription;
    
    public Joueur JoueurLoge;

    private string monJson;
    private JSONNode monNode;

    private string urlComp;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        StartCoroutine(CreateUser());
    }

    // Permet de créer un utilisateur dans la base
    public IEnumerator CreateUser()
    {
        urlComp = url;
        urlComp += "?pseudo=" + pseudo.text + "&mail=" + mail.text + "&pass=" + pass.text;
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

            if (result.ToLower() == "false")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }
            
            // L'inscription s'est bien déroulée
            else
            {
                ChargerLieu loading = new ChargerLieu();
                loading.Charger("Login");

                ChargerPopup.Charger("Succes");
                MessageErreur.messageErreur = "Votre compte a bien été crée";
            }
        }
    }
}
