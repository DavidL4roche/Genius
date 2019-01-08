using MySql.Data.MySqlClient;
using SimpleJSON;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class DemarrageGenius : MonoBehaviour {

    // Paramètres
    private string url = "http://seriousgameiut.alwaysdata.net/scripts/CheckIP.php";
    private string url2 = "http://seriousgameiut.alwaysdata.net/scripts/ConnectById.php";
    private string url3 = "http://seriousgameiut.alwaysdata.net/scripts/GetConnectOnIP.php";
    private WWW download;
    
    string requete;
    private string urlComp;
    private string urlComp2;
    public Joueur JoueurLoge;

    private string monJson;
    private JSONNode monNode;

    public void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine()
    {
        // Chargement
        yield return new WaitForSeconds(3f);

        // On redirige vers la connexion
        ChargerLieu charger = new ChargerLieu();
        charger.Charger("Index1");
    }
}
