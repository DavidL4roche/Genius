using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON
using MySql.Data.MySqlClient;

public class TestConnexion : MonoBehaviour {
    // Paramètres
    private string url = Configuration.url + "scripts/CheckConnection.php";
    private string url2 = Configuration.url + "scripts/AddIP.php";
    private WWW download;
    private WWW download2;

    public InputField pseudo;
    public InputField pass;
    public Joueur JoueurLoge;
    string requete;
    private string urlComp;

    private Connexion connexion;

    private string monJson;
    private JSONNode monNode;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        StartCoroutine(CheckConnection());
    }

    // Permet de se connecter
    public IEnumerator CheckConnection()
    {
        string ipLocal = DemarrageGenius2.LocalIPAddress();

        urlComp = url + "?pseudo=" + pseudo.text + "&pass=" + pass.text;
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

            // On vérifie si le JSON renvoyé est rempli (est-ce qu'un utilisateur est renvoyé)
            string result = monNode["result"].Value;

            if (result.ToLower() == "false")
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = monNode["msg"].Value;
            }

            // Sinon on correspond bien à un utilisateur
            else
            {
                // On ajoute l'IP du téléphone au joueur
                string urlComp2 = url2 + "?ip=" + ipLocal + "&playerId=" + monNode["utilisateur"][0]["id"];
                download = new WWW(urlComp2);
                yield return download;

                // On change le booléen isConnected de l'adresse IP du joueur en vrai (1)
                string urlIP = "http://seriousgameiut.alwaysdata.net/scripts/ConnectOnIP.php";
                urlIP += "?connect=true&playerId=" + ipLocal;
                download2 = new WWW(urlIP);
                yield return download2;

                // On récupère les données du Joueur pour l'attribuer à notre objet
                int.TryParse(monNode["utilisateur"][0]["id"].Value, out Joueur.IDJoueur);
                Joueur.NomJoueur = monNode["utilisateur"][0]["pseudo"].Value;
                Joueur.dateDerniereCo = Convert.ToDateTime(monNode["utilisateur"][0]["lastConnection"].Value);
                ChargerLieu loading = new ChargerLieu();

                // On vérifie si c'est la première connection de l'utilisateur
                if (monNode["utilisateur"][0]["isFirstConnection"] == 1)
                {
                    // On change le booléen isFirstConnection du joueur en faux (0)
                    string urlStat = "http://seriousgameiut.alwaysdata.net/scripts/ChangePlayerStats.php";
                    urlStat += "?stat=isFirstConnection&value=0&id=" + monNode["utilisateur"][0]["id"].Value;
                    download = new WWW(urlStat);
                    yield return download;

                    /*
                    // On teste la connection à la base de données
                    int total = 0;
                    string requeteTest = "SELECT COUNT(*) AS Total FROM mission";
                    MySqlCommand commande = new MySqlCommand(requeteTest, Connexion.connexion);
                    MySqlDataReader lien = commande.ExecuteReader();
                    try
                    {
                        while (lien.Read())
                        {
                            total = Int32.Parse(lien["Total"].ToString());
                        }
                    }
                    catch
                    {
                        ChargerPopup.Charger("Erreur");
                        MessageErreur.messageErreur = "Impossible d'accéder à la base de données.";
                    }
                    lien.Close();

                    // On connecte automatiquement au compte lié
                    if (total != 0)
                    {
                    */
                        loading.Charger("Tutoriel");
                        Instantiate(JoueurLoge);
                    //}
                }
                else
                {
                    // On teste la connection à la base de données
                    int total = 0;
                    string requeteTest = "SELECT COUNT(*) AS Total FROM mission";
                    MySqlCommand commande = new MySqlCommand(requeteTest, Connexion.connexion);
                    MySqlDataReader lien = commande.ExecuteReader();
                    try
                    {
                        while (lien.Read())
                        {
                            total = Int32.Parse(lien["Total"].ToString());
                        }
                    }
                    catch
                    {
                        ChargerPopup.Charger("Erreur");
                        MessageErreur.messageErreur = "Impossible d'accéder à la base de données.";
                    }
                    lien.Close();

                    // On connecte automatiquement au compte lié
                    if (total != 0)
                    {
                        loading.Charger("Daedelus");
                        Instantiate(JoueurLoge);
                    }
                }
            }
        }
    }
}
