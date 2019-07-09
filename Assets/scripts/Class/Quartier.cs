using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quartier : MonoBehaviour {
    public int IDQuartier;
    public string NomQuartier;
	public Quartier(int idq, string nomq)
    {
        IDQuartier = idq;
        NomQuartier = nomq;
    }
    public Quartier(Quartier quartier)
    {
        IDQuartier = quartier.IDQuartier;
        NomQuartier = quartier.NomQuartier;
    }
    public static Quartier trouverSonQuartier(int id)
    {
        foreach (Quartier quartier in RessourcesBdD.listeDesQuartiers)
        {
            if (id == quartier.IDQuartier)
            {
                return new Quartier(quartier);
            }
        }
        return null;
    }

    public void toString()
    {
        Debug.Log("Quartier " + IDQuartier + "(" + NomQuartier + ")");
    }
}
