using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionPrésente : MonoBehaviour {
    public Quartier SonQuartier;
    public Mission SaMission;
    public MissionPrésente(int idq, int idm, int identreprise)
    {
        SonQuartier = Quartier.trouverSonQuartier(idq);
        SaMission = Mission.trouverSaMission(idm);
        SaMission.MissionEntreprise = Entreprise.trouverSonEntreprise(identreprise);
    }

    public void toString()
    {
        Debug.Log("MissionPrésente " + SaMission.NomMission + "(" + SonQuartier.NomQuartier + ")");
    }
}
