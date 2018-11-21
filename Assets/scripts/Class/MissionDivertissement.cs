using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDivertissement : MonoBehaviour {
    public int IDMissionD;
    public string NomDivertissement;
    public Rang SonRang;
    public Gain[] SesGains;
    public Perte[] SesPertes;

    public MissionDivertissement(int id, string nom, int rang)
    {
        IDMissionD = id;
        NomDivertissement = nom;
        SonRang = trouverSonRang(rang);
    }
    public Rang trouverSonRang(int rank)
    {
        Rang lerang = null;
        for (int i = 0; i < RessourcesBdD.listeDesRangs.Length; ++i)
        {
            if (rank == RessourcesBdD.listeDesRangs[i].IDRang)
            {
                lerang = RessourcesBdD.listeDesRangs[i];
                break;
            }
        }
        return lerang;
    }
}
