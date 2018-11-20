using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerificationLieu : MonoBehaviour {
    public static int IDLieu = 0;


    private void Awake()
    {
        for (int i = 0; i < RessourcesBdD.listeDesLieux.Length; ++i)
        {
            if (SceneManager.GetActiveScene().name == RessourcesBdD.listeDesLieux[i].NomLieu)
            {
                IDLieu = RessourcesBdD.listeDesLieux[i].IDLieu;
                //Debug.Log("Tu es dans le quartier n." + IDQuartier);
                break;
            }
        }
        Debug.Log(IDLieu);
    }
}
