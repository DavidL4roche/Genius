using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gain : MonoBehaviour {
    public int IDGain;
    public string NomGain;
    public int ValeurDuGain;
    public Gain(int id, string nom)
    {
        IDGain = id;
        NomGain = nom;
        ValeurDuGain = 0;
    }
    public Gain(Gain gain, int valeur)
    {
        IDGain = gain.IDGain;
        NomGain = gain.NomGain;
        ValeurDuGain = valeur;
    }
    public void voirGain()
    {
        Debug.Log("Gain numéro " + IDGain + " -> Nom ->" + NomGain + " Valeur -> " + ValeurDuGain);
    }
    public static void calculDesGains(Mission mission)
    {
        Gain[] tabDeGains = new Gain[RessourcesBdD.listeDesGains.Length];
        for (int i = 0; i < RessourcesBdD.listeDesGains.Length; ++i)
        {
            switch (RessourcesBdD.listeDesGains[i].NomGain)
            {
                case "Compétence":
                    tabDeGains[i]= new Gain(RessourcesBdD.listeDesGains[i],calculGainCompétence(mission));
                    break;
                case "Orcus":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], calculGainOrcus(mission));
                    break;
                case "IA":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], calculGainIA(mission));
                    break;
                case "Objet":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], calculGainObjets(mission));
                    break;
                default:
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
            }
        }
        mission.SesGains = tabDeGains;
    }

    public static void calculDesGainsPNJ(Mission mission)
    {
        Gain[] tabDeGains = new Gain[RessourcesBdD.listeDesGains.Length];
        for (int i = 0; i < RessourcesBdD.listeDesGains.Length; ++i)
        {
            switch (RessourcesBdD.listeDesGains[i].NomGain)
            {
                case "Compétence":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
                case "Orcus":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 5000);
                    break;
                case "IA":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 5000);
                    break;
                case "Objet":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
                default:
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
            }
        }
        mission.SesGains = tabDeGains;
    }

    static int calculGainCompétence(Mission mission)
    {
        int TotCompJoueur = 0;
        for (int i = 0; i < Joueur.MesValeursCompetences.Length; ++i)
        {
            for (int j = 0; j < mission.CompétencesRequises.Length; ++j)
            {
                if (RessourcesBdD.listeDesCompétences[i].ID == mission.CompétencesRequises[j].ID && mission.CompétencesRequises[j].ID != 0)
                {
                    TotCompJoueur += Joueur.MesValeursCompetences[i];
                    break;
                }
            }
        }
        int val = (int)(((Mathf.Max((200 - mission.NiveauDeLaMission - TotCompJoueur), 0) + 1) / 100) * (FicheAmélioration.Optimisation + FicheAmélioration.BonusOComp));
        return val;
    }
    static int calculGainOrcus(Mission mission)
    {
        float test2 = (Joueur.MesRessources[2] + Joueur.MesRessources[3] + FicheAmélioration.BonusOOrcus);
        float test = (test2 / 200F);
        //Debug.Log(test + " Valeur de test" + test2);
        return (int)(mission.NiveauDeLaMission * (mission.SaDurée.ValeurDuree + FicheAmélioration.BonusODuree) * mission.MissionEntreprise.TailleEntreprise * test * FicheAmélioration.Optimisation / 2);
    }
    static int calculGainIA(Mission mission)
    {
        switch (mission.RangMission.NomRang)
        {
            case "B":
                return 1;
            case "B+":
                return 10;
            case "A":
                return 5;
            case "A+":
                return 50;
            case "S":
                return 25;
            case "S+":
                return 250;
            default:
                return 0;
        }
    }
    static int calculGainObjets(Mission mission)
    {
        int[] RangObjetsGains= new int[3];
        int NombreJets = 0;
        int ValeurRand = Random.Range(0, 100);
        if (ValeurRand > 84)
        {
            NombreJets = 3;
        }
        else if (ValeurRand < 85 && ValeurRand > 69)
        {
            NombreJets = 2;
        }
        else if (ValeurRand < 70 && ValeurRand > 29)
        {
            NombreJets = 1;
        }
        else
        {
            NombreJets = 0;
        }
        //Debug.Log("Nombre de jets pour les objets -> " + NombreJets);
        for (; NombreJets > 0; --NombreJets)
        {
            ValeurRand = Random.Range(0, 100);
            if (ValeurRand < 65)
            {
                RangObjetsGains[NombreJets - 1] = 0;
            }
            else if (ValeurRand < 85 && ValeurRand > 64)
            {
                RangObjetsGains[NombreJets - 1] = 1;
            }
            else if (ValeurRand < 94 && ValeurRand > 84)
            {
                RangObjetsGains[NombreJets - 1] = 2;
            }
            else if (ValeurRand < 100 && ValeurRand > 93)
            {
                RangObjetsGains[NombreJets - 1] = 3;
            }
            else
            {
                RangObjetsGains[NombreJets - 1] = 3;
            }
        }
        int nbObj = 0;
        for (int i = 0; i < RangObjetsGains.Length; ++i)
        {
            if (RangObjetsGains[i] != 0)
            {
                ++nbObj;
            }
        }
        mission.SesObjets = new Objet[nbObj];
        int incre = 0;
        for (int i = 0; i < RangObjetsGains.Length; ++i)
        {
            //Debug.Log("Rang de l'objet gain n." + (i + 1) + " de rang " + RangObjetsGains[i]);
            if (RangObjetsGains[i] != 0)
            {
                Objet id = Objet.ChercherUnObjetRandomParRang(RangObjetsGains[i]);
                mission.SesObjets[incre] = id;
                //Debug.Log("ID de l'objet gain n." + (i + 1) + " d'id " + RangObjetsGains[i] +" a été inséré pour la mission " +IDMission);
                ++incre;
            }
        }
        return nbObj;
    }
    public static void calculDesGains(Examen examen)
    {
        Gain[] tabDeGains = new Gain[RessourcesBdD.listeDesGains.Length];
        for (int i = 0; i< RessourcesBdD.listeDesGains.Length; ++i)
        {
            switch (RessourcesBdD.listeDesGains[i].NomGain)
            {
                case "Diplome":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 1);
                    break;
                case "Compétence":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 5);
                    break;
                default:
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
            }
        }
        examen.SesGains = tabDeGains;
    }
    public static void calculDesGains(MissionDivertissement divert)
    {
        Gain[] tabDeGains = new Gain[RessourcesBdD.listeDesGains.Length];
        for (int i = 0; i < RessourcesBdD.listeDesGains.Length; ++i)
        {
            switch (RessourcesBdD.listeDesGains[i].NomGain)
            {
                case "Divertissement":
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], calculGainDivertissement(divert));
                    break;
                default:
                    tabDeGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 0);
                    break;
            }
        }
        divert.SesGains = tabDeGains;
    }
    static int calculGainDivertissement(MissionDivertissement divert)
    {
        switch (divert.SonRang.NomRang)
        {
            case "C":
                return 5;
            case "B":
                return 10;
            case "A":
                return 20;
            case "S":
                return 40;
            default:
                return 0;
        }
    }
    public static void attribuerUnArtefact(Mission mission, Artefact artefact)
    {
        for (int i = 0; i < RessourcesBdD.listeDesGains.Length; ++i)
        {
            switch (RessourcesBdD.listeDesGains[i].NomGain)
            {
                case "Artefact":
                    mission.SesGains[i] = new Gain(RessourcesBdD.listeDesGains[i], 1);
                    mission.SonArtefact = new Artefact(artefact);
                    break;
                default:
                    break;
            }
        }
    }
    public static void utiliserUnArtefact()
    {
        Artefact artefact = RessourcesBdD.listeDesArtefactsJouables[VerifArtefactCliquer.ArtefactChoisi];
        switch (artefact.SonBonus.NomBonus)
        {
            case "Orcus":
                for (int i = 0; i< RessourcesBdD.listeDesRessources.Length; ++i)
                {
                    if (RessourcesBdD.listeDesRessources[i].NomRessource == "Orcus")
                    {
                        Joueur.MesRessources[i] += 10000;
                        break;
                    }
                }
                break;
            case "Divertissement":
                for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
                {
                    if (RessourcesBdD.listeDesRessources[i].NomRessource == "Divertissement")
                    {
                        Joueur.MesRessources[i] = 100;
                        break;
                    }
                }
                break;
            case "Social":
                for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
                {
                    if (RessourcesBdD.listeDesRessources[i].NomRessource == "Social")
                    {
                        Joueur.MesRessources[i] = 100;
                        break;
                    }
                }
                break;
            case "IA":
                for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
                {
                    if (RessourcesBdD.listeDesRessources[i].NomRessource == "IA")
                    {
                        Joueur.MesRessources[i] += 5000;
                        break;
                    }
                }
                break;
            case "Objet":
                gainObjetAvecArtefact();
                break;
            default:
                break;
        }
        string requete = "Insert INTO artefact_used VALUES (" + artefact.IDArtefact + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
    }
    public static void gainObjetAvecArtefact()
    {
        for (int i = 0; i < 5; ++i)
        {
            string RangObjet = "";
            int nbRandom = Random.Range(0, 100);
            if (nbRandom < 84)
            {
                RangObjet = "C";
            }
            else if (89 > nbRandom && nbRandom > 84)
            {
                RangObjet = "B";
            }
            else if(95 > nbRandom && nbRandom > 89)
            {
                RangObjet = "A";
            }
            else
            {
                RangObjet = "S";
            }
            Objet ObjetRandom = RessourcesBdD.randomObjetAvecRang(RangObjet);
            for(int j =0; j < RessourcesBdD.listeDesObjets.Length; ++j)
            {
                if (RessourcesBdD.listeDesObjets[j].Nom ==  ObjetRandom.Nom)
                {
                    ++Joueur.MesObjets[j];
                    break;
                }
            }
        }
    }

    public void toString()
    {
        Debug.Log("Gain " + IDGain + "(" + NomGain + ") - Valeur " + ValeurDuGain);
    }
}
