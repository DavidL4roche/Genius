using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriquePréRequisDivert : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    MissionDivertissement divert = SpawnerMission.SonDivertissement;
    public GameObject Tuple;
    public Text nomTuple;
    public Text ValeurTupleTexte;
    public Slider ValeurTupleSlider;
    public Text SliderTexte;
    public GameObject BlockRess;
    public Image ImageTuple;
    public Button lancer;
    GameObject instance;

    public void Start()
    {
        Perte.calculDesPertes(divert);
        blockdesprerequis();
    }
    public void blockdesprerequis()
    {
        lancer.interactable = true;
        for (int i = 0; i < divert.SesPertes.Length; ++i)
        {
            //Debug.Log(mission.tabprerequis[i] + " = " + mission.tabValeurPrerequis[i]);
            if (divert.SesPertes[i].ValeurDeLaPerte != 0 && divert.SesPertes[i].NomPerte =="Orcus")
            {
                Text Texte = nomTuple;
                Texte.text = divert.SesPertes[i].NomPerte;
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = divert.SesPertes[i].ValeurDeLaPerte.ToString();
                verificationRessAvecJoueur(divert.SesPertes[i].ValeurDeLaPerte, divert.SesPertes[i].NomPerte);
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else if (divert.SesPertes[i].NomPerte == "Orcus")
            {
                Text Texte = nomTuple;
                Texte.text = divert.SesPertes[i].NomPerte;
                ValeurTupleTexte.gameObject.SetActive(true);
                ValeurTupleSlider.gameObject.SetActive(false);
                ValeurTupleTexte.text = divert.SesPertes[i].ValeurDeLaPerte.ToString();
                verificationRessAvecJoueur(divert.SesPertes[i].ValeurDeLaPerte, divert.SesPertes[i].NomPerte);
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
            else
            {
                continue;
            }
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
