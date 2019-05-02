using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueProfil : MonoBehaviour {
    public Button ArtefactOrcus;
    public Button ArtefactIA;
    public Button ArtefactObjet;
    public Button ArtefactBoutique;
    public Button ArtefactSocial;
    public Button ArtefactDivertissement;

    public Text NomJoueur;

    public void Start()
    {
        int totalArtefact = 0;
        NomJoueur.text = Joueur.NomJoueur;
        for (int i = 0; i < RessourcesBdD.listeDesArtefactsJouables.Length; ++i)
        {
            switch(RessourcesBdD.listeDesArtefactsJouables[i].IDArtefact)
            {
                case 1:
                    ArtefactOrcus.interactable = true;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    break;

            }
        }
    }
}
