using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDivertissementPrésente : MonoBehaviour {
    public Quartier SonQuartier;
    public MissionDivertissement SonDivertissement;

    public MissionDivertissementPrésente(int idq, int idd)
    {
        SonQuartier = trouverSonQuartier(idq);
        SonDivertissement = trouverSonDivertissement(idd);
    }
    Quartier trouverSonQuartier(int idq)
    {
        Quartier quartier = null;
        for (int i = 0; i < RessourcesBdD.listeDesQuartiers.Length; ++i)
        {
            if (idq == RessourcesBdD.listeDesQuartiers[i].IDQuartier)
            {
                quartier = RessourcesBdD.listeDesQuartiers[i];
            }
        }
        return quartier;
    }
    MissionDivertissement trouverSonDivertissement(int idd)
    {
        MissionDivertissement md = null;
        for (int i = 0; i < RessourcesBdD.listeDesDivertissements.Length; ++i)
        {
            if (idd == RessourcesBdD.listeDesDivertissements[i].IDMissionD)
            {
                md = RessourcesBdD.listeDesDivertissements[i];
            }
        }
        return md;
    }
}
