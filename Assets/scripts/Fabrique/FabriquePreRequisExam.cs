using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePreRequisExam : MonoBehaviour
{
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Examen examen = RessourcesBdD.listeDesExamens[VerificationExamen.ExamChoisi];
    public GameObject Tuple;
    public Text nomTuple;
    public Text ValeurTupleTexte;
    public Text Divert;
    public Text Social;
    public GameObject ValeurBarre;
    public Image ImageTuple;
    public Button lancer;
    public Image fondLancer;
    GameObject instance;

    public void Start()
    {
        Perte.calculDesPertes(examen);
        blockdesprerequis();
    }
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        ValeurBarre.SetActive(false);
        int decalage = 0;

        for (int i = 0; i < examen.CompétencesRequises.Length; ++i)
        {
            Text Texte = nomTuple;
            Texte.text = examen.CompétencesRequises[i].NomCompétence;
            ValeurTupleTexte.gameObject.SetActive(false);
            int valeurr = examen.CompétencesRequises[i].Valeur;
            /*
            ValeurTupleSlider.value = valeurr;
            SliderTexte.text = valeurr.ToString();
            */
            verificationCompAvecJoueur(examen.CompétencesRequises[i].ID, valeurr);
            instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout1").transform;
            instance.transform.name = "Tuple " + (i + 1);
            ++decalage;
        }
        decalage = 0;
        for (int i = 0; i < examen.SesPertes.Length; ++i)
        {
            //Debug.Log(mission.tabprerequis[i] + " = " + mission.tabValeurPrerequis[i]);
            if (examen.SesPertes[i].ValeurDeLaPerte != 0)
            {
                Text Texte = nomTuple;
                Texte.text = examen.SesPertes[i].NomPerte;
                ValeurTupleTexte.gameObject.SetActive(true);

                switch (examen.SesPertes[i].NomPerte)
                {
                    case "Divertissement":
                        Divert.text = "-" + examen.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                        break;
                    case "Social":
                        Social.text = "-" + examen.SesPertes[i].ValeurDeLaPerte.ToString() + "%";
                        break;
                    default:
                        break;
                }

                /*
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = examen.SesPertes[i].ValeurDeLaPerte.ToString();
                verificationRessAvecJoueur(examen.SesPertes[i].ValeurDeLaPerte, examen.SesPertes[i].NomPerte);
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout2").transform;
                instance.transform.name = "Tuple " + (i + 1);
                ++decalage;
                */
            }
            else
            {
                continue;
            }
        }
    }
    public void verificationCompAvecJoueur(int idComp, int valeur)
    {
        int i = 0;
        for (; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            if (idComp == RessourcesBdD.listeDesCompétences[i].ID)
            {
                break;
            }
        }
        if (Joueur.MesValeursCompetences[i] >= valeur)
        {
            ImageTuple.color = changeColor(true);
        }
        else
        {
            ImageTuple.color = changeColor(false);
            lancer.interactable = false;
            fondLancer.color = new Color(255, 255, 255, 125);
        }
    }
    public void verificationRessAvecJoueur(int valeurressource, string nomPrerequis)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (nomPrerequis == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                if (Joueur.MesRessources[i] < valeurressource)
                {
                    lancer.interactable = false;
                }
            }
        }
    }

    // Renvoie la couleur (Color) si la condition est vraie ou fausse
    public Color changeColor(bool color)
    {
        if (color)
        {
            Color myColor = new Color32(57, 119, 155, 255);
            return myColor;
        }
        else
        {
            Color myColor = new Color32(55, 57, 75, 255);
            return myColor;
        }
    }
}
