using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perte : MonoBehaviour {
    public int IDPerte;
    public string NomPerte;
    public int ValeurDeLaPerte;
    public Perte(int id, string nom)
    {
        IDPerte = id;
        NomPerte = nom;
        ValeurDeLaPerte = 0;
    }
    public Perte(Perte perte, int valeur)
    {
        IDPerte = perte.IDPerte;
        NomPerte = perte.NomPerte;
        ValeurDeLaPerte = valeur;
    }
    public void voirPerte()
    {
        Debug.Log("Perte numéro " + IDPerte + " -> Nom ->" + NomPerte + " Valeur -> " + ValeurDeLaPerte);
    }
    public static void calculDesPertes(Mission mission)
    {
        Perte[] tabDePertes = new Perte[RessourcesBdD.listeDesPertes.Length];
        for (int i = 0; i < RessourcesBdD.listeDesPertes.Length; ++i)
        {
            switch (RessourcesBdD.listeDesPertes[i].NomPerte)
            {
                case "Divertissement":
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i],calculPerteDivertissement(mission));
                    break;
                case "Social":
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i], calculPerteSocial(mission));
                    break;
                default:
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i], 0);
                    break;
            }
        }
        mission.SesPertes = tabDePertes;
    }
    static int calculPerteSocial(Mission mission)
    {
        return (1 - mission.MissionEntreprise.TailleEntreprise / 6) * mission.SaDurée.ValeurDuree * FicheAmélioration.Concentration * mission.NiveauDeLaMission / 100;
    }
    static int calculPerteSocial(Examen ex)
    {
        switch (ex.RangExamen.NomRang)
        {
            case "C":
                return 20;
            case "B":
                return 40;
            case "A":
                return 60;
            case "S":
                return 80;
            default:
                return 0;
        }
    }
    static int calculPerteOrcus(MissionDivertissement md)
    {
        switch (md.SonRang.NomRang)
        {
            case "B":
                return 3000;
            case "A":
                return 4500;
            case "S":
                return 8000;
            default:
                return 0;
        }
    }
    static int calculPerteDivertissement(Examen ex)
    {
        switch (ex.RangExamen.NomRang)
        {
            case "C":
                return 20;
            case "B":
                return 40;
            case "A":
                return 60;
            case "S":
                return 80;
            default:
                return 0;
        }
    }
    static int calculPerteDivertissement(Mission mission)
    {
        return (int)(mission.SaDurée.ValeurDuree * FicheAmélioration.Concentration / (1 - (float)mission.NiveauDeLaMission / (600F)));
    }
    public static void calculDesPertes(Examen examen)
    {
        Perte[] tabDePertes = new Perte[RessourcesBdD.listeDesPertes.Length];
        for (int i = 0; i < RessourcesBdD.listeDesPertes.Length; ++i)
        {
            switch (RessourcesBdD.listeDesPertes[i].NomPerte)
            {
                case "Divertissement":
                    tabDePertes[i]= new Perte(RessourcesBdD.listeDesPertes[i], calculPerteDivertissement(examen));
                    break;
                case "Social":
                    tabDePertes[i]= new Perte(RessourcesBdD.listeDesPertes[i], calculPerteSocial(examen));
                    break;
                default:
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i], 0);
                    break;
            }
        }
        examen.SesPertes = tabDePertes;
    }
    public static void calculDesPertes(MissionDivertissement md)
    {
        Perte[] tabDePertes = new Perte[RessourcesBdD.listeDesPertes.Length];
        for (int i = 0; i < RessourcesBdD.listeDesPertes.Length; ++i)
        {
            switch (RessourcesBdD.listeDesPertes[i].NomPerte)
            {
                case "Orcus":
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i], calculPerteOrcus(md));
                    break;
                default:
                    tabDePertes[i] = new Perte(RessourcesBdD.listeDesPertes[i], 0);
                    break;
            }
        }
        md.SesPertes = tabDePertes;
    }
}
