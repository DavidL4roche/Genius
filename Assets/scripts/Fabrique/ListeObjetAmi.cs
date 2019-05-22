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
                NomObjet.text = RessourcesBdD.listeDesObjets[i].Nom;
                //logoPuzzle.SetActive(true);

                logoPuzzle.texture = Resources.Load<Texture>("icones/Item" + i);
                FondQuantite.SetActive(true);
                Quantite.text = Joueur.MesObjets[i].ToString();
                Debug.Log(RessourcesBdD.listeDesObjets[i].Bonus.NomBonus);
                switch(RessourcesBdD.listeDesObjets[i].Bonus.NomBonus)
                {
                    case "Orcus":
                        Debug.Log("Orcus");
                        IconeRessource.texture = Resources.Load<Texture>("icones/Icon_orcus_white");
                        break;
                    case "Compétence":
                        Debug.Log("Compétence");
                        IconeRessource.texture = Resources.Load<Texture>("icones/Icon_comp_requis");
                        break;
                    case "Temps":
                        Debug.Log("Temps");
                        IconeRessource.texture = Resources.Load<Texture>("icones/Icon_durée");
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
