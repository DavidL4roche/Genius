using MySql.Data.MySqlClient;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AjouterUnAmi : MonoBehaviour
{

    public Text nomAmi;

    public bool continueAjouter = false;
    public bool continueVerifierAmi = false;
    public bool continueRecupAmis = false;
    public bool continueReload = false;

    AutreJoueur ami = null;
    AutreJoueur[] verifSiAmiNul = new AutreJoueur[0];
    bool estAmi = false;

    public void AjouterAmi()
    {
        StartCoroutine(AjouterAmiCoroutine());
    }

    private IEnumerator AjouterAmiCoroutine()
    {
        // Recherche en base
        yield return StartCoroutine(fonctionAjouterAmi());

        // Le joueur existe
        if (verifSiAmiNul.Length > 0)
        {
            yield return StartCoroutine(verifierAmi(ami.SonID));

            // On vérifie si le joueur ne l'a pas déjà en ami
            if (estAmi)
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Ce joueur est déjà votre ami";
            }
            // On vérifie si ce n'est pas le joueur en cours
            else if (ami.SonID == Joueur.IDJoueur)
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Vous ne pouvez pas vous ajouter en tant qu'ami";
            }
            else
            {
                // Insertion en base
                yield return StartCoroutine(insertionAmiEnBase(ami.SonID));
                yield return StartCoroutine(RecupMesAmis());
            }
        }

        // Il n'existe pas, on affiche l'erreur
        else
        {
            ChargerPopup.Charger("Erreur");
            MessageErreur.messageErreur = "Ce joueur n'existe pas";
        }
    }

    /*
    public void Update()
    {
        if (continueAjouter)
        {
            // Le joueur existe
            if (verifSiAmiNul.Length > 0)
            {
                StartCoroutine(verifierAmi(ami.SonID));
            }

            // Il n'existe pas, on affiche l'erreur
            else
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Ce joueur n'existe pas";
            }

            continueAjouter = false;
        }

        if (continueVerifierAmi)
        {
            // On vérifie si le joueur ne l'a pas déjà en ami
            if (estAmi)
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Ce joueur est déjà votre ami";
            }
            // On vérifie si ce n'est pas le joueur en cours
            else if (ami.SonID == Joueur.IDJoueur)
            {
                ChargerPopup.Charger("Erreur");
                MessageErreur.messageErreur = "Vous ne pouvez pas vous ajouter en tant qu'ami";
            }
            else
            {
                // Insertion en base
                StartCoroutine(insertionAmiEnBase(ami.SonID));
            }
            continueVerifierAmi = false;
        }

        if (continueRecupAmis)
        {
            StartCoroutine(RecupMesAmis());
            continueRecupAmis = false;
        }
    }
    */

    // Recherche l'ami (par son pseudo) : on regarde s'il existe
    public IEnumerator fonctionAjouterAmi()
    {
        string urlInfosAmi = Configuration.url + "scripts/GetAmi.php?nomAmi=" + nomAmi.text;

        WWW dlInfosAmi = new WWW(urlInfosAmi);
        yield return dlInfosAmi;

        JSONNode NodeAmis = RessourcesBdD.RenvoiJSONScript(dlInfosAmi);
        Joueur.MesAmis = new AutreJoueur[NodeAmis["msg"].Count];
        for (int i = 0; i < NodeAmis["msg"].Count; ++i)
        {
            ami = new AutreJoueur((int)NodeAmis["msg"][i]["IDPCharacter"], NodeAmis["msg"][i]["PCName"].Value);
            verifSiAmiNul = new AutreJoueur[1];
        }

        continueAjouter = true;
    }

    public IEnumerator insertionAmiEnBase(int idAmi)
    {
        string urlComp = Configuration.url + "scripts/AjouterAmi.php?id=" + Joueur.IDJoueur + "&idAmi=" + idAmi;
        WWW dl = new WWW(urlComp);
        yield return dl;
        continueRecupAmis = true;
    }

    public IEnumerator verifierAmi(int IDAmi)
    {
        string urlAmis = Configuration.url + "scripts/scriptsRessources/VerifierAmi.php?id=" + Joueur.IDJoueur + "&idAmi=" + IDAmi;

        WWW dlAmis = new WWW(urlAmis);
        yield return dlAmis;

        string JsonAmis = dlAmis.text;

        if (JsonAmis == "true")
        {
            estAmi = true;
        }
        else
        {
            estAmi = false;
        }
        continueVerifierAmi = true;
    }

    // Récupère les amis du joueur
    public IEnumerator RecupMesAmis()
    {
        Joueur.MesAmis = new AutreJoueur[0];
        string urlAmis = Configuration.url + "scripts/scriptsRessources/RecupMesAmis.php?id=" + Joueur.IDJoueur;

        WWW dlAmis = new WWW(urlAmis);
        yield return dlAmis;

        JSONNode NodeAmis = RessourcesBdD.RenvoiJSONScript(dlAmis);
        Joueur.MesAmis = new AutreJoueur[NodeAmis["msg"].Count];
        for (int i = 0; i < NodeAmis["msg"].Count; ++i)
        {
            Joueur.MesAmis[i] = new AutreJoueur((int)NodeAmis["msg"][i]["IDPCharacter"], NodeAmis["msg"][i]["PCName"].Value);
        }

        foreach (AutreJoueur ami in Joueur.MesAmis)
        {
            yield return StartCoroutine(ami.majObjetAmi(ami.SonID));
            yield return StartCoroutine(ami.majComp(ami.SonID));
            yield return StartCoroutine(ami.majDiplome(ami.SonID));
            yield return StartCoroutine(RessourcesBdD.RecupActionsSociales());
        }

        FermerUneFenetre fermerfen = new FermerUneFenetre();
        ChargerFenetreSupp chargerfen = new ChargerFenetreSupp();
        fermerfen.Fermer("ReseauSocial");
        chargerfen.Charger("ReseauSocial");
    }
}
