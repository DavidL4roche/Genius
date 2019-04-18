using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeExamens : MonoBehaviour {
    
    public GameObject Tuple;
    public RawImage imageTuple;
    public Text nomExamen;
    public Button examenIDGO;
    GameObject instance;

    // Use this for initialization
    void Start () {
        genererListeExamens();
	}
	
	// Update is called once per frame
	void genererListeExamens () {

        // On génère chaque tuple d'examen
        for (int i=0; i<RessourcesBdD.listeDesExamens.Length; ++i)
        {
            // On rend interactible le bouton (ZoneInfo)
            examenIDGO.interactable = true;

            // On réinitialise la couleur du Tuple
            imageTuple.color = new Color32(49, 97, 125, 255);

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = examen.NomExamen;
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen-1).ToString();

            // On vérifie si l'examen a déjà été passé par le joueur
            RessourcesBdD.recupExamensNonJouables();
            foreach (int id in RessourcesBdD.listeDesExamensNonJouables)
            {
                if (id == examen.IDExamen)
                {
                    examenIDGO.interactable = false;
                    imageTuple.color = new Color32(43, 152, 26, 255);
                }
            }

            // On instancie le tuple
            instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
            instance.transform.parent = GameObject.Find("ListeExamens").transform;
            instance.transform.name = "TupleExamen" + (i + 1);
        }
    }
}
