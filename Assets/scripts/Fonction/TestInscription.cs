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

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        if (pseudo.text != "" && mail.text != "" && pass.text != "")
        {
            StartCoroutine(CreateUser());
        }
    }

    // Permet de créer un utilisateur dans la base
    public IEnumerator CreateUser()
    {
        url = url + "?pseudo=" + pseudo.text + "&mail=" + mail.text + "&pass=" + pass.text;
        download = new WWW(url);
        yield return download;
        print (url);

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            print("Ok");
        }
    }

    /*
    // Enregistre l'inscription
    public void testInscription()
    {
        // Vérification input vides
        if (pseudo.text=="")
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Veuillez spécifier un login";
            return;
        }
        if (mail.text == "")
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Veuillez spécifier un mail";
            return;
        }
        if (pass.text=="")
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Veuillez spécifier un mot de passe";
            return;
        }

        // Liste des requêtes à effectuer
        requeteLoginMail = "SELECT * FROM p_character where PCName='" + login.text + "' UNION SELECT * FROM p_character WHERE mail = '" + mail.text + "';";
        requeteInscription = "INSERT INTO p_character ('IDPCharacter', 'PCName', 'mail', 'Password', 'LastConnection') VALUES (NULL, '" + login.text
            + "', '" + mail.text + "', '" + pass.text + "', Now());";
        print(requeteLoginMail);
        print(requeteInscription);

        try
        {
            MySqlCommand commande = new MySqlCommand(requeteLoginMail, connexion.getConnexion());
            MySqlDataReader reader = commande.ExecuteReader();
            if (reader.Read())
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Le login ou le mail existent déjà.";
                return;
            }
            else
            {
                // Fermeture connexion
                connexion.stopConnexion();
                reader.Close();

                // Inscription dans la base
                commande = new MySqlCommand(requeteInscription, Connexion.connexion);
                commande.ExecuteReader();

                // Connexion et accès au jeu
                Joueur.IDJoueur = (int)lien["IDPCharacter"];
                Joueur.NomJoueur = lien["PCName"].ToString();
                Joueur.dateDerniereCo = (DateTime)lien["LastConnection"];
                ChargerLieu loading = new ChargerLieu();
                loading.Charger("Daedelus");
                Instantiate(JoueurLoge);
            }
            reader.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e);
        }
    }
    */
}
