using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinitialiserMdp : MonoBehaviour
{
    // Paramètres
    private string url = "http://seriousgameiut.alwaysdata.net/scripts/ReinitiatePassword.php";
    private WWW download;
    public InputField mail;
    private string requeteLoginMail;

    private string monJson;
    private JSONNode monNode;

    private string urlComp;

    // Reinitialiser le mot de passe
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
                ChargerLieu loading = new ChargerLieu();
                loading.Charger("Index2");

                ChargerPopup.Charger("Succes");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }
        }
    }
}