using MySql.Data.MySqlClient;
using SimpleJSON;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemarrageGenius2 : MonoBehaviour {

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

    public GameObject EcranErreur;

    public void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Pas d'internet");
            RendreVisibleEcranErreur(true);
        }
        else
        {
            StartCoroutine(MyCoroutine());
        }
    }

    IEnumerator MyCoroutine()
    {
        // On vérifie si l'adresse IP correspond à un compte
        string ip = LocalIPAddress();
        urlComp = url + "?ip=" + ip;
        download = new WWW(urlComp);
        yield return download;

        if ((!string.IsNullOrEmpty(download.error)))
        {
            print("Error downloading: " + download.error);
            RendreVisibleEcranErreur(true);
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
                // On redirige vers la connexion
                ChargerLieu charger = new ChargerLieu();
                charger.Charger("Index2");
            }

            // L'adresse correspond à un compte
            else
            {
                // On définit l'identifiant du compte lié
                int playerId = monNode["msg"];

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
                            //string id = monNode["msg"].Value;
                            urlComp2 = url2 + "?id=" + playerId;

                            download = new WWW(urlComp2);
                            yield return download;

                            if ((!string.IsNullOrEmpty(download.error)))
                            {
                                print("Error downloading: " + download.error);
                            }
                            else
                            {
                                //ChargerPopup.Charger("Succes");
                                //MessageErreur.messageErreur = "Lancement du jeu. Etape 4";

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
                                    /*
                                    // On teste la connection à la base de données
                                    int total = 0;
                                    string requeteTest = "SELECT COUNT(*) AS Total FROM mission";
                                    MySqlCommand commande = new MySqlCommand(requeteTest, Connexion.connexion);

                                    // TODO : EXECUTEREADER N'EST PAS LA BONNE METHODE, IL FAUT FAIRE APPEL A DES SCRIPTS PHP
                                    try
                                    {
                                        MySqlDataReader lien = commande.ExecuteReader();
                                        
                                        while (lien.Read())
                                        {
                                            total = Int32.Parse(lien["Total"].ToString());
                                        }
                                        lien.Close();
                                    }
                                    catch
                                    {
                                        ChargerPopup.Charger("Erreur");
                                        MessageErreur.messageErreur = "Impossible d'accéder à la base de données.";
                                    }
                                    
                                    // On connecte automatiquement au compte lié
                                    if (total != 0)
                                    {
                                    */
                                        // On récupère les données du Joueur pour l'attribuer à notre objet
                                        int.TryParse(monNode["utilisateur"][0]["id"].Value, out Joueur.IDJoueur);
                                        Joueur.NomJoueur = monNode["utilisateur"][0]["pseudo"].Value;
                                        Joueur.dateDerniereCo = Convert.ToDateTime(monNode["utilisateur"][0]["lastConnection"].Value);
                                        ChargerLieu loading = new ChargerLieu();
                                        Instantiate(JoueurLoge);

                                        ChargerPopup.Charger("Succes");
                                        MessageErreur.messageErreur = "Connexion réussie. Lancement du jeu.";

                                        // On charge la carte
                                        loading.Charger("Daedelus");
                                    //}
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

    public void RendreVisibleEcranErreur(bool visible)
    {
        EcranErreur.SetActive(visible);
    }

    public void RelancerJeu()
    {
        // On "relance" le jeu
        SceneManager.LoadScene("Index1");
    }
}
