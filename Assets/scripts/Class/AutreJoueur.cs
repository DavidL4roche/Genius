using MySql.Data.MySqlClient;
using SimpleJSON;
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
    
    private string url = Configuration.url + "scripts/";
    private string urlRess = Configuration.url + "scripts/scriptsRessources/";

    private bool continueComp = false;
    private bool continueDiplome = false;

    public AutreJoueur(int id, string nom)
    {
        SonID = id;
        SonNom = nom;
    }

    // Met à jour les objets de l'ami
    public IEnumerator majObjetAmi(int idJoueur)
    {
        SesObjets = new int[RessourcesBdD.listeDesObjets.Length];
        string urlComp = urlRess + "MAJObjet.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesObjets.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDItem"] == RessourcesBdD.listeDesObjets[j].ID)
                {
                    SesObjets[j] = (int)Node["msg"][i]["Quantity"];
                    //if (MesObjets[i] > 0)
                    //Debug.Log("Je possède "+ MesObjets[i] + " " + RessourcesBdD.listeDesObjets[j].Nom);
                }
            }
        }
        continueComp = true;
    }

    // Met à jour les compétences de l'ami du joueur
    public IEnumerator majComp(int idJoueur)
    {
        SesCompétences = new int[RessourcesBdD.listeDesCompétences.Length];
        string urlComp = urlRess + "MAJComp.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);

        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < SesCompétences.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDSkill"] == RessourcesBdD.listeDesCompétences[j].ID)
                {
                    SesCompétences[j] = (int)Node["msg"][i]["SkillLevel"];
                    //Debug.Log("Valeur compétence Joueur " + RessourcesBdD.listeDesCompétences[j].NomCompétence + " : " + MesValeursCompetences[j]);
                }
            }
        }
        continueDiplome = true;
    }

    // Met à jour les diplomes de l'ami du joueur
    public IEnumerator majDiplome(int idJoueur)
    {
        SesDiplomes = new int[RessourcesBdD.listeDesDiplomes.Length];
        string urlComp = urlRess + "MAJDiplome.php?id=" + idJoueur;

        WWW dl = new WWW(urlComp);
        yield return dl;

        string Json = dl.text;
        JSONNode Node = JSON.Parse(Json);
        for (int i = 0; i < Node["msg"].Count; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesDiplomes.Length; ++j)
            {
                if ((int)Node["msg"][i]["IDDiplom"] == RessourcesBdD.listeDesDiplomes[j].IDDiplome)
                {
                    SesDiplomes[j] = 1;
                    //Debug.Log("Diplome Joueur " + RessourcesBdD.listeDesDiplomes[j].NomDiplome + " : " + MesDiplomes[j]);
                }
                else
                {
                    SesDiplomes[j] = 0;
                }
            }
        }
    }
}
