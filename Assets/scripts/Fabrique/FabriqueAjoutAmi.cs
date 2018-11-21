using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueAjoutAmi : MonoBehaviour {
    public GameObject tupleajout;
    public Text nomtuple;
    GameObject instance;

    void Start()
    {
        for (int i = 0; i<RessourcesBdD.listeDesJoueurs.Length;++i)
        {
            bool testSiAmi = false;
            foreach(AutreJoueur autre2 in Joueur.MesAmis)
            {
                if (RessourcesBdD.listeDesJoueurs[i].SonID == autre2.SonID)
                {
                    testSiAmi = true;
                    break;
                }
            }
            if (testSiAmi)
            {
                continue;
            }
            else
            {
                nomtuple.text = RessourcesBdD.listeDesJoueurs[i].SonNom;
                instance = Instantiate(tupleajout, new Vector3(0, 0, 0), tupleajout.transform.rotation);
                instance.transform.name = "Joueur " + i;
                instance.transform.parent = GameObject.Find("VerticalLayout2").transform;
            }
        }
    }
}
