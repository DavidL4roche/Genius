using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueValidMagasin : MonoBehaviour {

    public Text NomObjet;
    public Text CoutIA;
    public Text CoutOrcus;

    void Start () {
        NomObjet.text = RessourcesBdD.LeMagasin[VerificationObjet.ObjetChoisi].SonObjet.Nom;
        CoutIA.text = RessourcesJoueur.getPriceInK(RessourcesBdD.LeMagasin[VerificationObjet.ObjetChoisi].SonPrixOrcus);
        CoutOrcus.text = RessourcesJoueur.getPriceInK(RessourcesBdD.LeMagasin[VerificationObjet.ObjetChoisi].SonPrixIA);
    }
}
