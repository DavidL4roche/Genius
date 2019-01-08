using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON

public class ReinitialiserMdp : MonoBehaviour {
    // Paramètres
    private string url = "http://seriousgameiut.alwaysdata.net/scripts/ReinitiatePassword.php";
    private WWW download;

    public InputField mail;
    private string requete;
    private string urlComp;

    private string monJson;
    private JSONNode monNode;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        StartCoroutine(Reinitialiser());
    }

    // Permet de réinitialiser le mot de passe
    public IEnumerator Reinitialiser()
    {
        urlComp = url;
        urlComp += "?mail=" + mail.text;
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

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
            string result = monNode["result"].Value;

            if (result.ToLower() == "false")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }

            // La réinitialisation s'est bien déroulée
            else
            {
                ChargerPopup.Charger("Succes");
                MessageErreur.messageErreur = monNode["msg"].Value;

                // On efface le contenu de l'inputfield
                mail.Select();
                mail.text = "";
            }
        }
    }
}