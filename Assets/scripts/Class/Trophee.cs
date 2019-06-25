using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophee : MonoBehaviour {
    public int IDTrophee;
    public Rang Rang;
    public Texture texture;
    public string Description;

    public Trophee(int idt, int idr, string descr)
    {
        IDTrophee = idt;
        for(int i = 0 ; i < RessourcesBdD.listeDesRangs.Length; ++i)
        {
            if(idr == RessourcesBdD.listeDesRangs[i].IDRang)
            {
                Rang = RessourcesBdD.listeDesRangs[i];
            }
        }
        Description = descr;
        trouverSaTexture();
    }
    public void trouverSaTexture()
    {
        switch (Rang.NomRang)
        {
            case "C":
                texture = RessourcesGraphiques.TropheeEnBronze;
                break;
            case "B":
                texture = RessourcesGraphiques.TropheeEnArgent;
                break;
            case "A":
                texture = RessourcesGraphiques.TropheeEnOr;
                break;
            case "S":
                texture = RessourcesGraphiques.TropheeEnPlatine;
                break;
        }
    }

    public void toString()
    {
        Debug.Log("Trophée " + IDTrophee + "(" + Rang.NomRang + ") - Description " + Description);
    }
}
