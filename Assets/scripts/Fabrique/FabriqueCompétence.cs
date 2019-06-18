using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCompétence : MonoBehaviour {

    public GameObject TupleCatégorie;
    public Text nomCatégorie;
    public Text IDExamen;

    public GameObject TupleCompétences;
    public Text nomCompétence;
    public Text IDCompetence;
    public Image colorTupleComp;

    public RawImage IconeAZ;
    public RawImage IconeDiplome;
    public static string methodeTri;

    GameObject instance;

    private void Start()
    {
        methodeTri = "diplome";

        for (int i=0; i<RessourcesBdD.listeDesExamens.Length; ++i)
        {
            nomCatégorie.text = RessourcesBdD.listeDesExamens[i].NomExamen;
            instance = Instantiate(TupleCatégorie, new Vector3(0.0F, 0.0F, 0.0F), TupleCatégorie.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "TupleCatégorie " + (i + 1);

            for (int j=0; j< RessourcesBdD.listeDesExamens[i].CompétencesRequises.Length; ++j)
            {
                nomCompétence.text = RessourcesBdD.listeDesExamens[i].CompétencesRequises[j].NomCompétence;
                IDExamen.text = (RessourcesBdD.listeDesExamens[i].IDExamen - 1).ToString();
                IDCompetence.text = RessourcesBdD.listeDesExamens[i].CompétencesRequises[j].ID.ToString();

                // On compare la valeur demandée de la compétence par l'examen avec la valeur du joueur
                int valComp = 0;
                for (; valComp < RessourcesBdD.listeDesExamens[i].CompétencesRequises.Length; ++valComp)
                {
                    if (RessourcesBdD.listeDesExamens[i].CompétencesRequises[valComp].ID == RessourcesBdD.listeDesExamens[i].CompétencesRequises[j].ID)
                    {
                        break;
                    }
                }

                int valJoueur = 0;
                for (; valJoueur < RessourcesBdD.listeDesCompétences.Length; ++valJoueur)
                {
                    if (RessourcesBdD.listeDesExamens[i].CompétencesRequises[j].ID == RessourcesBdD.listeDesCompétences[valJoueur].ID)
                    {
                        break;
                    }
                }

                // On affiche visuellement la valeur du joueur dans la compétence
                double pourcentage = ((double)Joueur.MesValeursCompetences[valJoueur] / 100) * 264.07;
                colorTupleComp.GetComponent<RectTransform>().sizeDelta = new Vector2((float)pourcentage, 41.6f);
                
                if (RessourcesBdD.listeDesExamens[i].CompétencesRequises[valComp].Valeur < Joueur.MesValeursCompetences[valJoueur])
                {
                    colorTupleComp.color = new Color32(0, 255, 196, 255);
                }
                // Si la valeur du joueur est supérieur à celle de l'examen
                else
                {
                    colorTupleComp.color = new Color32(49, 97, 125, 255);

                }

                instance = Instantiate(TupleCompétences, new Vector3(0.0F, 0.0F, 0.0F), TupleCompétences.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "TupleCompétence " + (i + 1) + "-" + (j + 1);
            }
        }
    }

    private void Update()
    {
        if (methodeTri == "alpha")
        {
            IconeAZ.color = new Color32(255, 255, 255, 255);
            IconeDiplome.color = new Color32(255, 255, 255, 51);
        }
        else if (methodeTri == "diplome")
        {
            IconeAZ.color = new Color32(255, 255, 255, 51);
            IconeDiplome.color = new Color32(255, 255, 255, 255);
        }
    }
}
