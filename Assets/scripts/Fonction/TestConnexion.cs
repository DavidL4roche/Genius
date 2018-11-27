using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

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
            String utilisateur = monNode["utilisateur"][0]["pseudo"].Value;

            if (utilisateur == "")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Votre identifiant ou votre mot de passe n'a pas été saisi correctement";
            }
            // Sinon on correspond bien à un utilisateur
            else
            {
                
            }
        }
    }

    /*
    public void testconnexion()
    {
        if (login.text=="" || passe.text=="")
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Les identifiants de connexion sont incorrects";
            return;
        }
        requete = "SELECT count(*) AS Total, IDPCharacter, PCName, LastConnection from p_character where PCName='"+login.text+"' and Password='"+passe.text+"';";
        try
        {
            MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
            MySqlDataReader lien = commande.ExecuteReader();
            while (lien.Read())
            {
                if (lien["Total"].ToString() == "1")
                {
                    Joueur.IDJoueur = (int)lien["IDPCharacter"];
                    Joueur.NomJoueur = lien["PCName"].ToString();
                    Joueur.dateDerniereCo = (DateTime)lien["LastConnection"];
                    ChargerLieu loading = new ChargerLieu();
                    loading.Charger("Daedelus");
                    Instantiate(JoueurLoge);            
                    break;
                }
                else
                {
                    ChargerPopup.Charger("Erreur");
                    MessageErreur.messageErreur = "Les identifiants de connexion sont incorrects";
                    break;
                }
            }
            lien.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e);
        }
    }
    */
}
