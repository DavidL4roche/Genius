using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifArtefactCliquer : MonoBehaviour {
    public static int ArtefactChoisi;

    public void Cliquer()
    {
        // On fait correspondre l'artefact
        foreach(Artefact art in RessourcesBdD.listeDesArtefacts)
        {
            if (art.IDArtefact == VerifArtefact.ArtefactChoisi)
            {
                for (int i = 0; i < RessourcesBdD.listeDesArtefactsJouables.Length; ++i)
                {
                    if (RessourcesBdD.listeDesArtefactsJouables[i].IDArtefact == art.IDArtefact)
                    {
                        ArtefactChoisi = i;
                    }
                }
            }
        }
        
        Debug.Log("Artefact utilisé : " + RessourcesBdD.listeDesArtefactsJouables[ArtefactChoisi].NomArtefact);
        
        Gain.utiliserUnArtefact();
        ChargerLieu charger = new ChargerLieu();
        charger.rechargerArtefact();
    }
}
