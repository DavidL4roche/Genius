using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifArtefactCliquer : MonoBehaviour {
    public static int ArtefactChoisi;
    public void Cliquer()
    {
        //Debug.Log(gameObject.name);
        char[] nomArtefact = gameObject.name.ToCharArray();
        ArtefactChoisi = (int)System.Char.GetNumericValue(nomArtefact[(nomArtefact.Length - 1)]);
        Debug.Log("tu as cliquer sur l'artefact ->"+ArtefactChoisi);
        Gain.utiliserUnArtefact();
        ChargerLieu charger = new ChargerLieu();
        charger.rechargerArtefact();
    }
}
