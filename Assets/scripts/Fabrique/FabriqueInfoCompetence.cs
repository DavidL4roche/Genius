using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class FabriqueInfoCompetence : MonoBehaviour {
    Mission mission = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];

    // Elements graphiques de la fenêtre
    public Text titreCompetence;
    public Text niveauActuel;
    public Text niveauRequis;

    public void Start()
    {
        // On récupère le champ de description de la compétence
        TextMeshPro competence = GetComponent<TextMeshPro>();

        // On attribue les valeurs de la compétence aux champs respectifs
        titreCompetence.text = "Titre de la compétence";
        niveauActuel.text = "99%";
        niveauRequis.text = "98%";
        competence.text = "Une petite description des familles.";
    }
}
