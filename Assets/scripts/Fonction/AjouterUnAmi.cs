using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjouterUnAmi : MonoBehaviour {
    public void AjouterAmi()
    {
        int idami = RessourcesBdD.listeDesJoueurs[VerificationAmi.AmiChoisi].SonID;
        string requete = "INSERT INTO friend VALUES ("+Joueur.IDJoueur+","+idami+")";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
        RessourcesBdD.RecupDeLaListeDesJoueurs();
        RessourcesBdD.RecupMesAmis();
        FermerPopup fermerpop = new FermerPopup();
        FermerUneFenetre fermerfen = new FermerUneFenetre();
        ChargerPopup chargerpop = new ChargerPopup();
        ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
        fermerpop.Fermer("AjouterAmi");
        fermerfen.Fermer("ReseauSocial");
        chargerfen.Charger("ReseauSocial");
        chargerpop.ChargerNonStatique("AjouterAmi");
    }
}
