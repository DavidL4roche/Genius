using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour {
    public int ID;
    public string NomRessource;
    public Ressource(int id, string nom)
    {
        ID = id;
        NomRessource = nom;
    }
    public void voirRessource()
    {
        Debug.Log(ID + " -> Nom du rang -> " + NomRessource);
    }
}
