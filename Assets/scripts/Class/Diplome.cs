using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diplome : MonoBehaviour {
    public int IDDiplome;
    public string NomDiplome;
    public Rang SonRang;
    public Diplome(int id, string nom)
    {
        IDDiplome = id;
        NomDiplome = nom;
    }
    public void voirDiplome()
    {
        Debug.Log("ID du diplome = "+IDDiplome + " , son nom = "+NomDiplome);
    }
    public Diplome(Diplome diplome)
    {
        IDDiplome = diplome.IDDiplome;
        NomDiplome = diplome.NomDiplome;
    }
    public static Diplome trouverSonDiplome(int iddiplome)
    {
        foreach (Diplome diplome in RessourcesBdD.listeDesDiplomes)
        {
            if(iddiplome == diplome.IDDiplome)
            {
                return new Diplome(diplome);
            }
        }
        return null;
    }
    public static void attribuerRangDiplome(int iddiplome, int rang)
    {
        foreach (Diplome diplome in RessourcesBdD.listeDesDiplomes)
        {
            if (iddiplome == diplome.IDDiplome)
            {
                diplome.SonRang = Rang.trouverSonRang(rang);
            }
        }
    }

    public void toString()
    {
        Debug.Log("Diplome " + IDDiplome + "(" + NomDiplome + ")");
    }
}
