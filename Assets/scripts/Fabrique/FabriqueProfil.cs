using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueProfil : MonoBehaviour {
    public Button Artefact;
    public Text NomJoueur;
    public Button instance;
    public int totalArtefact;
    public void Start()
    {
        totalArtefact = 0;
        NomJoueur.text = Joueur.NomJoueur;
        for (int i = 0; i < RessourcesBdD.listeDesArtefactsJouables.Length; ++i)
        {
            instance = Instantiate(Artefact, new Vector3(0, 0, 0), Artefact.transform.rotation);
            instance.transform.name = "Artefact n." + i;
            if (totalArtefact < 4)
            {
                instance.transform.parent = GameObject.Find("Ranger1").transform;
                ++totalArtefact;
            }
            else
            {
                instance.transform.parent = GameObject.Find("Ranger2").transform;
            }
        }
    }
}
