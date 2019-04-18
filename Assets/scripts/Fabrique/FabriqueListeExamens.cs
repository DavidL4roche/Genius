using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeExamens : MonoBehaviour {
    
    public GameObject Tuple;
    public Text nomExamen;
    public GameObject examenIDGO;
    GameObject instance;

    // Use this for initialization
    void Start () {
        genererListeExamens();
	}
	
	// Update is called once per frame
	void genererListeExamens () {

        for (int i=0; i<RessourcesBdD.listeDesExamens.Length; ++i)
        {
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = examen.NomExamen;
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen-1).ToString();
            //verificationCompAvecJoueur(examen.CompétencesRequises[i].ID, valeurr);
            instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
            instance.transform.parent = GameObject.Find("ListeExamens").transform;
            instance.transform.name = "TupleExamen" + (i + 1);
        }
    }
}
