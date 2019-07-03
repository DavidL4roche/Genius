using MySql.Data.MySqlClient;
using SimpleJSON;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class DemarrageGenius : MonoBehaviour {

    // Paramètres
    private string url = Configuration.url + "scripts/CheckIP.php";
    private string url2 = Configuration.url + "scripts/ConnectById.php";
    private string url3 = Configuration.url + "scripts/GetConnectOnIP.php";
    private WWW download;
    
    string requete;
    private string urlComp;
    private string urlComp2;
    public Joueur JoueurLoge;

    private string monJson;
    private JSONNode monNode;

    public void Start()
    {
        //StartCoroutine(MyCoroutine());

        // On détruit tout les objets
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)
        {
            Destroy(GameObjects[i]);
        }

        ChargerLieu charger = new ChargerLieu();
        charger.Charger("Index1");
    }

    private IEnumerator MyCoroutine()
    {
        // Chargement
        yield return new WaitForSeconds(0f); //3f

        // On redirige vers la connexion
        ChargerLieu charger = new ChargerLieu();
        charger.Charger("Index1");
    }
}
