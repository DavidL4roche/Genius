using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueAutreProfil : MonoBehaviour {
    public Text LeNom;
    public void Start()
    {
        AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];
        LeNom.text = joueur.SonNom;  
    }
}
