using MySql.Data.MySqlClient;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON
using System.Collections;

public class TestInscription : MonoBehaviour {

    // Paramètres
    private string url = "http://seriousgameiut.alwaysdata.net/scripts/CreateUser.php";
    private WWW download;
    public InputField pseudo;
    public InputField mail;
    public InputField pass;
    private string requeteLoginMail;
    private string requeteInscription;
    
    public Joueur JoueurLoge;
    private Connexion connexion;

    private string urlComp;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        if (pseudo.text != "" && mail.text != "" && pass.text != "")
        {
            StartCoroutine(CreateUser());
        }
        else
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Veuillez saisir les informations demandées";
        }
    }

    // Permet de créer un utilisateur dans la base
    public IEnumerator CreateUser()
    {
        urlComp = url;
        urlComp += "?pseudo=" + pseudo.text + "&mail=" + mail.text + "&pass=" + pass.text;
        download = new WWW(urlComp);
        yield return download;
        print (urlComp);

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            // L'inscription s'est bien déroulée
            
            ChargerLieu loading = new ChargerLieu();
            loading.Charger("Login");

            ChargerPopup.Charger("Succes");
            MessageErreur.messageErreur = "Votre compte a bien été crée";
        }
    }
}
