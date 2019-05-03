using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compétence : MonoBehaviour {
    public int ID;
    public string NomCompétence;
    public int Valeur = 0;
    public string Description;
    public Compétence(int id, string nomcomp, string description)
    {
        ID = id;
        NomCompétence = nomcomp;
        Valeur = 0;
        Description = description;
    }

    // Permet d'attribuer une valeur à une compétence existante
    public Compétence(Compétence comp, int val)
    {
        ID = comp.ID;
        NomCompétence = comp.NomCompétence;
        Valeur = val;
    }
    public void voirCompétence()
    {
        Debug.Log(ID + " -> Nom de la compétence ->" + NomCompétence +  " sa valeur ->" + Valeur);
    }
    static public int genereValeurComp(string rang)
    {
        int valeur = 0;
        switch (rang)
        {
            case "S":
            case "S+":
                valeur = Random.Range(85, 100);
                break;
            case "A":
            case "A+":
                valeur = Random.Range(60, 85);
                break;
            case "B":
            case "B+":
                valeur = Random.Range(20, 60);
                break;
            case "C":
            case "C+":
                valeur = 0;
                break;
            default:
                break;
        }
        return valeur;
    }
}
