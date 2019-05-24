﻿using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatDivert : MonoBehaviour {

    MissionDivertissement divert = SpawnerMission.SonDivertissement;
    public Text gainDivert;
    public Text perteOrcus;

    GameObject instance;

    public void Start()
    {
        //mission.voirRessources();
        AGagner();
        APerdu();
        blockdesprerequis();
        ReloadMissions();
        RessourcesBdD.DestroyListeDivertissement();
        RessourcesBdD.recupDivertJouable();

        // On transfert en base
        Joueur.transfertRessourcesEnBase();
    }

    public void blockdesprerequis()
    {
        for (int i = 0; i < divert.SesGains.Length; ++i)
        {
            if (divert.SesGains[i].NomGain == "Divertissement")
            {
                gainDivert.text = "+" + divert.SesGains[i].ValeurDuGain.ToString() + "%";
            }
        }
        for (int i = 0; i < divert.SesPertes.Length; ++i)
        {
            if (divert.SesPertes[i].NomPerte == "Orcus")
            {
                perteOrcus.text = "-" + divert.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
            }
            else
            {
                continue;
            }
        }
    }
    void AGagner()
    {
        for (int i = 0; i < divert.SesGains.Length; ++i)
        {
            if (divert.SesGains[i].ValeurDuGain != 0)
            {
                switch (divert.SesGains[i].NomGain)
                {
                    case "Orcus":
                    case "IA":
                    case "Divertissement":
                    case "Social":
                        gainRess(divert.SesGains[i].NomGain, divert.SesGains[i].ValeurDuGain);
                        break;
                    case "Compétence":
                    case "Objet":
                    default:
                        break;
                }
            }
        }
    }
    void gainRess(string NomRess, int valeur)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (NomRess == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                Joueur.MesRessources[i] = Joueur.MesRessources[i] + valeur;
                if ((NomRess == "Social" || NomRess == "Divertissement") && Joueur.MesRessources[i] > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
            }
        }
    }
    void APerdu()
    {
        //Ressources
        for (int i = 0; i < divert.SesPertes.Length; ++i)
        {
            if (divert.SesPertes[i].ValeurDeLaPerte != 0)
            {
                for (int j = 0; j < RessourcesBdD.listeDesRessources.Length; ++j)
                {
                    if (RessourcesBdD.listeDesRessources[j].NomRessource == divert.SesPertes[i].NomPerte)
                    {
                        Joueur.MesRessources[j] = Joueur.MesRessources[j] - divert.SesPertes[i].ValeurDeLaPerte;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
    public void ReloadMissions()
    {
        string requete = "Insert INTO entertainment_done VALUES (" + divert.IDMissionD + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
    }
}

