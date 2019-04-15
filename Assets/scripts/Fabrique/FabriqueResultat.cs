using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultat : MonoBehaviour {
    Mission mission = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public GameObject Tuple;
    public Text nomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    public Text SliderTexte;
    public Image ImageTuple;
    GameObject instance;
    public void Start()
    {
        //mission.voirRessources();
        AGagner();
        APerdu();
        blockdesprerequis();
        ValeurTupleTexte.color = new Color(1F, 1F, 1F,1F);
        ReloadMissions();
    }
    public void blockdesprerequis()
    {
        for (int i = 0; i < mission.SesGains.Length; ++i)
        {
            if (mission.SesGains[i].ValeurDuGain != 0)
            {
                Text Texte = nomTuple;
                Texte.text = mission.SesGains[i].NomGain;
                ValeurTupleTexte.color = new Color(0F, 1F, 0F, 1F);
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = "+" + mission.SesGains[i].ValeurDuGain.ToString();
                ImageTuple.color = new Color(0F,1F,0F,1F);
                instance = Instantiate(Tuple);
                instance.transform.parent = GameObject.Find("VerticalLayout1").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else
            {
                continue;
            }
        }
        for (int i = 0; i < mission.SesPertes.Length; ++i)
        {
            if (mission.SesPertes[i].ValeurDeLaPerte != 0)
            {
                Text Texte = nomTuple;
                Texte.text = mission.SesPertes[i].NomPerte;
                ValeurTupleTexte.color = new Color(1F, 0F, 0F, 1F);
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = "-" + mission.SesPertes[i].ValeurDeLaPerte.ToString();
                ImageTuple.color = new Color(1F, 0F, 0F, 1F);
                instance = Instantiate(Tuple);
                instance.transform.parent = GameObject.Find("VerticalLayout2").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else
            {
                continue;
            }
        }
    }

    // Calcul des gains du joueur et affectation de ses stats
	void AGagner () {
        for (int i = 0; i< mission.SesGains.Length; ++i)
        {
            if (mission.SesGains[i].ValeurDuGain != 0)
            {
                switch (mission.SesGains[i].NomGain)
                {
                    case "Orcus":
                    case "IA":
                    case "Divertissement":
                    case "Social":
                        gainRess(mission.SesGains[i].NomGain, mission.SesGains[i].ValeurDuGain);
                        break;
                    case "Compétence":
                        gainComp(mission.SesGains[i].ValeurDuGain);
                        break;
                    case "Objet":
                        gainObjet();
                        break;
                    default:
                        break;
                }
            }
        }   
    }

    // Ajoute la valeur dans la compétence du joueur
    void gainComp(int valeur)
    {
        for (int i = 0; i<mission.CompétencesRequises.Length; ++i)
        {
            for (int j = 0; j<RessourcesBdD.listeDesCompétences.Length; ++j)
            {
                if (mission.CompétencesRequises[i].ID == RessourcesBdD.listeDesCompétences[j].ID)
                {
                    if ((Joueur.MesValeursCompetences[j] + valeur) > 100)
                    {
                        Joueur.MesValeursCompetences[j] = 100;
                    }
                    else
                    {
                        Joueur.MesValeursCompetences[j] += valeur;
                        //Debug.Log(Joueur.MesValeursCompetences[j]+ " gain de compétences");
                    }
                }
            }
        }
    }

    // Augmente la ressource du joueur avec la valeur souhaitée
    void gainRess(string NomRess, int valeur)
    {
        for(int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if(NomRess == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                Joueur.MesRessources[i] = Joueur.MesRessources[i] + valeur;
                if((NomRess == "Social" || NomRess == "Divertissement")&& Joueur.MesRessources[i] > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
            }
        }
    }

    // Augmente la quantité d'un objet du joueur de +1
    void gainObjet()
    {
        for (int i = 0; i < mission.SesObjets.Length; ++i)
        {
            for (int j = 0; i < RessourcesBdD.listeDesObjets.Length; ++j)
            {
                if(mission.SesObjets[i].ID == RessourcesBdD.listeDesObjets[j].ID)
                {
                    ++Joueur.MesObjets[j];
                    Debug.Log("Objet gagné");
                    break;
                }
            }
        }
    }

    // Calcul des pertes du joueur et affectation de ses stats
    void APerdu()
    {
        //Ressources
        for (int i = 0; i < mission.SesPertes.Length; ++i)
        {
            if (mission.SesPertes[i].ValeurDeLaPerte != 0)
            {
                for(int j=0; j<RessourcesBdD.listeDesRessources.Length; ++j)
                {
                    if (RessourcesBdD.listeDesRessources[j].NomRessource == mission.SesPertes[i].NomPerte)
                    { 
                        Joueur.MesRessources[j] = Joueur.MesRessources[j] - mission.SesPertes[i].ValeurDeLaPerte;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }    
        //Objet
        if (FicheAmélioration.IDObjetUtilise != 0)
        {
            for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if(RessourcesBdD.listeDesObjets[i].ID == FicheAmélioration.IDObjetUtilise)
                {
                    --Joueur.MesObjets[i];
                    break;
                }
            }
        }
        else
        {

        }
    }
    public void ReloadMissions()
    {
        string requete = "Insert INTO present_missions_done VALUES (" + mission.IDMission + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();  
    }
}
