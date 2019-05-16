using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjouterUnAmi : MonoBehaviour {

    public Text nomAmi;

    public void AjouterAmi()
    {
        // Recherche en base
        AutreJoueur ami= null;
        AutreJoueur[] verifSiAmiNul = new AutreJoueur[0];

        string requete = "SELECT IDPCharacter,PCName from p_character WHERE PCName='" + nomAmi.text + "';";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            ami = new AutreJoueur((int)lien["IDPCharacter"], lien["PCName"].ToString());
            verifSiAmiNul = new AutreJoueur[1];
        }
        lien.Close();

        // Le joueur existe
        if (verifSiAmiNul.Length > 0) {

            // On vérifie si le joueur ne l'a pas déjà en ami
            if (verifierAmi(ami.SonID))
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Ce joueur est déjà votre ami";
            }
            // On vérifie si ce n'est pas le joueur en cours
            else if(ami.SonID == Joueur.IDJoueur)
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Vous ne pouvez pas vous ajouter en tant qu'ami";
            }
            else
            {
                // Insertion en base
                string requeteAjout = "INSERT INTO friend VALUES (" + Joueur.IDJoueur + "," + ami.SonID + ")";
                MySqlCommand commandeAjout = new MySqlCommand(requeteAjout, Connexion.connexion);
                MySqlDataReader lienAjout = commandeAjout.ExecuteReader();
                lienAjout.Close();

                // Mise à jour fenêtre Reseau Social
                RessourcesBdD.RecupDeLaListeDesJoueurs();
                RessourcesBdD.RecupMesAmis();
                FermerUneFenetre fermerfen = new FermerUneFenetre();
                ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
                fermerfen.Fermer("ReseauSocial");
                chargerfen.Charger("ReseauSocial");
            }
        }
        // Il n'existe pas, on affiche l'erreur
        else
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Ce joueur n'existe pas";
        }
    }

    public bool verifierAmi(int IDAmi)
    {
        string requete = "SELECT * FROM friend WHERE IDPCharacter = " + Joueur.IDJoueur + " AND IDFriend = " + IDAmi + " UNION SELECT* FROM friend WHERE IDPCharacter = " + IDAmi + " AND IDFriend = " + Joueur.IDJoueur + ";";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        int cpt = 0;
        while (lien.Read())
        {
            ++cpt;
        }
        lien.Close();

        return ((cpt > 0) ? true : false);
    }
}
