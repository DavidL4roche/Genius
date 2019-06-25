using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJPrésent : MonoBehaviour {
    public PNJ SonPNJ;
    public Mission SaMission;

    public PNJPrésent(int idpnj, int idmission)
    {
       SonPNJ = PNJ.trouverSonPNJ(idpnj);
       SaMission = Mission.trouverSaMission(idmission);
       //Debug.Log("PNJ présent");
    }
    public PNJPrésent(int nonpresent)
    {
        SonPNJ = new PNJ(0, "", 1);
    }
}
