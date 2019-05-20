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
                instance = Instantiate(Tuple, new Vector3(0.0F, 0.0F, 0.0F), Tuple.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Tuple " + (i + 1);
            }
        }
    }
}
