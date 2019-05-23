using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListeObjetAmi: MonoBehaviour
{
    AutreJoueur joueur = Joueur.MesAmis[VerificationAmi.AmiChoisi];

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
            if (joueur.SesObjets[i] > 0)
            {
                IconeRessource.enabled = true;
                NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;
                //logoPuzzle.SetActive(true);

                logoPuzzle.texture = Resources.Load<Texture>("icones/Item" + (i + 1));
                FondQuantite.SetActive(true);
                Quantite.text = Joueur.MesObjets[i].ToString();

                switch(RessourcesBdD.listeDesObjets[i].Bonus.NomBonus)
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
    }
}
