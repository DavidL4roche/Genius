using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetPrésent : MonoBehaviour {
    public Objet SonObjet;
    public int SonPrixOrcus;
    public int SonPrixIA;
    public ObjetPrésent(int idobjet)
    {
        SonObjet = Objet.trouverSonObjet(idobjet);
        calculerSonPrix();
    }
    public void calculerSonPrix()
    {
        switch (SonObjet.RangObjet.NomRang)
        {
            case "C":
                SonPrixOrcus = 700;
                SonPrixIA = 0;
                break;
            case "B":
                SonPrixOrcus = 3600;
                SonPrixIA = 60;
                break;
            case "A":
                SonPrixOrcus = 8500;
                SonPrixIA = 250;
                break;
            case "S":
                SonPrixOrcus = 10000;
                SonPrixIA = 1000;
                break;
            default:
                break;
        }
    }
}
