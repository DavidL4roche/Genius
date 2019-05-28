using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueInfoCompetencePNJ : MonoBehaviour
{
    //Compétence competence = RessourcesBdD.listeDesCompétences[VerificationCompetence.CompetenceChoisie - 1];
    Mission mr = RessourcesBdD.listeDesMissions[VerificationPNJ.MissionChoisi];

    // Elements graphiques de la fenêtre
    public Text titreCompetence;
    public Text niveauActuel;
    public Text niveauRequis;
    public Text competenceDetail;

    public void Start()
    {
        Compétence competence = null;

        // On recherche la compétence cliquée
        for (int i=0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            if (RessourcesBdD.listeDesCompétences[i].ID == VerificationCompetence.CompetenceChoisie)
            {
                competence = RessourcesBdD.listeDesCompétences[i];
                break;
            }
        }

        // On attribue les valeurs de la compétence aux champs respectifs
        titreCompetence.text = competence.NomCompétence;
        competenceDetail.text = competence.Description;

        // On cherche la compétence correspondante dans les compétences requises de la mission
        int idComp = 0;
        for (; idComp < mr.CompétencesRequises.Length; ++idComp)
        {
            if (mr.CompétencesRequises[idComp].ID == competence.ID)
            {
                break;
            }
        }

        int idJoueur = 0;
        for (; idJoueur < RessourcesBdD.listeDesCompétences.Length; ++idJoueur)
        {
            if (competence.ID == RessourcesBdD.listeDesCompétences[idJoueur].ID)
            {
                break;
            }
        }

        niveauActuel.text = Joueur.MesValeursCompetences[idJoueur] + "%";
;       niveauRequis.text = mr.CompétencesRequises[idComp].Valeur + "%";
    }
}
