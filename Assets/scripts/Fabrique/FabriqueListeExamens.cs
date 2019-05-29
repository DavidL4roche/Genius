using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeExamens : MonoBehaviour {
    
    public GameObject Tuple;
    public Image imageTuple;
    public Text nomExamen;
    public RawImage iconeCheck;
    public Button examenIDGO;

    public GameObject TupleCategorie;
    public Text NomCategorie;

    GameObject instance;

    // Use this for initialization
    void Start () {
        genererListeExamens();
	}
	
	// Update is called once per frame
	void genererListeExamens () {

        // On instancie la catégorie "Disponibles"
        NomCategorie.text = "Disponibles";
        instance = Instantiate(TupleCategorie, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
        instance.transform.parent = GameObject.Find("ListeExamens").transform;
        instance.transform.name = "TupleCategorieDisponibles";

        // On génère chaque tuple d'examen disponibles
        for (int i=0; i<RessourcesBdD.listeDesExamens.Length; ++i)
        {
            // On rend interactible le bouton (ZoneInfo)
            examenIDGO.interactable = true;

            // On réinitialise la couleur du Tuple
            imageTuple.color = new Color32(51, 54, 82, 255);
            iconeCheck.enabled = false;

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = truncateString(examen.NomExamen, 15);
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen-1).ToString();

            bool playing = true;
            // On vérifie si le joueur peut passer l'examen
            for (int j = 0; j < examen.CompétencesRequises.Length; ++j)
            {
                int valeurr = examen.CompétencesRequises[j].Valeur;
                if (!verificationCompAvecJoueur(examen.CompétencesRequises[j].ID, valeurr))
                {
                    playing = false;
                    break;
                }
            }

            // On vérifie si l'examen a déjà été passé par le joueur
            RessourcesBdD.recupExamensNonJouables();
            foreach (int id in RessourcesBdD.listeDesExamensNonJouables)
            {
                if (id == examen.IDExamen)
                {
                    playing = false;
                }
            }

            if (playing)
            {
                imageTuple.color = new Color32(49, 97, 125, 255);

                // On instancie le tuple
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("ListeExamens").transform;
                instance.transform.name = "TupleExamen" + (i + 1);
            }
        }

        // On instancie la catégorie "Indisponibles"
        NomCategorie.text = "Indisponibles";
        instance = Instantiate(TupleCategorie, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
        instance.transform.parent = GameObject.Find("ListeExamens").transform;
        instance.transform.name = "TupleCategorieIndisponibles";

        // On génère chaque tuple d'examen indisponibles
        for (int i = 0; i < RessourcesBdD.listeDesExamens.Length; ++i)
        {
            // On rend interactible le bouton (ZoneInfo)
            examenIDGO.interactable = true;

            // On réinitialise la couleur du Tuple
            imageTuple.color = new Color32(51, 54, 82, 255);
            iconeCheck.enabled = false;

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = truncateString(examen.NomExamen, 15);
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen - 1).ToString();

            bool playing = true;
            // On vérifie si le joueur peut passer l'examen
            for (int j = 0; j < examen.CompétencesRequises.Length; ++j)
            {
                int valeurr = examen.CompétencesRequises[j].Valeur;
                if (!verificationCompAvecJoueur(examen.CompétencesRequises[j].ID, valeurr))
                {
                    playing = false;
                    break;
                }
            }

            if (!playing)
            {
                // On instancie le tuple
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("ListeExamens").transform;
                instance.transform.name = "TupleExamen" + (i + 1);
            }
        }

        // On instancie la catégorie "Acquis"
        NomCategorie.text = "Acquis";
        instance = Instantiate(TupleCategorie, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
        instance.transform.parent = GameObject.Find("ListeExamens").transform;
        instance.transform.name = "TupleCategorieAcquis";

        // On génère chaque tuple d'examen acquis
        for (int i = 0; i < RessourcesBdD.listeDesExamens.Length; ++i)
        {
            // On rend interactible le bouton (ZoneInfo)
            examenIDGO.interactable = true;

            // On réinitialise la couleur du Tuple
            imageTuple.color = new Color32(51, 54, 82, 255);
            iconeCheck.enabled = false;

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = truncateString(examen.NomExamen, 15);
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen - 1).ToString();

            // On vérifie si l'examen a déjà été passé par le joueur
            RessourcesBdD.recupExamensNonJouables();
            foreach (int id in RessourcesBdD.listeDesExamensNonJouables)
            {
                if (id == examen.IDExamen)
                {
                    examenIDGO.interactable = false;
                    imageTuple.color = new Color32(6, 212, 168, 255);
                    iconeCheck.enabled = true;

                    // On instancie le tuple
                    instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                    instance.transform.parent = GameObject.Find("ListeExamens").transform;
                    instance.transform.name = "TupleExamen" + (i + 1);
                }
            }
        }
    }

    public string truncateString(string myStr, int trun)
    {
        // Si la chaine est supérieur en taille au paramètre trun alors on ajoute "..." à la fin
        if (myStr.Length >= trun - 4)
        {
            return myStr.Substring(0, trun - 4) + ((myStr.Length - 1 >= trun) ? "..." : "");
        }
        else
        {
            return myStr;
        }
    }

    bool verificationCompAvecJoueur(int idComp, int valeur)
    {
        int i = 0;
        for (; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            if (idComp == RessourcesBdD.listeDesCompétences[i].ID)
            {
                break;
            }
        }
        if (Joueur.MesValeursCompetences[i] >= valeur)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
