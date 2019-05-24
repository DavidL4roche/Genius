using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuvrirMenuReseau : MonoBehaviour {

    public GameObject MenuAmi;

    public void Clique()
    {
        GameObject[] respawns = GameObject.FindGameObjectsWithTag("menuami");
        foreach (GameObject respawn in respawns)
        {
            if (respawn != MenuAmi)
            {
                respawn.SetActive(false);
            }
        }

        MenuAmi.SetActive(!MenuAmi.activeInHierarchy);
    }
}
