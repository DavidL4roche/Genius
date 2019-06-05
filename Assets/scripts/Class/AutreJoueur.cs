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
    public int[] SesDiplomes;
    public AutreJoueur(int id, string nom)
    {
        SonID = id;
        SonNom = nom;
    }
    public void trouverToutesInformations()
    {
        majObjet();
        majComp();
        majDiplomes();
    }

    // Mise à jour objets ami
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

    // Mise à jour compétences ami
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

    // Mise à jour diplômes ami
    void majDiplomes()
    {
        SesDiplomes = new int[RessourcesBdD.listeDesDiplomes.Length];
        string requete = "SELECT diplom_pc.IDDiplom AS IDDiplom, diplom.DiplomName AS DiplomName FROM diplom_pc, diplom WHERE diplom_pc.IDDiplom = diplom.IDDiplom AND IDPCharacter = " + SonID;
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        while (lien.Read())
        {
            for (int i = 0; i < RessourcesBdD.listeDesDiplomes.Length; ++i)
            {
                if (Int32.Parse(lien["IDDiplom"].ToString()) == RessourcesBdD.listeDesDiplomes[i].IDDiplome)
                {
                    SesDiplomes[i] = 1;
                }
                else
                {
                    SesDiplomes[i] = 0;
                }
            }
        }
        lien.Close();
    }
}
