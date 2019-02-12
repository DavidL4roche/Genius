using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VerifQuartier : MonoBehaviour {
    public static int IDQuartier = 0;
    private void Awake()
    {
        for(int i = 0; i < RessourcesBdD.listeDesQuartiers.Length; ++i)
        {
            if (SceneManager.GetActiveScene().name == RessourcesBdD.listeDesQuartiers[i].NomQuartier)
            {
                IDQuartier = RessourcesBdD.listeDesQuartiers[i].IDQuartier;
                //Debug.Log("Tu es dans le quartier n." + IDQuartier);
                break;
            }
        }
    }
}
