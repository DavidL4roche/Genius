using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueValidArtefact : MonoBehaviour {

    public Text NomArtefact;
    public Text Gain;

    void Start () {
        // On affiche le nom de l'artefact
        foreach (Artefact artefact in RessourcesBdD.listeDesArtefactsJouables)
        {
            if (artefact.IDArtefact == VerifArtefact.ArtefactChoisi)
            {
                NomArtefact.text = artefact.NomArtefact;
            }
        }

        // On affiche les gains de l'artefact
        Gain.text = "Gains : ";
        switch(VerifArtefact.ArtefactChoisi)
        {
            // Artéfact Orcus
            case 1:
                Gain.text += "+10.000 Orcus";
                break;
            // Artéfact Boutique
            case 2:
                Gain.text += "Aucune action (temporaire)";
                break;
            // Artéfact IA
            case 3:
                Gain.text += "+10.000 IA";
                break;
            // Artéfact Objet
            case 4:
                Gain.text += "+1 Objet";
                break;
            // Artéfact Divertissement
            case 5:
                Gain.text += "100% Divertissement";
                break;
            // Artéfact Social
            case 6:
                Gain.text += "100% Social";
                break;
            default:
                break;

        }
    }
}
