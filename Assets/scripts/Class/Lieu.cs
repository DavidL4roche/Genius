using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lieu : MonoBehaviour {
    public int IDLieu;
    public string NomLieu;
    public Lieu(int id, string nom)
    {
        IDLieu = id;
        NomLieu = nom;
    }

    public void toString()
    {
        Debug.Log("Lieu " + IDLieu + "(" + NomLieu + ")");
    }
}
