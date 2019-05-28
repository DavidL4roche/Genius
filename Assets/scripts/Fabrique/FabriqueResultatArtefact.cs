using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatArtefact : MonoBehaviour {

    public RawImage IconeCheck;
    public RawImage IconeRessource;
    public Text ValeurGain;

	void Start () {
        IconeCheck.color = new Color32(255, 255, 255, 255);
        IconeRessource.texture = Resources.Load<Texture>("icones/IconM_" + FabriqueMissionPNJ.trouverArtefact(VerifArtefact.ArtefactChoisi));
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
                return "+10.000";
            // Artéfact Boutique
            case 2:
                return "0";
            // Artéfact IA
            case 3:
                return "+10.000";
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
}
