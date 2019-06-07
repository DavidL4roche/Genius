using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chargement : MonoBehaviour {

    public GameObject EcranChargement;
        
	public void RendreVisibleEcran () {
        EcranChargement.SetActive(true);
	}
}
