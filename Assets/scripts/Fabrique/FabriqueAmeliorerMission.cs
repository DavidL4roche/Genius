﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueAmeliorerMission : MonoBehaviour {
    Mission mission = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    Color vert = new Color32(255, 255, 255, 255);
    Color blanc = new Color32(255, 255, 255, 0);

    public Slider sliderConcentration;
    public RawImage imageObjet;

    public GameObject EcranTuto;

    public void Start()
    {
        // On vérifie que le joueur a fait le tuto Optimisation (6)
        StartCoroutine(Joueur.VerifierStatusTuto(6, EcranTuto));

        VerificationAmelioration();
    }

    public void cliqueGainComp(Button bout)
    {
        int IARequis=0;
        float Valeur = 1.0F;
        switch (bout.transform.name)
        {
            case "Bouton0":
                Valeur = 1.0F;
                IARequis = 0;
                break;
            case "Bouton1":
                Valeur = 1.1F;
                IARequis = 500;
                break;

            case "Bouton2":
                Valeur = 1.2F;
                IARequis = 1500;
                break;

            case "Bouton3":
                Valeur = 1.5F;
                IARequis = 4000;
                break;
        }

        //Debug.Log(bout.image.color + " et le vert " + vert);
        if (bout.image.color == vert)
        {
            bout.image.color = blanc;
           //enleveGainComp();
        }
        else
        {
            nettoyerBouton();
            bool testIA = testIARequise(IARequis);
            if (testIA == true)
            {
                bout.image.color = vert;
                FicheAmélioration.Optimisation = Valeur;
                Gain.calculDesGains(mission);
                Perte.calculDesPertes(mission);
                for (int i = 0; i<mission.SesPertes.Length; ++i)
                {
                    if (mission.SesPertes[i].NomPerte == "IA")
                    {
                        mission.SesPertes[i].ValeurDeLaPerte = IARequis;
                    }
                }
            }
            else
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Pas assez de Matière IA";
            }
        }
    }
    public void nettoyerBouton()
    {
        for (int i = 0; i < 4; ++i)
        {
            GameObject obj = GameObject.Find("Bouton" + i);
            obj.GetComponent<Button>().image.color = blanc;
        }
        for (int i = 0; i < mission.SesPertes.Length; ++i)
        {
            if (mission.SesPertes[i].NomPerte == "IA")
            {
                mission.SesPertes[i].ValeurDeLaPerte = 0;
            }
        }
    }
    public void enleveGainComp()
    {
        return;
    }
    public bool testIARequise(int IA)
    {
        bool Ok = false;
        for(int i = 0 ; i < Joueur.MesRessources.Length ; ++i)
        {
            if(RessourcesBdD.listeDesRessources[i].NomRessource == "IA" && Joueur.MesRessources[i] >= IA)
            {
                Ok = true;
            }
            else
            {

            }
        }
        return Ok;
    }

    // On change les champs de la fenêtre en fonction des valeurs d'amélioration déjà saisies
    void VerificationAmelioration()
    {
        //Boutons compétence
        if (FicheAmélioration.Optimisation == 1.1F)
        {
            GameObject.Find("Bouton1").GetComponent<Button>().image.color = vert;
        }
        else if (FicheAmélioration.Optimisation == 1.2F)
        {
            GameObject.Find("Bouton2").GetComponent<Button>().image.color = vert;
        }
        else if  (FicheAmélioration.Optimisation == 1.5F)
        {
            GameObject.Find("Bouton3").GetComponent<Button>().image.color = vert;
        }
        
        //concentration
        if (FicheAmélioration.Concentration == false)
        {
            sliderConcentration.value = 0;
        }
        else
        {
            sliderConcentration.value = 1;
        }

        //Objet
        if  (FicheAmélioration.IDObjetUtilise == 0)
        {
            //GameObject.Find("Ameliorer").GetComponent<Button>().image.color = new Color32(82, 86, 118, 255);
            imageObjet.texture = Resources.Load<Texture>("icones/ObjetCross");
        }
        else
        {
            //GameObject.Find("Ameliorer").GetComponent<Button>().image.color = new Color32(82, 86, 118, 255);
            imageObjet.texture = Resources.Load<Texture>("icones/Item" + FicheAmélioration.IDObjetUtilise);
        }
    }

    public void boutonOnOff()
    {
        if(sliderConcentration.value == 1)
        {
            //sliderConcentration.value = 0;
            FicheAmélioration.Concentration = true;
            Gain.calculDesGains(mission);
            Perte.calculDesPertes(mission);
        }
        else
        {
            //sliderConcentration.value = 1;
            FicheAmélioration.Concentration = false;
            Gain.calculDesGains(mission);
            Perte.calculDesPertes(mission);
        }
    }
    public void utiliserObjet()
    {
        ChargerPopup.Charger("AjoutObjet");
    }
}
