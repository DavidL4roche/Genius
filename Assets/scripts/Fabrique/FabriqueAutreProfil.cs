using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueAutreProfil : MonoBehaviour {
    public Text LeNom;
    public Button Objet;
    public Button Competence;
    public RawImage FondObjet;
    public RawImage FondCompetence;

    public void Start()
    {
        //Color32 activeColor = new Color32(57, 119, 155, 255);
        //Color32 inactiveColor = new Color32(44, 74, 105, 255);

        AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];
        LeNom.text = joueur.SonNom;

        // Si l'action sociale a été faite
        if (Joueur.MesActionsSocialesObjet[VerificationAmi.AmiChoisi])
        {
            Objet.interactable = false;
        }

        // Si l'action sociale a été faite
        if (Joueur.MesActionsSocialesSkill[VerificationAmi.AmiChoisi])
        {
            Competence.interactable = false;
        }
    }
}
