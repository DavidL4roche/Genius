using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueResultatExam : MonoBehaviour {
    Examen examen = SpawnerExam.LesExam[VerificationExamen.ExamChoisi];
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
        ValeurTupleTexte.color = new Color(1F, 1F, 1F, 1F);
        ReloadMissions();
    }
    public void blockdesprerequis()
    {
        for (int i = 0; i < examen.SesGains.Length; ++i)
        {
            if (examen.SesGains[i].ValeurDuGain != 0)
            {
                Text Texte = nomTuple;
                Texte.text = examen.SesGains[i].NomGain;
                ValeurTupleTexte.color = new Color(0F, 1F, 0F, 1F);
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = "+" + examen.SesGains[i].ValeurDuGain.ToString();
                ImageTuple.color = new Color(0F, 1F, 0F, 1F);
                instance = Instantiate(Tuple);
                instance.transform.parent = GameObject.Find("VerticalLayout1").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else
            {
                continue;
            }
        }
        for (int i = 0; i < examen.SesPertes.Length; ++i)
        {
            if (examen.SesPertes[i].ValeurDeLaPerte != 0)
            {
                Text Texte = nomTuple;
                Texte.text = examen.SesPertes[i].NomPerte;
                ValeurTupleTexte.color = new Color(1F, 0F, 0F, 1F);
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = "-" + examen.SesPertes[i].ValeurDeLaPerte.ToString();
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
    void AGagner()
    {
        for (int i = 0; i < examen.SesGains.Length; ++i)
        {
            if (examen.SesGains[i].ValeurDuGain != 0)
            {
                switch (examen.SesGains[i].NomGain)
                {
                    case "Orcus":
                    case "IA":
                    case "Divertissement":
                    case "Social":
                        gainRess(examen.SesGains[i].NomGain, examen.SesGains[i].ValeurDuGain);
                        break;
                    case "Compétence":
                        gainComp(examen.SesGains[i].ValeurDuGain);
                        break;
                    case "Diplome":
                        gainDiplome();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    void gainComp(int valeur)
    {
        for (int i = 0; i < examen.CompétencesRequises.Length; ++i)
        {
            for (int j = 0; j < RessourcesBdD.listeDesCompétences.Length; ++j)
            {
                if (examen.CompétencesRequises[i].ID == RessourcesBdD.listeDesCompétences[j].ID)
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
    void gainRess(string NomRess, int valeur)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (NomRess == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                Joueur.MesRessources[i] = Joueur.MesRessources[i] + valeur;
                if ((NomRess == "Social" || NomRess == "Divertissement") && Joueur.MesRessources[i] > 100)
                {
                    Joueur.MesRessources[i] = 100;
                }
            }
        }
    }
    void gainDiplome()
    {
        for(int i = 0; i<RessourcesBdD.listeDesDiplomes.Length; ++i)
        {
            if (examen.LeDiplome.IDDiplome == RessourcesBdD.listeDesDiplomes[i].IDDiplome)
            {
                Joueur.MesDiplomes[i] = true;
                break;
            }
        }
    }
    void APerdu()
    {
        //Ressources
        for (int i = 0; i < examen.SesPertes.Length; ++i)
        {
            if (examen.SesPertes[i].ValeurDeLaPerte != 0)
            {
                for (int j = 0; j < RessourcesBdD.listeDesRessources.Length; ++j)
                {
                    if (RessourcesBdD.listeDesRessources[j].NomRessource == examen.SesPertes[i].NomPerte)
                    {
                        Joueur.MesRessources[j] = Joueur.MesRessources[j] - examen.SesPertes[i].ValeurDeLaPerte;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
    public void ReloadMissions()
    {
        string requete = "Insert INTO diplom_pc VALUES (" + examen.LeDiplome.IDDiplome + "," + Joueur.IDJoueur + ");";
        MySqlCommand commande = new MySqlCommand(requete, Connexion.connexion);
        MySqlDataReader lien = commande.ExecuteReader();
        lien.Close();
    }
}
