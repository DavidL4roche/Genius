using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duree : MonoBehaviour {
    public int IDDuree;
    public string NomDuree;
    public int ValeurDuree;
    public Duree(int id, string nom, int val)
    {
        IDDuree = id;
        NomDuree = nom;
        ValeurDuree = val;
    }
    public static Duree genereTemps()
    {
        int i = 1;
        int valeur = Random.Range(0, 100);
        if (28 < valeur && valeur < 47)
        {
            i = 2;
        }
        else if (46 < valeur && valeur < 62)
        {
            i = 3;
        }
        else if (61 < valeur && valeur < 74)
        {
            i = 4;
        }
        else if (73 < valeur && valeur < 83)
        {
            i = 5;
        }
        else if (82 < valeur && valeur < 90)
        {
            i = 6;
        }
        else if (89 < valeur && valeur < 95)
        {
            i = 7;
        }
        else if (94 < valeur && valeur < 98)
        {
            i = 8;
        }
        else if (97 < valeur && valeur < 100)
        {
            i = 9;
        }
        else
        {
            i = 10;
        } 
        return RessourcesBdD.listeDesDurees[i-1];
    }

    public void toString()
    {
        Debug.Log("Durée " + IDDuree + "(" + NomDuree + ") - Valeur " + ValeurDuree);
    }
}
