using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueTrophees : MonoBehaviour {

    public GameObject LeTuple;
    public RawImage Icone;
    public Text TexteDuNom;
    GameObject instance;
    public Text LeTexteQuantite;
    private void Start()
    {
        LeTexteQuantite.text = "";
        for (int i = 0; i < RessourcesBdD.listeDesTrophees.Length; ++i)
        {
            if (Joueur.MesTrophees[i] == true)
            {
                Icone.texture = RessourcesBdD.listeDesTrophees[i].texture;
                TexteDuNom.text = RessourcesBdD.listeDesTrophees[i].Description;
                instance = Instantiate(LeTuple, new Vector3(0.0F, 0.0F, 0.0F), LeTuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
    }
}
