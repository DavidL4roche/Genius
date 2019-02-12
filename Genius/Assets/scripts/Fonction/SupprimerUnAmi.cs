using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class SupprimerUnAmi : MonoBehaviour {
    public void SupprimerAmi()
    {
        int idami = Joueur.MesAmis[VerificationAmi.AmiChoisi].SonID;
        string requete = "DELETE FROM friend WHERE (IDFriend=" + idami + " AND IDPCharacter=" + Joueur.IDJoueur + ") OR (IDFriend=" + Joueur.IDJoueur + " AND IDPCharacter=" + idami + ")";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
        RessourcesBdD.RecupDeLaListeDesJoueurs();
        RessourcesBdD.RecupMesAmis();
        FermerPopup fermerpop = new FermerPopup();
        FermerUneFenetre fermerfen = new FermerUneFenetre();
        ChargerPopup chargerpop = new ChargerPopup();
        ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
        fermerpop.Fermer("SupprimerAmi");
        fermerfen.Fermer("ReseauSocial");
        chargerfen.Charger("ReseauSocial");
        chargerpop.ChargerNonStatique("SupprimerAmi");
    }
}
