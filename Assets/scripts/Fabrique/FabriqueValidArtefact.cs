using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueValidArtefact : MonoBehaviour {

    public RawImage IconeArtefact;
    public Text NomArtefact;
    public RawImage IconeGain;
    public Text ValeurGain;
    public Button Equiper;

    void Start () {
        Equiper.interactable = true;

        // On affiche le nom et l'icone de l'artefact
        foreach (Artefact artefact in RessourcesBdD.listeDesArtefactsJouables)
        {
            if (artefact.IDArtefact == VerifArtefact.ArtefactChoisi)
            {
                IconeArtefact.texture = Resources.Load<Texture>("icones/Artefact" + FabriqueMissionPNJ.trouverArtefact(artefact.IDArtefact));
                NomArtefact.text = artefact.NomArtefact;
            }
        }

        // On affiche les gains de l'artefact
        ValeurGain.text = "";
        switch(VerifArtefact.ArtefactChoisi)
        {
            // Artéfact Orcus
            case 1:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_orcus");
                ValeurGain.text += "+10.000";
                break;
            // Artéfact Boutique
            case 2:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_durée"); // A CHANGER
                ValeurGain.text += "0";
                Equiper.interactable = false;
                break;
            // Artéfact IA
            case 3:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_durée"); // A CHANGER
                ValeurGain.text += "+10.000";
                break;
            // Artéfact Objet
            case 4:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_orcus"); // A CHANGER
                ValeurGain.text += "+1";
                break;
            // Artéfact Divertissement
            case 5:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_durée"); // A CHANGER
                ValeurGain.text += "100%";
                break;
            // Artéfact Social
            case 6:
                IconeGain.texture = Resources.Load<Texture>("icones/IconM_durée"); // A CHANGER
                ValeurGain.text += "100%";
                break;
            default:
                break;

        }
    }
}
