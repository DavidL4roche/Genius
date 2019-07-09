using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCompétenceAmi : MonoBehaviour
{
    AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];

    public GameObject TupleCatégorie;
    public Text nomCatégorie;
    public Text IDExamen;

    public GameObject TupleCompétences;
    public Text nomCompétence;
    public Text IDCompetence;
    public Image colorTupleComp;

    GameObject instance;

    private void Start()
    {
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
                try
                {
                    double pourcentage = ((double)joueur.SesCompétences[valJoueur] / 100) * 264.07;
                    colorTupleComp.GetComponent<RectTransform>().sizeDelta = new Vector2((float)pourcentage, 42.4f);
                }
                catch(NullReferenceException e)
                {
                    RessourcesBdD.ReloadGame();
                }

                if (RessourcesBdD.listeDesExamens[i].CompétencesRequises[valComp].Valeur < joueur.SesCompétences[valJoueur])
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
}
