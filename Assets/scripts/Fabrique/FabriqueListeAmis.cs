using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueListeAmis : MonoBehaviour {
    public GameObject TupleAmi;
    public Text NomAmi;
    public Button VoirProfil;
    GameObject instance;
    private void Start()
    {
        for (int i =0;i<Joueur.MesAmis.Length;++i) { 
            NomAmi.text = Joueur.MesAmis[i].SonNom;
            VoirProfil.transform.name = "Ami " + i;
            instance = Instantiate(TupleAmi, new Vector3(0, 0, 0), TupleAmi.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout").transform;
            instance.transform.name = "Ami " + i;
        }
    }
}
