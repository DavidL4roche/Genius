using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public int IDMission;
    public string NomMission;
    public Rang RangMission;
    public Entreprise MissionEntreprise;
    public Compétence[] CompétencesRequises;
    public Duree SaDurée;
    public Gain[] SesGains;
    public Perte[] SesPertes;
    public Objet[] SesObjets;
    public Artefact SonArtefact;
    public int NiveauDeLaMission;
    public Mission(int id, string nom, int idrang, int idcomp1, int idcomp2, int idcomp3, int idcomp4, int idcomp5)
    {
        IDMission = id;
        NomMission = nom;
        RangMission = Rang.trouverSonRang(idrang);
        CompétencesRequises = trouverSesCompétences(idcomp1,idcomp2,idcomp3,idcomp4,idcomp5);
        SaDurée = Duree.genereTemps();
        NiveauDeLaMission = calculDuNiveau();
    }
    Compétence[] trouverSesCompétences(int comp1, int comp2, int comp3, int comp4, int comp5)
    {
        int total = 0;
        int[] listecompparam = { comp1, comp2, comp3, comp4, comp5 };
        for(int i = 0; i < listecompparam.Length; ++i)
        {
            if (listecompparam[i] != 0)
            {
                ++total;
            }
        }
        Compétence[] listecompbdd = new Compétence[total];
        int incrementation=0;
        for (int j = 0; j < listecompparam.Length; ++j)
        {
            for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
            {
                if (listecompparam[j] == RessourcesBdD.listeDesCompétences[i].ID)
                {
                    listecompbdd[incrementation] = new Compétence(RessourcesBdD.listeDesCompétences[i],Compétence.genereValeurComp(RangMission.NomRang));
                    ++incrementation;
                    break;
                }
            }
        }
        //Debug.Log(total +" Total pour la mission " + IDMission);
        return listecompbdd;
    }
    public void voirMission()
    {
        Debug.Log("ID de la mission ->" + IDMission + " et son Rang ->" +RangMission.NomRang + ", sa durée ->"+SaDurée.NomDuree);
        for(int i = 0; i < CompétencesRequises.Length; ++i)
        {
            Debug.Log("Nom de la comp ->" + CompétencesRequises[i].NomCompétence + " et sa valeur ->" + CompétencesRequises[i].Valeur);
        }
    }
    int calculDuNiveau()
    {
        int total = 0;
        for(int i = 0; i< CompétencesRequises.Length;++i)
        {
            total += CompétencesRequises[i].Valeur;
        }
        //Debug.Log(total + " Total pour la mission " + IDMission);
        return total;
    }
    public Mission(Mission mission)
    {
        IDMission = mission.IDMission;
        NomMission = mission.NomMission;
        RangMission = mission.RangMission;
        MissionEntreprise = mission.MissionEntreprise;
        CompétencesRequises = mission.CompétencesRequises;
        SaDurée = mission.SaDurée;
        NiveauDeLaMission = mission.NiveauDeLaMission;
    }
    public static Mission trouverSaMission(int idmission)
    {
        foreach (Mission mission in RessourcesBdD.listeDesMissions)
        {
            if (idmission ==mission.IDMission)
            {
                return new Mission(mission);
            }
        }
        return null;
    }
}
