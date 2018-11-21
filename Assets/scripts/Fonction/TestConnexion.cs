using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestConnexion : MonoBehaviour {
    public InputField login;
    public InputField passe;
    string requete;
    public Joueur JoueurLoge;
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
}
