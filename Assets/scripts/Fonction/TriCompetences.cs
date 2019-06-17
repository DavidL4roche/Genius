using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TriCompetences : MonoBehaviour {

    public GameObject TupleCatégorie;
    public Text nomCatégorie;
    public Text IDExamen;

    public GameObject TupleCompétences;
    public Text nomCompétence;
    public Text IDCompetence;
    public RawImage colorTupleComp;

    GameObject instance;

    // Trie les compétences dans l'ordre alphabétique
    public void triAlpha()
    {
        // On supprime les éléments de la liste
        supprimerElementsVerticalLayout();

        // On annonce que le tri en est alpha
        FabriqueCompétence.methodeTri = "alpha";

        // On trie les compétences par ordre alphabétique
        Compétence[] compétencesAlpha = new Compétence[RessourcesBdD.listeDesCompétences.Length];
        compétencesAlpha = RessourcesBdD.listeDesCompétences.OrderBy(s => s.NomCompétence).ToArray();

        // On instancie les compétences dans la liste
        for (int i = 0; i<compétencesAlpha.Length; i++)
        {
            nomCompétence.text = compétencesAlpha[i].NomCompétence;
            IDCompetence.text = compétencesAlpha[i].ID.ToString();

            // On recherche le premier examen associé
            bool exam = false;
            for (int j = 0; j < RessourcesBdD.listeDesExamens.Length; ++j)
            {
                if (exam == false)
                {
                    for (int k = 0; k < RessourcesBdD.listeDesExamens[j].CompétencesRequises.Length; k++)
                    {
                        if (RessourcesBdD.listeDesExamens[j].CompétencesRequises[k].ID == compétencesAlpha[i].ID)
                        {
                            // On compare la valeur demandée de la compétence par l'examen avec la valeur du joueur
                            int valComp = k;

                            int valJoueur = 0;
                            for (; valJoueur < RessourcesBdD.listeDesCompétences.Length; ++valJoueur)
                            {
                                if (RessourcesBdD.listeDesExamens[j].CompétencesRequises[k].ID == RessourcesBdD.listeDesCompétences[valJoueur].ID)
                                {
                                    break;
                                }
                            }

                            if (RessourcesBdD.listeDesExamens[j].CompétencesRequises[valComp].Valeur < Joueur.MesValeursCompetences[valJoueur])
                            {
                                colorTupleComp.color = new Color32(6, 212, 168, 255);
                            }
                            else
                            {
                                colorTupleComp.color = new Color32(49, 97, 125, 255);
                            }

                            IDExamen.text = (RessourcesBdD.listeDesExamens[j].IDExamen - 1).ToString();
                            exam = true;
                        }
                    }
                }
            }
            

            instance = Instantiate(TupleCompétences, new Vector3(0.0F, 0.0F, 0.0F), TupleCompétences.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "TupleCompétence " + (i + 1);
        }
    }

    // Trie les compétences par diplôme
    public void triDiplome()
    {
        // On supprime les éléments de la liste
        supprimerElementsVerticalLayout();

        // On annonce que le tri en est diplome
        FabriqueCompétence.methodeTri = "diplome";

        for (int i = 0; i < RessourcesBdD.listeDesExamens.Length; ++i)
        {
            nomCatégorie.text = RessourcesBdD.listeDesExamens[i].NomExamen;
            instance = Instantiate(TupleCatégorie, new Vector3(0.0F, 0.0F, 0.0F), TupleCatégorie.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "TupleCatégorie " + (i + 1);

            for (int j = 0; j < RessourcesBdD.listeDesExamens[i].CompétencesRequises.Length; ++j)
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

                if (RessourcesBdD.listeDesExamens[i].CompétencesRequises[valComp].Valeur < Joueur.MesValeursCompetences[valJoueur])
                {
                    colorTupleComp.color = new Color32(6, 212, 168, 255);
                }
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

    // Supprime tous les éléments de la liste de compétences
    void supprimerElementsVerticalLayout()
    {
        foreach (Transform child in GameObject.Find("VerticalLayout").transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
