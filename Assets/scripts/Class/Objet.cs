using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objet : MonoBehaviour
{
    public int ID;
    public string Nom;
    public Rang RangObjet;
    public Bonus Bonus;
    public int Valeur;

    public Objet(int id, string name, int rank,int idbonus, int valeur)
    {
        ID = id;
        Nom = name;
        Bonus = Bonus.trouverSonBonus(idbonus);
        Valeur = valeur;
        RangObjet = Rang.trouverSonRang(rank);
    }
    public void voirObjet()
    {
        Debug.Log(ID+" -> Nom de l'objet ->"+Nom);
    }
    public static Objet ChercherUnObjetRandomParRang(int rang)
    {
        Objet objet = null;
        int random = Random.Range(0, RessourcesBdD.listeDesObjets.Length);
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            if (random == i)
            {
                objet = RessourcesBdD.listeDesObjets[i];
                break;
            }
        }
        return objet;
    }
    public Objet(Objet obj)
    {
        ID = obj.ID;
        Nom = obj.Nom;
        RangObjet = obj.RangObjet;
        Bonus = obj.Bonus;
        Valeur = obj.Valeur;
    }
    public static Objet trouverSonObjet(int id)
    {
        foreach (Objet obj in RessourcesBdD.listeDesObjets)
        {
            if (obj.ID == id)
            {
                return new Objet(obj);
            }
        }
        return null;
    }

    public void toString()
    {
        Debug.Log("Objet " + ID + "(" + Nom + ") - Rang " + RangObjet + " - Bonus " + Bonus + " Valeur " + Valeur);
    }
}
