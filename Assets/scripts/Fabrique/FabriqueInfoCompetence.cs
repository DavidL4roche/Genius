using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueInfoCompetence : MonoBehaviour {
    Compétence competence = RessourcesBdD.listeDesCompétences[VerificationCompetence.CompetenceChoisie];

    // Elements graphiques de la fenêtre
    public Text titreCompetence;
    public Text niveauActuel;
    public Text niveauRequis;
    public TextMeshProUGUI competenceDetail;

    public void Start()
    {
        // On récupère le champ de description de la compétence
        //TextMeshPro competence = GetComponent<TextMeshPro>();

        // On attribue les valeurs de la compétence aux champs respectifs
        titreCompetence.text = competence.NomCompétence;
        niveauActuel.text = "98%";
        niveauRequis.text = competence.Valeur + "%";
        //competenceDetail = GetComponent<TextMeshProUGUI>();
        competenceDetail.text = competence.Description;
    }
}
