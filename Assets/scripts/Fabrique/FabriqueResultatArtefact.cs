using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatArtefact : MonoBehaviour {

    public RawImage IconeCheck;
    public RawImage IconeRessource;
    public Text ValeurGain;

	void Start () {
        IconeCheck.color = trouverCouleur(VerifArtefact.ArtefactChoisi);
        IconeRessource.color = trouverCouleur(VerifArtefact.ArtefactChoisi);
        ValeurGain.color = trouverCouleur(VerifArtefact.ArtefactChoisi);
        IconeRessource.texture = Resources.Load<Texture>("icones/Artefact_" + trouverArtefact(VerifArtefact.ArtefactChoisi));
        ValeurGain.text = trouverGain(VerifArtefact.ArtefactChoisi);
    }
	
	string trouverRessource(int idArtefact)
    {
        switch(idArtefact)
        {
            // Artéfact Orcus
            case 1:
                return "orcus";
            // Artéfact Boutique
            case 2:
                return "boutique";
            // Artéfact IA
            case 3:
                return "IA";
            // Artéfact Objet
            case 4:
                return "objet";
            // Artéfact Divertissement
            case 5:
                return "divertissement";
            // Artéfact Social
            case 6:
                return "social";
            default:
                return null;
        }
    }

    string trouverGain(int idArtefact)
    {
        switch (idArtefact)
        {
            // Artéfact Orcus
            case 1:
                return "+10k";
            // Artéfact Boutique
            case 2:
                return "0";
            // Artéfact IA
            case 3:
                return "+10k";
            // Artéfact Objet
            case 4:
                return "+1";
            // Artéfact Divertissement
            case 5:
                return "+100%";
            // Artéfact Social
            case 6:
                return "+100%";
            default:
                return null;
        }
    }

    string trouverArtefact(int idArtefact)
    {
        switch (idArtefact)
        {
            // Artéfact Orcus
            case 1:
                return "Orcus";
            // Artéfact Boutique
            case 2:
                return "Magasin";
            // Artéfact IA
            case 3:
                return "IA";
            // Artéfact Objet
            case 4:
                return "Objets";
            // Artéfact Divertissement
            case 5:
                return "Divertissement";
            // Artéfact Social
            case 6:
                return "Social";
            default:
                return null;
        }
    }

    Color32 trouverCouleur (int idArtefact)
    {
        switch (idArtefact)
        {
            // Artéfact Orcus
            case 1:
                return new Color32(255, 207, 0, 255);
            // Artéfact Boutique
            case 2:
                return new Color32(239, 4, 121, 255);
            // Artéfact IA
            case 3:
                return new Color32(0, 227, 174, 255);
            // Artéfact Objet
            case 4:
                return new Color32(207, 46, 255, 255);
            // Artéfact Divertissement
            case 5:
                return new Color32(255, 126, 0, 255);
            // Artéfact Social
            case 6:
                return new Color32(81, 203, 255, 255);
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
}
