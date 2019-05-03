using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifArtefact : MonoBehaviour {

    public static int ArtefactChoisi;

    public void Cliquer()
    {
        Text IDArtefactText = gameObject.GetComponentInChildren<Text>();
        ArtefactChoisi = System.Int32.Parse(IDArtefactText.text);

        Debug.Log("Artefact choisi : " + ArtefactChoisi);

        ChargerPopup charger = new ChargerPopup();
        charger.ChargerNonStatique("ValidArtefact");
    }
}
