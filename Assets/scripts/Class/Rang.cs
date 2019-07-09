using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rang : MonoBehaviour
{
    public int IDRang;
    public string NomRang;
    public Texture texture;
    public Rang(int id, string rank)
    {
        IDRang = id;
        NomRang = rank;
        trouverSaTexture();
    }
    public void trouverSaTexture()
    {
        switch (NomRang)
        {
            case "C":
            case "C+":
                texture = RessourcesGraphiques.GradeC;
                break;
            case "B":
            case "B+":
                texture = RessourcesGraphiques.GradeB;
                break;
            case "A":
            case "A+":
                texture = RessourcesGraphiques.GradeA;
                break;
            case "S":
            case "S+":
                texture = RessourcesGraphiques.GradeS;
                break;
        }
    }
    public void voirRang()
    {
        Debug.Log(IDRang + " -> Nom du rang -> "+NomRang);
    }
    public Rang(Rang rang)
    {
        IDRang = rang.IDRang;
        NomRang = rang.NomRang;
        texture = rang.texture;
    }
    public static Rang trouverSonRang(int idrang)
    {
        foreach(Rang rang in RessourcesBdD.listeDesRangs)
        {
            if(idrang == rang.IDRang)
            {
                return new Rang(rang);
            }
        }
        return null;
    }

    public void toString()
    {
        Debug.Log("Rang " + IDRang + "(" + NomRang + ")");
    }
}
