using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueSuppAmi : MonoBehaviour {
    public GameObject TupleSupp;
    public Text NomAmi;
    GameObject instance;
    private void Start()
    {
        for(int i = 0; i<Joueur.MesAmis.Length; ++i)
        {
            NomAmi.text = Joueur.MesAmis[i].SonNom;
            instance = Instantiate(TupleSupp, new Vector3(0, 0, 0), TupleSupp.transform.rotation);
            instance.transform.parent = GameObject.Find("VerticalLayout2").transform;
            instance.transform.name = "Ami " + i; 
        }
    }
}
