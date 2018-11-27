using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON

public class TestConnexion : MonoBehaviour {
    // Paramètres
    private string url = "http://seriousgameiut.alwaysdata.net/scripts/CheckConnection.php";
    private WWW download;

    public InputField pseudo;
    public InputField pass;
    public Joueur JoueurLoge;
    string requete;
    private string urlComp;

    private string monJson;
    private JSONNode monNode;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        if (pseudo.text != "" && pass.text != "")
        {
            StartCoroutine(CheckConnection());
        }
        else {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Veuillez saisir les identifiants de connexion";
            return;
        }
    }

    // Permet de créer un utilisateur dans la base
    public IEnumerator CheckConnection()
    {
        urlComp = url + "?pseudo=" + pseudo.text + "&pass=" + pass.text;
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
            string utilisateur = monNode["utilisateur"][0]["pseudo"].Value;

            if (utilisateur == "")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Votre identifiant ou votre mot de passe n'a pas été saisi correctement";
            }
            // Sinon on correspond bien à un utilisateur
            else
            {
                // On récupère les données du Joueur pour l'attribuer à notre objet
                int.TryParse(monNode["utilisateur"][0]["id"].Value, out Joueur.IDJoueur);
                Joueur.NomJoueur = monNode["utilisateur"][0]["pseudo"].Value;
                Joueur.dateDerniereCo = Convert.ToDateTime(monNode["utilisateur"][0]["lastConnection"].Value);
                ChargerLieu loading = new ChargerLieu();
                loading.Charger("Daedelus");
                Instantiate(JoueurLoge);
            }
        }
    }
}
