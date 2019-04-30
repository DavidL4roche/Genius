using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCompétence : MonoBehaviour {

    public GameObject TupleCatégorie;
    public GameObject TupleCompétences;

    public Text nomCatégorie;
    public Text nomCompétence;
    
    public Text IDExamen;
    public Text IDCompetence;

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

                instance = Instantiate(TupleCompétences, new Vector3(0.0F, 0.0F, 0.0F), TupleCompétences.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "TupleCompétence " + (i + 1) + "-" + (j + 1);
            }
        }
    }
}
