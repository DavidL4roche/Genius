using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueActionSociale : MonoBehaviour {
    AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];
    public GameObject Bouton;
    string choix = VerifActionSocial.ActionSocialeChoisie;
    public GameObject tuple;
    public Image CadreBlanc;
    public Slider LeSlider;
    public Text LeTexteSlider;
    public Text LeNomDuJoueur;
    public Text QuantitéObjet;
    public Text TexteTuple;
    GameObject instance;
    public void Start()
    {
        Bouton.gameObject.SetActive(true);
        CadreBlanc.color = new Color(1.0F,1.0F,1.0F);
        LeSlider.gameObject.SetActive(false);
        QuantitéObjet.gameObject.SetActive(false);
        LeNomDuJoueur.text = joueur.SonNom;
        if (choix == "Objet")
        {
            LeSlider.gameObject.SetActive(false);
            QuantitéObjet.gameObject.SetActive(true);
            for (int i = 0; i<RessourcesBdD.listeDesObjets.Length; ++i)
            {
                QuantitéObjet.text = joueur.SesObjets[i].ToString();
                TexteTuple.text = RessourcesBdD.listeDesObjets[i].Nom;
                instance = Instantiate(tuple, new Vector3(0,0,0), tuple.transform.rotation);
                instance.transform.name = "Objet " + i;
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            }
        }
        else
        {
            LeSlider.gameObject.SetActive(true);
            for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
            {
                TexteTuple.text = RessourcesBdD.listeDesCompétences[i].NomCompétence;
               // LeSlider.GetComponent<Slider>().value = joueur.SesCompétences[i];
                LeTexteSlider.text = joueur.SesCompétences[i].ToString();
                instance = Instantiate(tuple, new Vector3(0, 0, 0), tuple.transform.rotation);
                instance.transform.name = "Compétence " + i;
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            }
        }
        LeSlider.gameObject.SetActive(false);
        QuantitéObjet.gameObject.SetActive(true);
        Bouton.gameObject.SetActive(false);  
    }
}
