using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RessourcesJoueur : MonoBehaviour {
    //public Slider[] barres = new Slider[2];
    public Text[] textes = new Text[4];
    static public bool stop = false;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if(!stop)
        {
            if (Joueur.MesRessources != null)
            {
                for (int i = 0; i < Joueur.MesRessources.Length; ++i)
                {
                    switch (RessourcesBdD.listeDesRessources[i].NomRessource)
                    {
                        // SOCIAL
                        case "Social":
                            //barres[0].value = (Joueur.MesRessources[i]);
                            textes[0].text = Joueur.MesRessources[i].ToString() + "%";
                            break;

                        // DIVERTISSEMENT
                        case "Divertissement":
                            //barres[1].value = (Joueur.MesRessources[i]);
                            textes[1].text = Joueur.MesRessources[i].ToString() + "%";
                            break;

                        //ORCUS
                        case "Orcus":
                            textes[2].text = Joueur.MesRessources[i].ToString();
                            break;

                        // MATIERE IA
                        case "IA":
                            textes[3].text = Joueur.MesRessources[i].ToString();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
