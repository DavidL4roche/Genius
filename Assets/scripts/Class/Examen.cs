using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examen : MonoBehaviour {
    public int IDExamen;
    public string NomExamen;
    public Compétence[] CompétencesRequises;
    public Rang RangExamen;
    public Diplome LeDiplome;
    public Gain[] SesGains;
    public Perte[] SesPertes;
    public Examen(int id, string nom, int diplome, int comp1, int comp2, int comp3, int comp4, int comp5, int rank)
    {
        IDExamen = id;
        NomExamen = nom;
        RangExamen = Rang.trouverSonRang(rank);
        LeDiplome = Diplome.trouverSonDiplome(diplome);
        Diplome.attribuerRangDiplome(LeDiplome.IDDiplome, RangExamen.IDRang);
        CompétencesRequises = trouverSesCompétences(comp1, comp2, comp3, comp4, comp5);
    }
    Compétence[] trouverSesCompétences(int comp1, int comp2, int comp3, int comp4, int comp5)
    {
        Compétence[] listecompbdd = new Compétence[5];
        int[] listecompparam = { comp1, comp2, comp3, comp4, comp5 };
        for (int j = 0; j<listecompparam.Length; ++j)
        {
            for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
            {
                if (listecompparam[j]== RessourcesBdD.listeDesCompétences[i].ID)
                {
                    listecompbdd[j] = new Compétence(RessourcesBdD.listeDesCompétences[i],Compétence.genereValeurComp(RangExamen.NomRang));
                    break;
                }
            }
        }
        return listecompbdd;
    }

    public void toString()
    {
        Debug.Log("Examen " + IDExamen + "(" + NomExamen + ")");
    }
}
