using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeAmis : MonoBehaviour {
    public GameObject TupleAmi;
    public Text NomAmi;
    public Image Cadre;
    public Button VoirProfil;
    GameObject instance;
    private void Start()
    {
        bool color = true;
        if (Joueur.MesAmis != null) 
        {
            for (int i = 0; i < Joueur.MesAmis.Length; ++i)
            {
                NomAmi.text = Joueur.MesAmis[i].SonNom;
                VoirProfil.transform.name = "Ami " + i;
                if (color)
                {
                    Cadre.GetComponent<Image>().color = new Color32(39, 41, 65, 255);
                }
                else
                {
                    Cadre.GetComponent<Image>().color = new Color32(34, 36, 57, 255);
                }

                instance = Instantiate(TupleAmi, new Vector3(0, 0, 0), TupleAmi.transform.rotation);
                instance.transform.parent = GameObject.Find("VerticalLayout").transform;
                instance.transform.name = "Ami " + i;
                color = ((color) ? false : true);
            }
        }
    }
}
