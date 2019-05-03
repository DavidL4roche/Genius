using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; // Permet un meilleur traitement du JSON

public class ChangerInfosProfil : MonoBehaviour {
    
    public InputField nomJoueur;
    public InputField mdpJoueur;
    public InputField mdpJoueurVerif;

    // Paramètres
    private string url = Configuration.url + "scripts/ChangePlayerStats.php";
    private WWW download;

    private string monJson;
    private JSONNode monNode;

    private string urlComp;

    // Permet d'appeler l'URL pour transmettre au script PHP les informations
    public void CallFunction()
    {
        StartCoroutine(ChangerInfos());
    }

    // Permet de créer un utilisateur dans la base
    public IEnumerator ChangerInfos()
    {
        bool continueStatus = true;

        // On change le pseudo
        if (nomJoueur.text.Length <= 24)
        {
            urlComp = url;
            urlComp += "?stat=" + "PCName" + "&value=" + nomJoueur.text + "&id=" + Joueur.IDJoueur;
            download = new WWW(urlComp);
            yield return download;

            if ((!string.IsNullOrEmpty(download.error)))
            {
                continueStatus = false;
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
                    continueStatus = false;
                    ChargerPopup.Charger("Erreur");
                    MessageErreur.messageErreur = monNode["msg"].Value;
                }
            }
        }
        else
        {
            continueStatus = false;
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Le pseudo est trop long (24 caractères max)";
        }

        // S'il n'y a pas eu d'erreur pour le pseudo on continue alors avec le mot de passe
        if (continueStatus)
        {
            // On change le mot de passe
            if (mdpJoueur.text == mdpJoueurVerif.text)
            {
                if (mdpJoueur.text.Length <= 24 && mdpJoueur.text.Length > 2)
                {
                    Debug.Log(mdpJoueur.text);
                    // On l'insert en base
                    urlComp = url;
                    urlComp += "?stat=" + "Password" + "&value=" + mdpJoueur.text + "&id=" + Joueur.IDJoueur;
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

                        // L'inscription s'est bien déroulée
                        else
                        {
                            // On ferme la fenêtre de modification
                            FermerUneFenetre fermer = new FermerUneFenetre();
                            fermer.Fermer("ModificationProfil");

                            // On ouvre la fenêtre de Profil
                            ChargerFenetreSupp charger = new ChargerFenetreSupp();
                            charger.Charger("Profil");

                            // On affiche le Succès
                            ChargerPopup.Charger("Succes");
                            MessageErreur.messageErreur = "Votre compte a bien été modifié";

                            // On change le nom du joueur en local 
                            Joueur.NomJoueur = nomJoueur.text;
                        }
                    }
                }
                // Si le mot de passe est vide
                else if (mdpJoueur.text.Length == 0)
                {
                    // On ferme la fenêtre de modification
                    FermerUneFenetre fermer = new FermerUneFenetre();
                    fermer.Fermer("ModificationProfil");

                    // On ouvre la fenêtre de Profil
                    ChargerFenetreSupp charger = new ChargerFenetreSupp();
                    charger.Charger("Profil");

                    // On affiche le Succès
                    ChargerPopup.Charger("Succes");
                    MessageErreur.messageErreur = "Votre compte a bien été modifié";

                    // On change le nom du joueur en local 
                    Joueur.NomJoueur = nomJoueur.text;
                }
                else
                {
                    ChargerPopup.Charger("Erreur");
                    MessageErreur.messageErreur = "Le mot de passe est trop court ou trop long (2-24 caractères)";
                }
            }
            else
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Les mots de passe doivent être identiques.";
            }
        }
    }
}
