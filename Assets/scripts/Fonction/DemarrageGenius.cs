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

    void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        // Chargement
        yield return new WaitForSeconds(3f);

        // On vérifie si l'adresse IP correspond à un compte
        string ip = LocalIPAddress();
        urlComp = url + "?ip=" + ip;
        download = new WWW(urlComp);
        yield return download;

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
        }
        else
        {
            monJson = download.text;
            monNode = JSON.Parse(monJson);

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
            string result = monNode["result"].Value;

            // Si l'adresse IP ne correspond à aucun compte
            if (result.ToLower() == "false")
            {
                Debug.Log("Adresse IP ne correspond à aucun compte");
                // On redirige vers la connexion
                ChargerLieu charger = new ChargerLieu();
                charger.Charger("Index2");
            }

            // L'adresse correspond à un compte
            else
            {
                // On vérifie si l'utilisateur s'est déconnecté
                urlComp = url3 + "?ip=" + ip;
                download = new WWW(urlComp);
                yield return download;

                if ((!string.IsNullOrEmpty(download.error)))
                {
                    print("Error downloading: " + download.error);
                }
                else
                {
                    monJson = download.text;
                    monNode = JSON.Parse(monJson);

                    Debug.Log(monNode["msg"].Value);

                    // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
                    string result3 = monNode["result"].Value;

                    // Si l'adresse IP ne correspond à aucun compte
                    if (result.ToLower() == "false")
                    {
                        // On redirige vers la connexion
                        ChargerLieu charger = new ChargerLieu();
                        charger.Charger("Index2");
                    }
                    else
                    {
                        // On vérifie si l'utilisateur s'est déconnecté (si c'est non :)
                        if (monNode["msg"].Value == "1")
                        {
                            // On connecte automatiquement au compte lié
                            string id = monNode["msg"].Value;
                            urlComp2 = url2 + "?id=" + id;

                            download = new WWW(urlComp2);
                            yield return download;

                            if ((!string.IsNullOrEmpty(download.error)))
                            {
                                print("Error downloading: " + download.error);
                            }
                            else
                            {
                                monJson = download.text;
                                monNode = JSON.Parse(monJson);

                                // On vérifie si le JSON renvoyé est rempli (est-ce qu'une réponse est renvoyé)
                                string result2 = monNode["result"].Value;

                                // L'id n'existe pas
                                if (result2.ToLower() == "false")
                                {
                                    // On redirige vers la connexion
                                    ChargerLieu charger = new ChargerLieu();
                                    charger.Charger("Index2");
                                }

                                // L'adresse correspond à un compte
                                else
                                {
                                    // On connecte automatiquement au compte lié
                                    // On récupère les données du Joueur pour l'attribuer à notre objet
                                    int.TryParse(monNode["utilisateur"][0]["id"].Value, out Joueur.IDJoueur);
                                    Joueur.NomJoueur = monNode["utilisateur"][0]["pseudo"].Value;
                                    Joueur.dateDerniereCo = Convert.ToDateTime(monNode["utilisateur"][0]["lastConnection"].Value);
                                    ChargerLieu loading = new ChargerLieu();
                                    Instantiate(JoueurLoge);

                                    // On vérifie si c'est la première connection de l'utilisateur
                                    if (monNode["msg"]["utilisateur"][0]["isFirstConnection"] == 1)
                                    {
                                        // On change le booléen isFirstConnection du joueur en faux (0)
                                        string urlStat = "http://seriousgameiut.alwaysdata.net/scripts/ChangePlayerStats.php";
                                        urlStat += "?stat=isFirstConnection&value=0&id=" + monNode["msg"]["utilisateur"][0]["id"].Value;
                                        download = new WWW(urlStat);
                                        yield return download;
                                        loading.Charger("Tutoriel");
                                    }
                                    else
                                    {
                                        loading.Charger("Daedelus");
                                    }
                                }
                            }
                        }
                        // Si l'utilisateur s'est déconnecté
                        else
                        {
                            // On redirige vers la connexion
                            ChargerLieu charger = new ChargerLieu();
                            charger.Charger("Index2");
                        }
                    }
                }
            }
        }

        
    }

    // Récupère l'adresse IP du support local
    public static string LocalIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}
