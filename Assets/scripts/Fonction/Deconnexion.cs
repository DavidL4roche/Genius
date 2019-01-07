using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deconnexion : MonoBehaviour {
    
	public void Deconnection () {
        /*
        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }
        */

        Destroy(GameObject.Find("Joueur(Clone)").gameObject);
        Destroy(GameObject.Find("Camera").gameObject);

        // On redirige vers l'index
        ChargerLieu loading = new ChargerLieu();
        loading.Charger("Index");
    }
}
