using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueInfoCompetence : MonoBehaviour {
    Compétence competence = RessourcesBdD.listeDesCompétences[VerificationCompetence.CompetenceChoisie];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];

    // Elements graphiques de la fenêtre
    public Text titreCompetence;
    public Text niveauActuel;
    public Text niveauRequis;
    public TextMeshProUGUI competenceDetail;

    public void Start()
    {
        // On attribue les valeurs de la compétence aux champs respectifs
        titreCompetence.text = competence.NomCompétence;
        competenceDetail.text = competence.Description;

        // On cherche la compétence correspondante dans les compétences requises de la mission
        int idComp = 0;
        foreach (Compétence m in mr.CompétencesRequises)
        {
            if(m.ID == competence.ID)
            {
                break;
            }
            ++idComp;
        }

        niveauRequis.text = mr.CompétencesRequises[idComp].Valeur + "%";
        niveauActuel.text = Joueur.MesValeursCompetences[idComp] + "%";
    }
}
