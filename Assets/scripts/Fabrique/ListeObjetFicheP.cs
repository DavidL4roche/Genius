using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListeObjetFicheP: MonoBehaviour
{
    public GameObject Tuple;
    public Text NomObjet;
    public Text Quantite;
    public RawImage IconeRessource;
    public Text Gain;

    public RawImage logoPuzzle;
    public GameObject FondQuantite;

    GameObject instance;

    private void Start()
    {
        for (int i = 0; i < RessourcesBdD.listeDesObjets.Length; ++i)
        {
            NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;
            //logoPuzzle.SetActive(true);

            logoPuzzle.texture = Resources.Load<Texture>("icones/Item" + (i+1));
            FondQuantite.SetActive(true);

            if (Joueur.MesObjets[i] > 0)
            {
                Quantite.text = Joueur.MesObjets[i].ToString();
                instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
                
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
