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

    public GameObject EcranTuto;

    public static bool continueListeExamens = false;

    // Use this for initialization
    void Start ()
    {
        // On vérifie que le joueur a fait le tuto Examen (2)
        StartCoroutine(Joueur.VerifierStatusTuto(2, EcranTuto));
        StartCoroutine(RessourcesBdD.recupExamensNonJouables());
	}

    private void Update()
    {
        if (continueListeExamens)
        {
            genererListeExamens();
            continueListeExamens = false;
        }
    }

    void genererListeExamens () {

        // On instancie la catégorie "Disponibles"
        NomCategorie.text = "Disponible";
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
            imageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2(204.6f, 42.43f);
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
            //StartCoroutine(RessourcesBdD.recupExamensNonJouables());
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
                imageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2(204.6f, 42.43f);

                // On instancie le tuple
                instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("ListeExamens").transform;
                instance.transform.name = "TupleExamen" + (i + 1);
            }
        }

        // On instancie la catégorie "Indisponibles"
        NomCategorie.text = "Indisponible";
        instance = Instantiate(TupleCategorie, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
        instance.transform.parent = GameObject.Find("ListeExamens").transform;
        instance.transform.name = "TupleCategorieIndisponibles";

        // On génère chaque tuple d'examen indisponibles
        for (int i = 0; i < RessourcesBdD.listeDesExamens.Length; ++i)
        {
            // On rend interactible le bouton (ZoneInfo)
            examenIDGO.interactable = true;

            // On réinitialise la couleur du Tuple
            imageTuple.color = new Color32(49, 97, 125, 255);
            iconeCheck.enabled = false;

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = truncateString(examen.NomExamen, 15);
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen - 1).ToString();

            // On affiche la valeur du joueur (avancement compétence pour Examen)
            int avancement = 0;

            bool playing = true;
            // On vérifie si le joueur peut passer l'examen
            for (int j = 0; j < examen.CompétencesRequises.Length; ++j)
            {
                int valeurr = examen.CompétencesRequises[j].Valeur;
                if (!verificationCompAvecJoueur(examen.CompétencesRequises[j].ID, valeurr))
                {
                    playing = false;
                }
                else
                {
                    ++avancement;
                }
            }

            double valeurJoueur = ((double)avancement / 5) * 204.6;
            imageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2((float)valeurJoueur, 42.43f);

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
            imageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2(204.6f, 42.43f);
            iconeCheck.enabled = false;

            // Définition nom et id examen
            Examen examen = RessourcesBdD.listeDesExamens[i];
            Text Texte = nomExamen;
            Texte.text = truncateString(examen.NomExamen, 15);
            Text examenID = examenIDGO.GetComponentInChildren<Text>();
            examenID.text = (examen.IDExamen - 1).ToString();

            // On vérifie si l'examen a déjà été passé par le joueur
            //StartCoroutine(RessourcesBdD.recupExamensNonJouables());
            foreach (int id in RessourcesBdD.listeDesExamensNonJouables)
            {
                if (id == examen.IDExamen)
                {
                    examenIDGO.interactable = false;
                    imageTuple.color = new Color32(6, 212, 168, 255);
                    imageTuple.GetComponent<RectTransform>().sizeDelta = new Vector2(204.6f, 42.43f);
                    iconeCheck.enabled = true;

                    // On instancie le tuple
                    instance = Instantiate(Tuple, new Vector3(0F, 0F, 0F), Tuple.transform.rotation);
                    instance.transform.parent = GameObject.Find("ListeExamens").transform;
                    instance.transform.name = "TupleExamen" + (i + 1);
                }
            }
        }
    }

    public IEnumerator WaitFor(float seconds)
    {
        yield return new WaitForSeconds(seconds);
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
