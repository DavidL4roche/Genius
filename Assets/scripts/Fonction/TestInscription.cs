using MySql.Data.MySqlClient;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestInscription : MonoBehaviour {

    // Paramètres
    public InputField login;
    public InputField mail;
    public InputField pass;
    private string requeteLoginMail;
    private string requeteInscription;
    
    public Joueur JoueurLoge;
    private Connexion connexion;

    // Enregistre l'inscription
    public void testInscription()
    {
        // Vérification input vides
        if (login.text=="")
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
                /*
                Joueur.IDJoueur = (int)lien["IDPCharacter"];
                Joueur.NomJoueur = lien["PCName"].ToString();
                Joueur.dateDerniereCo = (DateTime)lien["LastConnection"];
                ChargerLieu loading = new ChargerLieu();
                loading.Charger("Daedelus");
                Instantiate(JoueurLoge);
                */
            }
            reader.Close();
        }
        catch (IOException e)
        {
            Debug.Log(e);
        }
    }
}
