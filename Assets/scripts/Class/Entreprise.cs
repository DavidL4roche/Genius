using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entreprise : MonoBehaviour {
    public int IDEntreprise;
    public string NomEntreprise;
    public int TailleEntreprise;
    public Quartier[] SesEmplacements;
    public Quartier[] SesSpécialisations;
    public Entreprise(int id, string nom, int taille)
    {
        IDEntreprise = id;
        NomEntreprise = nom;
        TailleEntreprise = taille;
    } 
    public Entreprise(Entreprise entreprise)
    {
        IDEntreprise = entreprise.IDEntreprise;
        NomEntreprise = entreprise.NomEntreprise;
        TailleEntreprise = entreprise.TailleEntreprise;
        SesEmplacements = entreprise.SesEmplacements;
        SesSpécialisations = entreprise.SesSpécialisations;
    }
    public static Entreprise trouverSonEntreprise(int identreprise)
    {
        foreach (Entreprise entreprise in RessourcesBdD.listeDesEntreprises)
        {
            if (identreprise == entreprise.IDEntreprise)
            {
                return new Entreprise(entreprise);
            }
        }
        return null;
    }

    public void toString()
    {
        Debug.Log("Entreprise " + IDEntreprise + "(" + NomEntreprise + ") - Taille " + TailleEntreprise);
    }
}
