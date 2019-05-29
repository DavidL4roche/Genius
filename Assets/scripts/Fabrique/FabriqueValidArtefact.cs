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
                IconeArtefact.texture = Resources.Load<Texture>("icones/Artefact" + trouverArtefact(artefact.IDArtefact));
                NomArtefact.text = artefact.NomArtefact;
                IconeGain.color = new Color32(81, 203, 255, 255);
                ValeurGain.color = new Color32(81, 203, 255, 255);
            }
        }

        // On affiche les gains de l'artefact
        ValeurGain.text = "";
        switch(VerifArtefact.ArtefactChoisi)
        {
            // Artéfact Orcus
            case 1:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_Orcus");
                ValeurGain.text += "+10k";
                break;
            // Artéfact Boutique
            case 2:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_Magasin"); // A CHANGER
                ValeurGain.text += "0";
                Equiper.interactable = false;
                break;
            // Artéfact IA
            case 3:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_IA"); // A CHANGER
                ValeurGain.text += "+10k";
                break;
            // Artéfact Objet
            case 4:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_Objets"); // A CHANGER
                ValeurGain.text += "+1";
                break;
            // Artéfact Divertissement
            case 5:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_Divertissement"); // A CHANGER
                ValeurGain.text += "100%";
                break;
            // Artéfact Social
            case 6:
                IconeGain.texture = Resources.Load<Texture>("icones/Artefact_Social"); // A CHANGER
                ValeurGain.text += "100%";
                break;
            default:
                break;

        }
    }

    string trouverArtefact(int IDArtefact)
    {
        switch (IDArtefact)
        {
            case 1:
                return "Orcus";
            case 2:
                return "Boutique";
            case 3:
                return "IA";
            case 4:
                return "Objets";
            case 5:
                return "Divertissement";
            case 6:
                return "Social";
            default:
                return null;
        }
    }
}
