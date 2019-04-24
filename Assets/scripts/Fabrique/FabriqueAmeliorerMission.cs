using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueAmeliorerMission : MonoBehaviour {
    Mission mission = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    Color vert = new Color32(255, 255, 255, 255);
    Color blanc = new Color32(255, 255, 255, 0);
    public Slider sliderConcentration;
    public void Start()
    {
        VerificationAmelioration();
    }
    public void cliqueGainComp(Button bout)
    {
        int IARequis=0;
        float Valeur = 1.0F;
        switch (bout.transform.name)
        {
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
        for (int i = 1; i < 4; ++i)
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
        else
        {

        }

        //concentration
        if (FicheAmélioration.Concentration == 1)
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
            GameObject.Find("BoutonObjet").GetComponent<Button>().image.color = blanc;
            GameObject.Find("TexteObjet").GetComponent<Text>().text = "X";
        }
        else
        {
            GameObject.Find("BoutonObjet").GetComponent<Button>().image.color = vert;
            GameObject.Find("TexteObjet").GetComponent<Text>().text = FicheAmélioration.IDObjetUtilise.ToString();
        }
    }
    public void boutonOnOff()
    {
        if(sliderConcentration.value == 1)
        {
            sliderConcentration.value = 0;
            FicheAmélioration.Concentration = 1;
            Gain.calculDesGains(mission);
            Perte.calculDesPertes(mission);
        }
        else
        {
            sliderConcentration.value = 1;
            FicheAmélioration.Concentration = 2;
            Gain.calculDesGains(mission);
            Perte.calculDesPertes(mission);
        }
    }
    public void utiliserObjet()
    {
        ChargerPopup.Charger("AjoutObjet");
    }
}
