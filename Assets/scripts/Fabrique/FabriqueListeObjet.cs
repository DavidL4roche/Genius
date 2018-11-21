using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeObjet : MonoBehaviour {
    public GameObject listede4;
    public GameObject objet;
    public Text idobjet;
    public Text quantite;
    GameObject InstanceListe;
    GameObject Instanceobjet;
    public GameObject VerticalLayout;
    // Use this for initialization
    void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
