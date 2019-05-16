using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListeObjetFicheP: MonoBehaviour
{
    public GameObject Tuple;
    public Text NomObjet;
    public Text Quantite;

    public RawImage logoPuzzle;
    public GameObject FondQuantite;

    GameObject instance;

    private void Start()
    {
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;
            //logoPuzzle.SetActive(true);

            logoPuzzle.texture = Resources.Load<Texture>("icones/Item" + i);
            FondQuantite.SetActive(true);

            if (Joueur.MesObjets[i] > 0)
            {
                Quantite.text = Joueur.MesObjets[i].ToString();
                instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }

        /*
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;
            //logoPuzzle.SetActive(true);
            FondQuantite.SetActive(true);

            if (Joueur.MesObjets[i] == 0)
            {
                Debug.Log("Objet non détenu : " + i);
                Quantite.text = "";
                //logoPuzzle.SetActive(false);
                FondQuantite.SetActive(false);
                instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
        */
    }
}
