using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePreRequisExam : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Examen examen = RessourcesBdD.listeDesExamens[VerificationExamen.ExamChoisi];
    public GameObject Tuple;
    public Text nomTuple;
    public Text ValeurTupleTexte;
    public Text Divert;
    public Text Social;
    public Slider ValeurTupleSlider;
    public Text SliderTexte;
    public Image ImageTuple;
    public Button lancer;
    GameObject instance;

    public void Start()
    {
        Perte.calculDesPertes(examen);
        blockdesprerequis();
    }
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        int decalage = 0;

        for (int i = 0; i < examen.CompétencesRequises.Length; ++i)
        {
            Text Texte = nomTuple;
            Texte.text = examen.CompétencesRequises[i].NomCompétence;
            ValeurTupleSlider.gameObject.SetActive(true);
            ValeurTupleTexte.gameObject.SetActive(false);
            int valeurr = examen.CompétencesRequises[i].Valeur;
            ValeurTupleSlider.value = valeurr;
            SliderTexte.text = valeurr.ToString();
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
            ImageTuple.color = new Color(0F, 1F, 0F, 1F);
        }
        else
        {
            ImageTuple.color = new Color(1F, 0F, 0F, 1F);
            Destroy(GameObject.Find("Lancer"));
            Destroy(GameObject.Find("Ameliorer"));

        }
    }
    public void verificationRessAvecJoueur(int valeurressource, string nomPrerequis)
    {
        for (int i = 0; i < RessourcesBdD.listeDesRessources.Length; ++i)
        {
            if (nomPrerequis == RessourcesBdD.listeDesRessources[i].NomRessource)
            {
                if (Joueur.MesRessources[i] >= valeurressource)
                {
                    ImageTuple.color = new Color(0F, 1F, 0F, 1F);
                }
                else
                {
                    ImageTuple.color = new Color(1F, 0F, 0F, 1F);
                    lancer.interactable = false;
                }
            }
        }
    }
}
