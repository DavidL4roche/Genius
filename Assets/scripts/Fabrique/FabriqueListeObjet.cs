using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeObjet : MonoBehaviour {

    public GameObject Tuple;
    public Text NomObjet;
    public Text Quantite;
    public Text IDObjet;
    public RawImage IconeRessource;
    public Text Gain;

    public RawImage logoPuzzle;
    public GameObject FondQuantite;

    GameObject instance;

    void Start () {
        // On crée le premier objet (AUCUN OBJET)
        NomObjet.text = "Aucun objet";
        IconeRessource.enabled = false;
        logoPuzzle.enabled = false;
        FondQuantite.SetActive(false);
        IDObjet.text = "X";
        Quantite.text = "";
        Gain.text = "";

        instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
        instance.transform.parent = GameObject.Find("VerticalLayout").transform;
        instance.transform.name = "Tuple 0";

        // On parcourt tous les objets du jeu
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            IconeRessource.enabled = true;
            logoPuzzle.enabled = true;

            if (Joueur.MesObjets[i] > 0)
            {
                NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;

                logoPuzzle.texture = Resources.Load<Texture>("icones/Item" + (i+1));
                FondQuantite.SetActive(true);
                IDObjet.text = RessourcesBdD.listeDesObjets[i].ID.ToString();
                Quantite.text = Joueur.MesObjets[i].ToString();

                switch (RessourcesBdD.listeDesObjets[i].Bonus.NomBonus)
                {
                    case "Orcus":
                        IconeRessource.texture = Resources.Load<Texture>("icones/IconM_orcus");
                        break;
                    case "Compétence":
                        IconeRessource.texture = Resources.Load<Texture>("icones/IconM_comp");
                        break;
                    case "Temps":
                        IconeRessource.texture = Resources.Load<Texture>("icones/IconM_durée");
                        break;
                }

                Gain.text = "+" + RessourcesBdD.listeDesObjets[i].Valeur;

                instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }

        /*
        idobjet.text = "";
        quantite.text = "";
        int maxsurligne = 4;
        int listeNombre = 1;
        InstanceListe = Instantiate(listede4, new Vector3(0.0F,0.0F,0.0F),listede4.transform.rotation);
        InstanceListe.transform.parent = GameObject.Find("VerticalLayout").transform;
        InstanceListe.transform.name = "Liste" + listeNombre;
        idobjet.text = "X";
        quantite.text = "";
        Instanceobjet = Instantiate(objet, new Vector3(0.0F, 0.0F, 0.0F), objet.transform.rotation);
        Instanceobjet.transform.parent = GameObject.Find("Liste"+listeNombre).transform;
        Instanceobjet.transform.name = "Objet0";  
        --maxsurligne;
        for (int i = 0; i < Joueur.MesObjets.Length; ++i)
        {
            if (Joueur.MesObjets[i] != 0)
            {
                idobjet.text = RessourcesBdD.listeDesObjets[i].ID.ToString();
                quantite.text = Joueur.MesObjets[i].ToString();
                if (maxsurligne == 0)
                {
                    ++listeNombre;
                    InstanceListe = Instantiate(listede4, new Vector3(0.0F, 0.0F, 0.0F), listede4.transform.rotation);
                    InstanceListe.transform.parent = VerticalLayout.transform;
                    InstanceListe.transform.name = "Liste" + listeNombre;
                    maxsurligne = 4;
                    Instanceobjet = Instantiate(objet, new Vector3(0.0F, 0.0F, 0.0F), objet.transform.rotation);
                    Instanceobjet.transform.parent = GameObject.Find("Liste" + listeNombre).transform;
                    Instanceobjet.transform.name = "Objet" + (i + 1);
                    --maxsurligne;
                }
                else
                {
                    Instanceobjet = Instantiate(objet, new Vector3(0.0F, 0.0F, 0.0F), objet.transform.rotation);
                    Instanceobjet.transform.parent = GameObject.Find("Liste" + listeNombre).transform;
                    Instanceobjet.transform.name = "Objet" + (i + 1);
                    --maxsurligne;
                }
            }
        }
        */
	}
}
