using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjetClique : MonoBehaviour {
    Mission mission = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public Text texte;
    public void ObjetCliqué()
    {
        string objetid = texte.text;
        if(objetid == "X")
        {
            objetid = "0";
        }
        //mission.IDObjetUtilise = objetid;
        Debug.Log("Tu as cliqué sur l'objet n." + objetid);
        FicheAmélioration.IDObjetUtilise = Int32.Parse(objetid);
        FicheAmélioration.attribuerBonusObjets();
        Gain.calculDesGains(mission);
        Perte.calculDesPertes(mission);
    }
}
