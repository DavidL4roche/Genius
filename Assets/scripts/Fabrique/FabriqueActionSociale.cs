using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueActionSociale : MonoBehaviour {
    AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];
    string choix = VerifActionSocial.ActionSocialeChoisie;

    public Text LeNomDuJoueur;

    public GameObject Bouton;
    public GameObject tuple;
    public RawImage CadreBlanc;
    public Text LeTexteSlider;
    public RawImage imageAction;
    //public Text QuantitéObjet;
    public Text TexteTuple;

    GameObject instance;

    public void Start()
    {
        Bouton.gameObject.SetActive(true);
        //CadreBlanc.color = new Color(1.0F,1.0F,1.0F);
        //QuantitéObjet.gameObject.SetActive(false);
        LeNomDuJoueur.text = joueur.SonNom;
        if (choix == "Objet")
        {
            //QuantitéObjet.gameObject.SetActive(true);
            for (int i = 0; i<RessourcesBdD.listeDesObjets.Length; ++i)
            {
                if (joueur.SesObjets[i] > 0)
                {
                    //QuantitéObjet.text = joueur.SesObjets[i].ToString();
                    TexteTuple.text = RessourcesBdD.listeDesObjets[i].Nom;
                    imageAction.texture = Resources.Load<Texture>("icones/Item" + i);
                    Bouton.transform.name = i.ToString();
                    instance = Instantiate(tuple, new Vector3(0, 0, 0), tuple.transform.rotation);
                    instance.transform.name = "Objet " + i;
                    instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                }
            }
        }
        else
        {
            for (int i = 0; i < RessourcesBdD.listeDesCompétences.Length; ++i)
            {
                if (joueur.SesCompétences[i] == 100)
                {
                    TexteTuple.text = ">_" + RessourcesBdD.listeDesCompétences[i].NomCompétence;
                    // LeSlider.GetComponent<Slider>().value = joueur.SesCompétences[i];
                    LeTexteSlider.text = joueur.SesCompétences[i].ToString();
                    imageAction.texture = Resources.Load<Texture>("icones/Icon_diplome");
                    Bouton.transform.name = i.ToString();
                    instance = Instantiate(tuple, new Vector3(0, 0, 0), tuple.transform.rotation);
                    instance.transform.name = "Compétence " + i;
                    instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                }
            }
        }
        //QuantitéObjet.gameObject.SetActive(true);
        Bouton.gameObject.SetActive(false);
    }
}
