using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJ : MonoBehaviour
{
    public int IDPNJ;
    public string NomPNJ;
    public Artefact SonArtefact;
    public Quartier SonQuartier;
    public PNJ(int id, string nom, int artefact)
    {
        IDPNJ = id;
        NomPNJ = nom;
        SonArtefact = Artefact.trouverSonArtefact(artefact);
    }
    public PNJ(PNJ pnj)
    {
        IDPNJ = pnj.IDPNJ;
        NomPNJ = pnj.NomPNJ;
        SonArtefact = pnj.SonArtefact;
        SonQuartier = pnj.SonQuartier;
    }
    public static PNJ trouverSonPNJ(int idpnj)
    {
        foreach (PNJ pnj in RessourcesBdD.listeDesPNJ)
        {
            if(idpnj == pnj.IDPNJ)
            {
                return new PNJ(pnj);
            }
        }
        return null;
    }
    public void associerSonQuartier(int id)
    {
        SonQuartier = Quartier.trouverSonQuartier(id);
        //voirPNJ();
    }
    public void voirPNJ()
    {
        Debug.Log("ID du PNJ ->" + IDPNJ + " , Son Nom -> "+ NomPNJ +" , et son quartier ->" +SonQuartier.IDQuartier);
    }
}
