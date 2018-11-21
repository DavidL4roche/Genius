using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutreJoueur : MonoBehaviour {
    public int SonID;
    public string SonNom;
    public int[] SesCompétences;
    public int[] SesObjets;
    public AutreJoueur(int id, string nom)
    {
        SonID = id;
        SonNom = nom;
    }
    public void trouverToutesInformations()
    {
        majObjet();
        majComp();
    }
    void majObjet()
    {
        SesObjets = new int[RessourcesBdD.listeDesObjets.Length];
        string requete = "SELECT IDItem, Quantity FROM item_pc WHERE IDPCharacter=" + SonID;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if ((int)lien["IDItem"] == RessourcesBdD.listeDesObjets[i].ID)
                {
                    SesObjets[i] = (int)lien["Quantity"];
                    //Debug.Log("Je possède "+MesObjets[i]+ " " + RessourcesBdD.listeNomObjets[i]);
                }
            }
        }
        lien.Close();
    }
    void majComp()
    {
        SesCompétences = new int[RessourcesBdD.listeDesCompétences.Length];
        string requete = "SELECT * FROM skill_pc WHERE IDPCharacter=" + SonID;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < SesCompétences.Length; ++i)
            {
                if (Int32.Parse(lien["IDSkill"].ToString()) == RessourcesBdD.listeDesCompétences[i].ID)
                {
                    SesCompétences[i] = Int32.Parse(lien["SkillLevel"].ToString());
                }
                else
                {
                    continue;
                }
            }
        }
        lien.Close();
    }
}
