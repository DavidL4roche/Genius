using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListeObjetFicheP: MonoBehaviour
{
    public GameObject LeTuple;
    public RawImage Icone;
    public Text LeTexteQuantite;
    public Text TexteDuNom;
    GameObject instance;


    private void Start()
    {
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            if (Joueur.MesObjets[i] > 0)
            {
                TexteDuNom.text = RessourcesBdD.listeDesObjets[i].Nom;
                LeTexteQuantite.text = Joueur.MesObjets[i].ToString();
                instance = Instantiate(LeTuple, new Vector3(0.0F, 0.0F, 0.0F), LeTuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }   
        }
    }
}
