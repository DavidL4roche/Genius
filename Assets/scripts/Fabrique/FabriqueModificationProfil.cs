using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FabriqueModificationProfil : MonoBehaviour {

    public InputField nomJoueur;
    public Text placeholderNomJoueur;

    // Use this for initialization
    void Start () {
        nomJoueur.text = Joueur.NomJoueur;
        placeholderNomJoueur.text = Joueur.NomJoueur;
    }
}
