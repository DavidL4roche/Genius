using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueCompétence : MonoBehaviour {
    public GameObject LeTuple;
    public Slider LeSlider;
    public Text LeTexteDansLeSlider;
    public Text TexteDuNom;
    GameObject instance;


    private void Start()
    {
        for(int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
        {
            LeSlider.value = Joueur.MesValeursCompetences[i];
            LeTexteDansLeSlider.text = Joueur.MesValeursCompetences[i].ToString();
            TexteDuNom.text = RessourcesBdD.listeDesCompétences[i].NomCompétence;
            instance = Instantiate(LeTuple, new Vector3(0.0F,0.0F,0.0F), LeTuple.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Tuple " + (i + 1);
        }
    }
}
