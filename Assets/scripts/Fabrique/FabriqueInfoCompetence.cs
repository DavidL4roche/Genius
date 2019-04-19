using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueInfoCompetence : MonoBehaviour {
    Compétence competence = RessourcesBdD.listeDesCompétences[VerificationCompetence.CompetenceChoisie - 1];

    // Elements graphiques de la fenêtre
    public Text titreCompetence;
    public Text niveauActuel;
    public Text niveauRequis;
    public Text competenceDetail;

    public void Start()
    {
        // On attribue les valeurs de la compétence aux champs respectifs
        titreCompetence.text = competence.NomCompétence;
        competenceDetail.text = competence.Description;

        // Dans le cas où c'est une mission
        if (VerificationMission.MissionChoisi != 0)
        {
            Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];

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
            niveauRequis.text = mr.CompétencesRequises[idComp].Valeur + "%";
        }

        // Dans le cas où c'est un examen
        else
        {
            Examen examen = RessourcesBdD.listeDesExamens[VerificationExamen.ExamChoisi];

            // On cherche la compétence correspondante dans les compétences requises de l'examen
            int idComp = 0;
            for (; idComp < examen.CompétencesRequises.Length; ++idComp)
            {
                if (examen.CompétencesRequises[idComp].ID == competence.ID)
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
            niveauRequis.text = examen.CompétencesRequises[idComp].Valeur + "%";
        }
    }
}
