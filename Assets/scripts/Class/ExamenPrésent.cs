using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamenPrésent : MonoBehaviour {
    public Lieu SonLieu;
    public Examen SonExamen;

    public ExamenPrésent(int idl, int ide)
    {
        SonExamen = trouverSonExamen(ide);
        SonLieu = trouverSonLieu(idl);
    }
    Examen trouverSonExamen(int ide)
    {
        Examen examen = null;
        for (int i = 0; i < RessourcesBdD.listeDesExamens.Length; ++i)
        {
            if (ide == RessourcesBdD.listeDesExamens[i].IDExamen)
            {
                examen = RessourcesBdD.listeDesExamens[i];
            }
        }
        return examen;
    }
    Lieu trouverSonLieu(int idl)
    {
        Lieu lieu = null;
        for (int i = 0; i < RessourcesBdD.listeDesLieux.Length; ++i)
        {
            if (idl == RessourcesBdD.listeDesLieux[i].IDLieu)
            {
                lieu = RessourcesBdD.listeDesLieux[i];
            }
        }
        return lieu;
    }

    public void toString()
    {
        Debug.Log("Examen présent " + SonExamen.NomExamen + "(Lieu : " + SonLieu.NomLieu + ")");
    }
}
