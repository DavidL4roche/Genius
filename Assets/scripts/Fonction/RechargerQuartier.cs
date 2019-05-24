using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RechargerQuartier : MonoBehaviour {

    public static string NomQuartier;
	
	public void definirNomQuartier()
    {
        NomQuartier = gameObject.name.Substring(8);

        if (NomQuartier == "InfoCom")
        {
            NomQuartier = "InformationCommunication";
        }
    }

    public void Recharger()
    {
        SceneManager.LoadScene("Daedelus", LoadSceneMode.Single);
    }
}
