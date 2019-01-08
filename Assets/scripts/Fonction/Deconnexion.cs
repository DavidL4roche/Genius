using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deconnexion : MonoBehaviour {

    private WWW download;
    string ipLocal = DemarrageGenius.LocalIPAddress();

    public void Deconnection () {
        
        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        /*
        Destroy(GameObject.Find("Joueur(Clone)").gameObject);
        Destroy(GameObject.Find("Camera").gameObject);
        */

        StartCoroutine(ChangeIsConnected());

        SceneManager.LoadScene("Index");

        // On redirige vers l'index
        ChargerLieu loading = new ChargerLieu();
        //loading.Charger("Index");
    }

    // On change l'attribut isConnected pour l'IP local
    public IEnumerator ChangeIsConnected()
    {
        // On change le booléen isConnected de l'adresse IP du joueur en vrai (1)
        string urlIP = "http://seriousgameiut.alwaysdata.net/scripts/ConnectOnIP.php";
        urlIP += "?connect=false&playerId=" + ipLocal;
        download = new WWW(urlIP);
        yield return download;
    }
}
