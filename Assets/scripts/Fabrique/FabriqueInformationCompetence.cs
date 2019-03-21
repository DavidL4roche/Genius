using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueInformationCompetence : MonoBehaviour {
    //MissionRessources mission = ListeMissions.listeDeMissions[VerifQuartier.IDQuartier].missions[VerificationMission.MissionChoisi];
    Mission mr = SpawnerMission.LesMissions[VerificationMission.MissionChoisi];
    public GameObject TupleObjet;
    public Text TitreCompetence;
    private TextMeshPro DescriptionCompetence;
    public Text NiveauActuel;
    public Text NiveauRequis;
    GameObject instance;
    private void Start()
    {
        DescriptionCompetence = FindObjectOfType<TextMeshPro>();
        TitreCompetence.text = "coucou";
        DescriptionCompetence.text = "coucou";
        NiveauActuel.text = "coucou";
        NiveauRequis.text = "coucou";
    }
}
