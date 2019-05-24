using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueValidSupprimerAmi : MonoBehaviour {

    public Text phrasePseudo; 

	public void Start () {
        phrasePseudo.text = "Voulez-vous supprimer " + Joueur.MesAmis[VerificationAmi.AmiChoisi].SonNom + " de vos amis ?";
	}
}
